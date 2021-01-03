using System;

namespace Exercise03
{
    class Program
    {
        static void Main(string[] args)
        {
            uint maxNum = 100;
            for(uint i = 1; i <= maxNum; i++){
                if(i % 3 == 0 && i % 5 == 0){
                    Console.Write("FizzBuzz");
                }
                else if (i % 3 == 0)
                {
                    Console.Write("Fizz");
                }
                else if (i % 5 == 0)
                {
                    Console.Write("Buzz");
                }
                else{
                    Console.Write(i);
                }

                if (i < maxNum)
                {
                    Console.Write(", ");
                }
            }
        }
    }
}
