using System;
using Xunit;
using System.Collections.Generic;
using static Primes.Primes;

namespace Exercise01Tests
{
    public class Exercise01Tests
    {
        [Fact]
        public void TestNumber40()
        {
            //arrange
            int a = 40;
            string expected = "2 2 2 5 ";

            //act
            string actual = PrimeFactors(a);

            //assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestNumber4()
        {
            //arrange
            int a = 4;
            string expected = "2 2 ";

            //act
            string actual = PrimeFactors(a);

            //assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestNumber7()
        {
            //Given
            int a = 7;
            string expected = "7 ";

            //When
            string actual = PrimeFactors(a);

            //Then
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestNumber50()
        {
            //Given
            int a = 50;
            string expected = "2 5 5 ";

            //When
            string actual = PrimeFactors(a);

            //Then
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestNumber999()
        {
            //Given
            int a = 999;
            string expected = "3 3 3 37 ";

            //When
            string actual = PrimeFactors(a);

            //Then
            Assert.Equal(expected, actual);
        }
    }
}
