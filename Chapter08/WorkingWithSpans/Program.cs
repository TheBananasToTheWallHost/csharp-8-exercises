using System;

namespace WorkingWithSpans
{
    class Program
    {
        static void Main(string[] args)
        {
            Index idx = new Index(5, true);

            int[] arr = new int[] {1, 2, 3, 4, 5};

            int val = arr[idx];

            Console.WriteLine($"Accessed {val} at index {idx}");

            string name = "Samantha Jones";

            int lengthOfFirst = name.IndexOf(' ');
            int lengthOfLast = name.Length - lengthOfFirst - 1;

            string firstName = name.Substring(0, lengthOfFirst);
            string lastName = name.Substring(name.Length - lengthOfLast);

            Console.WriteLine($"First name: {firstName}, Last name: {lastName}");

            ReadOnlySpan<char> nameAsSpan = name.AsSpan();
            var firstNameRange = new Range(0, lengthOfFirst);
            var lastNameRange = new Range(^lengthOfLast, ^0);
            var firstNameSpan = nameAsSpan[firstNameRange];
            var lastNameSpane = nameAsSpan[lastNameRange];

            Console.WriteLine("First name: {0}, Last name: {1}", firstNameSpan.ToString(), lastNameSpane.ToString());

            System.Collections.Generic.List<int> ints = new System.Collections.Generic.List<int>(){1, 5, 10, 15, 20, 25, 30, 35, 40, 45};

            var intArr = ints.ToArray();
            Span<int> intsSpan = new Span<int>(intArr);

            for(int i = 0; i < intsSpan.Length; i++){
                if(i % 2 == 0){
                    intsSpan[i] *= 2;
                }
            }

            foreach(var value in intArr){
                Console.WriteLine(value);
            }
        }
    }
}
