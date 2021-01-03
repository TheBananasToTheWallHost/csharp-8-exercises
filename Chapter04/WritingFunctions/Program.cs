using System;
using static System.Console;

namespace WritingFunctions
{
    class Program
    {
        static void Main(string[] args)
        {
            //RunTimesTable();
            //RunCalculateTax();
            RunCardinalToOrdinal();
        }

        private static void TimeTable(byte number)
        {
            WriteLine("This is the number {0} times tables", number);
            for (int row = 1; row <= 12; row++)
            {
                WriteLine($"{row} x {number} = {row * number}");
            }
            WriteLine();
        }

        private static void RunTimesTable()
        {
            bool isNumber;
            do
            {
                Write("Enter a number between 0 and 255: ");

                isNumber = byte.TryParse(ReadLine(), out byte number);

                if (isNumber)
                {
                    TimeTable(number);
                }
                else
                {
                    WriteLine("You did not enter a valid number: ");
                }
            } while (isNumber);
        }

        private static decimal CalculateTax(decimal amount, string twoLetterRegionCode)
        {
            decimal rate = 0.0m;
            switch (twoLetterRegionCode)
            {
                case "CH":
                    rate = 0.08m;
                    break;
                case "DK":
                case "NO":
                    rate = 0.25m;
                    break;
                case "GB":
                case "FR":
                    rate = 0.2m;
                    break;
                case "HU":
                    rate = 0.27m;
                    break;
                case "OR":
                case "AK":
                case "MT":
                    rate = 0.0m;
                    break;
                case "ND":
                case "WI":
                case "ME":
                case "VA":
                    rate = 0.05m;
                    break;
                case "CA":
                    rate = 0.0825m;
                    break;
                default:
                    rate = 0.06m;
                    break;
            }
            return amount * rate;
        }

        private static void RunCalculateTax(){
            Write("Enter an amount: ");
            string amountInText = ReadLine();
            Write("Enter a two-letter region code: ");
            string region = ReadLine();

            if(decimal.TryParse(amountInText, out decimal amount)){
                decimal taxToPay = CalculateTax(amount, region);
                WriteLine($"You must pay {taxToPay} in sales tax.");
            }
            else{
                WriteLine("You did not enter a valid amount!");
            }
        }

        /// <summary>
        /// Pass a 32 bit integer and it will be converted into its ordinal equivalent.
        /// </summary>
        /// <param name="number"> A cardinal value e.g. 1, 2, 3, and so on.</param>
        /// <returns>An ordinal representation of number</returns>
        private static string CardinalToOrdinal(int number){
            switch (number)
            {
                case 11:
                case 12:
                case 13:
                    return $"{number}th";
                default:
                    string numberAsText = number.ToString();
                    char lastDigit = numberAsText[numberAsText.Length - 1];
                    string suffix = string.Empty;

                    switch (lastDigit)
                    {
                        case '1':
                            suffix = "st";
                            break;
                        case '2':
                            suffix = "nd";
                            break;
                        case '3':
                            suffix = "rd";
                            break;
                        default:
                            suffix = "th";
                            break;
                    }
                    return $"{number}{suffix}";
            }
        }

        private static void RunCardinalToOrdinal(){
            for(int number = 1; number <= 40; number++){
                Write($"{CardinalToOrdinal(number)} ");
            }
            WriteLine();
        }

        private static int Factorial(int number){
            if(number < 1){
                return 0;
            }
            else if(number == 1){
                return 1;
            }
            else{
                return number * Factorial(number - 1);
            }
        }

        private static void RunFactorial(){
            bool isNumber;

            do{
                Write("Enter a number: ");

                isNumber = int.TryParse(ReadLine(), out int number);

                if(isNumber){
                    WriteLine($"{number:N0}! = {Factorial(number):N0}");
                }
                else{
                    WriteLine("You did not enter a valid number");
                }
            }while(isNumber);
        }
    }
}
