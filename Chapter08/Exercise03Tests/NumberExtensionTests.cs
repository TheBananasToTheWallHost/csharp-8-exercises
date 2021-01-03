using System;
using Xunit;
using Nanas.Extensions;

namespace Nanas.Tests
{
    public class NumberExtensionTests
    {
        [Fact]
        public void TestZeroToWords()
        {
            // arrange
            int zero = 0;
            string expected = "zero";

            //act
            string actual = zero.ToWords();

            //assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestNumberWithManyZerosToWords()
        {
        //Given
        int val = 203_000_001;
        string expected = "two hundred and three million, one";
        
        //When
        string actual = val.ToWords();
        
        //Then
        Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestMaxIntToWords()
        {
        //Given
        int val = int.MaxValue;
        string expected = "two billion, one hundred and fourty seven million, four hundred and eighty three thousand, six hundred and fourty seven";
        
        //When
        string actual = val.ToWords();
        
        //Then
        Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestMinIntToWords()
        {
        //Given
        int val = int.MinValue;
        string expected = "negative two billion, one hundred and fourty seven million, four hundred and eighty three thousand, six hundred and fourty eight";
        
        //When
        string actual = val.ToWords();
        
        //Then
        Assert.Equal(expected, actual);
        }
    }
}
