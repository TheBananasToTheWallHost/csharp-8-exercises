using System;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
using System.IO;

namespace Exercise02
{
    class Program
    {
        static void Main(string[] args)
        {
            Protector.RegisterCustomer("Bob", "password", "4444-4444-4444-4444");
            Protector.RegisterCustomer("Jill", "12345667887876", "5433-1213-4342-1219");
            Protector.RegisterCustomer("Orgo", "orgobest", "1111-2222-3333-4444");

            Protector.CreateCustomerXML();
            Protector.GetUnencryptedCreditCardNumbers();
        }
    }

    public static class Protector{
        public static List<Customer> customers = new List<Customer>();

        public static readonly byte[] encryptionSalt = Encoding.Unicode.GetBytes("This15theBESTenc");

        public static readonly int iterations = 2500;

        public static Customer RegisterCustomer(string name, string password, string creditCard){
            RandomNumberGenerator rng = RandomNumberGenerator.Create();
            byte[] saltBytes = new byte[32];

            rng.GetBytes(saltBytes);
            string saltText = Convert.ToBase64String(saltBytes);
            string saltAndHashedPassword = SaltAndHashPassword(password, saltText);

            string encryptedCreditCard = Encrypt(saltAndHashedPassword, creditCard);

            var customer = new Customer{
                Name = name,
                CreditCard = encryptedCreditCard,
                Password = saltAndHashedPassword,
                Salt = saltText,
            };

            customers.Add(customer);

            return customer;
        }

        public static void CreateCustomerXML(){
            var serializer = new XmlSerializer(typeof(List<Customer>));

            string path = Path.Combine((Directory.GetParent(Environment.CurrentDirectory)).FullName, "customers.xml");
            FileStream file = File.Create(path);

            using(file){
                serializer.Serialize(file, customers);
            }
        }

        public static void GetUnencryptedCreditCardNumbers(){
            string path = Path.Combine((Directory.GetParent(Environment.CurrentDirectory)).FullName, "customers.xml");
            List<string> creditCardNumbers = new List<string>();

            if(File.Exists(path)){
                var serializer = new XmlSerializer(typeof(List<Customer>));
                FileStream file = File.Open(path, FileMode.Open, FileAccess.Read);
                List<Customer> customerData = (List<Customer>)serializer.Deserialize(file);

                foreach(Customer customer in customerData){
                    var ccNumber = Decrypt(customer.Password, customer.CreditCard);
                    creditCardNumbers.Add(ccNumber);
                }
            }

            foreach(string creditCardNumber in creditCardNumbers){
                Console.WriteLine(creditCardNumber);
            }
        }

        public static uint GetIterations(){
            Random random = new Random();
            int iterations = random.Next(1000, 1500);

            return Convert.ToUInt32(iterations);
        }

        public static string Encrypt(string password, string data, int customIterations = -1){
            byte[] encryptedBytes;
            byte[] dataBytes = Encoding.Unicode.GetBytes(data);
            int actualIterations = customIterations < 1 ? iterations : customIterations;

            Aes aes = Aes.Create();
            var pk = new Rfc2898DeriveBytes(password, encryptionSalt, actualIterations);

            aes.Key = pk.GetBytes(32);
            aes.IV = pk.GetBytes(16);

            using(var memoryStream = new MemoryStream()){
                using(var cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write)){
                    cryptoStream.Write(dataBytes, 0, dataBytes.Length);
                }
                encryptedBytes = memoryStream.ToArray();
            }
            return Convert.ToBase64String(encryptedBytes);
        }

        public static string Decrypt(string password, string encryptedData){
            byte[] decryptedBytes;
            byte[] dataBytes = Convert.FromBase64String(encryptedData);

            Aes aes = Aes.Create();
            var pk = new Rfc2898DeriveBytes(password, encryptionSalt, iterations);
            aes.Key = pk.GetBytes(32);
            aes.IV = pk.GetBytes(16);

            using(var memoryStream = new MemoryStream()){
                using(var cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Write)){
                    cryptoStream.Write(dataBytes, 0, dataBytes.Length);
                }
                decryptedBytes = memoryStream.ToArray();
            }
            return Encoding.Unicode.GetString(decryptedBytes);
        }

        public static string SaltAndHashPassword(string password, string salt){
            string saltedPassword = password + salt;
            var sha = SHA256.Create();

            byte[] hashedAndSaltedPass = sha.ComputeHash(Encoding.Unicode.GetBytes(saltedPassword));

            return Convert.ToBase64String(hashedAndSaltedPass);
        }
    }

    [Serializable]
    public class Customer{
        public string Name {get; set;}
        public string CreditCard{get;set;}
        public string Password{get;set;}
        [XmlIgnore]
        public string Salt{get; set;}
        [XmlIgnore]
        public uint Iterations{get;set;}
    }
}
