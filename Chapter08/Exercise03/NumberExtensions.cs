using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Nanas.Extensions
{
    public static class NumberExtensions
    {
        private static readonly Dictionary<byte, string> singleDigitNames = new Dictionary<byte, string>(){
            {0, "zero"}, {1, "one"}, {2, "two"}, {3, "three"}, {4, "four"},
            {5, "five"}, {6, "six"}, {7, "seven"}, {8, "eight"}, {9, "nine"}
        };
        private static readonly Dictionary<byte, string> tensNames = new Dictionary<byte, string>(){
            {2, "twenty"}, {3, "thirty"}, {4, "fourty"}, {5, "fifty"}, {6, "sixty"},
            {7, "seventy"}, {8, "eighty"}, {9, "ninety"}
        };

        private static readonly Dictionary<byte, string> specialTensNames = new Dictionary<byte, string>(){
            {10, "ten"}, {11, "eleven"}, {12, "twelve"}, {13, "thirteen"}, {14, "fourteen"}, {15, "fifteen"},
            {16,"sixteen"}, {17, "seventeen"}, {18, "eighteen"}, {19, "nineteen"}
        };

        private static readonly Dictionary<byte, string> largeNumberNames = new Dictionary<byte, string>(){
            {0, "hundred"}, {1, "thousand"}, {2, "million"},
            {3, "billion"}, {4, "trillion"}, {5, "quadrillion"}, {6, "quintillion"}, {7, "sextillion"},
            {8, "septillion"}, {9, "octillion"}, {10, "nonillion"}, {11, "decillion"}, {12, "undecillion"},
            {13, "duodecillion"}, {14, "tredecillion"}, {15, "quattuordecillion"}, {16, "quincedecillion"},
            {17, "sexdecillion"}, {18, "septendecillion"}, {19, "octodecillion"}, {20, "novemdecillion"}
        };

        public static string ToWords(this byte value)
        {
            return ConvertWholeNumberToWord(value);
        }

        public static string ToWords(this sbyte value)
        {
            return ConvertWholeNumberToWord(value);
        }

        public static string ToWords(this short value)
        {
            return ConvertWholeNumberToWord(value);
        }

        public static string ToWords(this ushort value)
        {
            return ConvertWholeNumberToWord(value);
        }

        public static string ToWords(this int value)
        {
            return ConvertWholeNumberToWord(value);
        }

        public static string ToWords(this uint value)
        {
            return ConvertWholeNumberToWord(value);
        }

        public static string ToWords(this long value)
        {
            return ConvertWholeNumberToWord(value);
        }

        public static string ToWords(this ulong value)
        {
            return ConvertWholeNumberToWord(value);
        }

        public static string ToWords(this BigInteger value)
        {
            BigInteger copy = value < 0 ? BigInteger.Abs(value) : value;
            string unsignedWordRepresentation = ConvertWholeNumberToWord(copy);

            if (value < 0)
            {
                return "negative " + unsignedWordRepresentation;
            }
            return unsignedWordRepresentation;
        }

        public static string ToWords(this float value)
        {
            return ConvertRealNumberToWord(value);
        }

        public static string ToWords(this double value)
        {
            return ConvertRealNumberToWord(value);
        }

        public static string ToWords(this decimal value)
        {
            return ConvertRealNumberToWord(value);
        }

        private static string ConvertRealNumberToWord(dynamic value)
        {
            var integerValue = value < 0 ? Math.Ceiling(value) : Math.Floor(value);
            var decimalValue = Math.Abs(value - integerValue);
            var bigIntegerValue = new BigInteger(integerValue);
            string integerPortionRepresentation = bigIntegerValue.ToWords();

            StringBuilder decimalPortionRepresentation = new StringBuilder();

            while (decimalValue != 0)
            {
                decimalValue *= 10;
                byte currentDigit = Convert.ToByte(Math.Floor(decimalValue));
                decimalPortionRepresentation.Append(singleDigitNames[currentDigit]);
                decimalPortionRepresentation.Append(" ");
                decimalValue -= currentDigit;
            }

            if (decimalPortionRepresentation.Length > 0)
            {
                decimalPortionRepresentation.Insert(0, " point ");
            }

            string fullRepresentation = integerPortionRepresentation + decimalPortionRepresentation.ToString();
            return fullRepresentation.Trim();
        }

        private static string ConvertWholeNumberToWord(dynamic value)
        {
            try
            {
                var valueCopy = value < 0 ? Math.Abs(value) : value;

                if (valueCopy < 10 && valueCopy >= 0)
                {
                    return value >= 0 ? singleDigitNames[(byte)value] : "negative " + singleDigitNames[(byte)value];
                }

                List<short> largeNumberSlices = new List<short>();
                StringBuilder valueAsWords = new StringBuilder();

                while (valueCopy != 0)
                {
                    short threeNumberSlice = (short)(valueCopy % 1000);
                    valueCopy /= 1000;
                    largeNumberSlices.Add(threeNumberSlice);
                }

                for (int i = largeNumberSlices.Count - 1; i >= 0; i--)
                {
                    short slice = largeNumberSlices[i];
                    ConvertNumberSliceToWords(slice, (byte)i, valueAsWords);

                }
                if (value < 0)
                {
                    valueAsWords.Insert(0, "negative ");
                }

                return valueAsWords.ToString().TrimEnd(' ').TrimEnd(',');
            }
            catch (OverflowException)
            {
                BigInteger val = new BigInteger(value);
                return val.ToWords();
            }

        }

        private static void ConvertNumberSliceToWords(short slice, byte sliceGroup, StringBuilder words)
        {
            if (slice == 0)
            {
                return;
            }

            byte[] digits = new byte[3];
            byte digitCount = 2;
            bool hasHundreds = false;
            bool hasTens = false;
            bool hasSpecialTens = false;

            while (slice > 0)
            {
                digits[digitCount] = (byte)(slice % 10);
                slice /= 10;
                digitCount--;
            }

            if (digits[0] != 0)
            {
                words.Append(singleDigitNames[digits[0]]);
                words.Append(" ");
                words.Append(largeNumberNames[0]);
                words.Append(" ");
                hasHundreds = true;
            }
            if (digits[1] != 0)
            {
                if (hasHundreds)
                {
                    words.Append("and");
                    words.Append(" ");
                }
                if (digits[1] == 1)
                {
                    byte val = (byte)(10 + digits[2]);
                    words.Append(specialTensNames[val]);
                    words.Append(" ");
                    hasSpecialTens = true;
                }
                else
                {
                    words.Append(tensNames[digits[1]]);
                    words.Append(" ");
                }
                hasTens = true;
            }
            if (!hasSpecialTens && digits[2] != 0)
            {
                if (hasHundreds && !hasTens)
                {
                    words.Append("and");
                    words.Append(" ");
                }
                words.Append(singleDigitNames[digits[2]]);
                words.Append(" ");
            }

            if (sliceGroup >= 1)
            {
                words.Append(largeNumberNames[sliceGroup]);
                words.Append(", ");
            }
        }
    }
}
