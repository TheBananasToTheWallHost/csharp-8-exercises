using System;
using System.Collections.Generic;

namespace Primes
{
    public class Primes
    {
        private static List<int> primesLessThan1000 = GetPrimesUpTo(1000);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string PrimeFactors(int number){
            int indexer = 0;
            string factors = string.Empty;
            int primeDivisor = primesLessThan1000[indexer];

            while(number > 1){
                if(number % primeDivisor == 0){
                    factors += primeDivisor.ToString() + " ";
                    number /= primeDivisor;
                }
                else{
                    if(indexer < primesLessThan1000.Count - 1){
                        primeDivisor = primesLessThan1000[++indexer];
                    }
                    else{
                        factors += number.ToString() + " ";
                        break;
                    }
                }
            }

            return factors;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="upperLimit"></param>
        /// <returns></returns>
        private static List<int> GetPrimesUpTo(int upperLimit){
            List<int> primes = new List<int>();

            for(int i = 2; i <= upperLimit; i++){
                bool isPrime = true;

                for(int j = 2; j <= i/2; j++){
                    if(i % j == 0){
                        isPrime = false;
                        break;
                    }
                }

                if(isPrime){
                    primes.Add(i);
                }
            }

            return primes;
        }
    }
}
