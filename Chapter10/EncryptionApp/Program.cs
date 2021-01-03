using System;
using System.Security.Cryptography;
using Packt.Cryptography;
using static System.Console;

namespace EncryptionApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //HashingPrompt();
            SigningPrompt();
        }

        private static void SigningPrompt(){
            Write("Enter some text to sign: ");
            string data = ReadLine();
            var signature = Protector.GenerateSignature(data);
            WriteLine($"Signature: {signature}");
            WriteLine("Public key used to check signature: ");
            WriteLine(Protector.PublicKey);

            if(Protector.ValidateSignature(data, signature)){
                WriteLine("Correct! Signature is valid");
            }
            else{
                WriteLine("Invalid signature");
            }

            var fakeSignature = signature.Replace(signature[0], 'X');
            if(Protector.ValidateSignature(data, fakeSignature)){
                WriteLine("Correct! Signature is valid");
            }
            else{
                WriteLine($"Invalid signature: {fakeSignature}");
            }
        }

        private static void EncryptionPrompt()
        {
            Write("Enter text to encrypt: ");
            string message = ReadLine();

            Write("Enter a password: ");
            string password = ReadLine();

            string cryptoText = Protector.Encrypt(message, password);

            WriteLine($"Encrypted text: {cryptoText}");
            Write("Re-enter the password: ");
            string passwordConfirmation = ReadLine();

            try
            {
                string clearText = Protector.Decrypt(cryptoText, passwordConfirmation);
                WriteLine($"Decrypted text: {clearText}");
            }
            catch (CryptographicException ex)
            {
                WriteLine($"{"You entered the wrong password!"}\nMore details: {ex.Message}");
            }
            catch (Exception ex)
            {
                WriteLine("Non cryptographic exception: {0}, {1}", ex.GetType().Name, ex.Message);
            }
        }

        private static void HashingPrompt(){
            WriteLine("Registering Alice with Pa$$word");
            var Alice = Protector.Register("Alice", "Pa$$word");
            WriteLine($"Name: {Alice.Name}");
            WriteLine($"Salt: {Alice.Salt}");
            WriteLine($"Password(salted and hashed): {Alice.SaltedHashedPassword}");
            WriteLine();

            Write("Enter a new user to register: ");
            string username = ReadLine();
            Write($"Enter a password for {username}");
            string password = ReadLine();
            var user = Protector.Register(username, password);
            WriteLine($"Name: {user.Name}");
            WriteLine($"Salt: {user.Salt}");
            WriteLine($"Password(salted and hashed): {user.SaltedHashedPassword}");
            WriteLine();

            bool correctPassword = false;

            while(!correctPassword){
                Write("Enter a username to log in: ");
                string loginUsername = ReadLine();
                Write("Enter a password to log in: ");
                string loginPassword = ReadLine();

                correctPassword = Protector.CheckPassword(loginUsername, loginPassword);

                if(correctPassword){
                    WriteLine($"Correct! {loginUsername} has been logged in.");
                }
                else{
                    WriteLine("Invalid username or password. Try again.");
                }
            }

        }
    }
}
