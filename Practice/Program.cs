using System;

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            Console.WriteLine(ProcessString(input));
            Console.ReadLine();
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