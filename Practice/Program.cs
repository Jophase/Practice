using System;
using System.Collections.Generic;

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            var validationResult = ValidateInput(input);

            if (!validationResult.IsValid)
            {
                Console.WriteLine($"Ошибка: введены неподходящие символы: {string.Join(", ", validationResult.InvalidChars)}");
            }
            else
            {
                Console.WriteLine(ProcessString(input));
            }
            Console.ReadLine();
        }


        static (bool IsValid, List<char> InvalidChars) ValidateInput(string input)
        {
            List<char> invalidChars = new List<char>();

            foreach (char c in input)
            {
                if (c < 'a' || c > 'z')
                {
                    invalidChars.Add(c);
                }
            }

            return (invalidChars.Count == 0, invalidChars);
        }


        static string ProcessString(string text)
        {
            if (string.IsNullOrEmpty(text)) return text;

            if (text.Length % 2 == 0)
            {
                int half = text.Length / 2;
                string firstPart = Reverse(text.Substring(0, half));
                string secondPart = Reverse(text.Substring(half));
                return firstPart + secondPart;
            }
            else
            {
                string reversed = Reverse(text);
                return reversed + text;
            }
        }

        static string Reverse(string s)
        {
            char[] arr = s.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }
    }
}