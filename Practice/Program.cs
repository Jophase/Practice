using System;
using System.Collections.Generic;

public class Program
{
    static void Main()
    {
        Console.Write("Введите строку: ");
        string input = Console.ReadLine();


        List<char> invalidChars = new List<char>();
        foreach (char c in input)
        {
            if (c < 'a' || c > 'z')
            {
                invalidChars.Add(c);
            }
        }


        if (invalidChars.Count > 0)
        {
            Console.WriteLine($"Ошибка: некорректные символы - {string.Join(", ", invalidChars)}");
            return;
        }


        string result = ProcessString(input);
        Console.WriteLine("Результат: " + result);


        Console.WriteLine("\nПовторение символов:");
        Dictionary<char, int> counter = new Dictionary<char, int>();

        foreach (char c in result)
        {
            if (counter.ContainsKey(c))
                counter[c]++;
            else
                counter[c] = 1;
        }

        List<char> sortedKeys = new List<char>(counter.Keys);
        sortedKeys.Sort();

        foreach (char key in sortedKeys)
        {
            Console.WriteLine($"{key} = {counter[key]}");
        }
    }

    static string ProcessString(string text)
    {
        if (text.Length % 2 == 0)
        {
            int half = text.Length / 2;
            return Reverse(text.Substring(0, half)) + Reverse(text.Substring(half));
        }
        else
        {
            char[] arr = text.ToCharArray();
            Array.Reverse(arr);
            return new string(arr) + text;
        }
    }

    static string Reverse(string s)
    {
        char[] arr = s.ToCharArray();
        Array.Reverse(arr);
        return new string(arr);
    }
}