using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Console.Write("Ввод: ");
        string input = Console.ReadLine();

        // Проверка символов
        var err = new List<char>();
        foreach (char c in input)
            if (c < 'a' || c > 'z') err.Add(c);

        if (err.Count > 0)
        {
            Console.WriteLine($"Ошибка: {string.Join(", ", err)}");
            return;
        }

        // Обработка строки
        string res = input.Length % 2 == 0
            ? Reverse(input[..(input.Length / 2)]) + Reverse(input[(input.Length / 2)..])
            : Reverse(input) + input;

        Console.WriteLine($"Результат: {res}");

        // Подсчёт символов
        var counts = new Dictionary<char, int>();
        foreach (char c in res)
            counts[c] = counts.ContainsKey(c) ? counts[c] + 1 : 1;

        Console.WriteLine("Частота символов:");
        foreach (var c in counts)
            Console.WriteLine($"{c.Key}: {c.Value}");

        // Поиск подстроки
        string max = "";
        for (int i = 0; i < res.Length; i++)
        {
            if (!"aeiouy".Contains(res[i])) continue;
            for (int j = res.Length - 1; j > i; j--)
            {
                if (!"aeiouy".Contains(res[j])) continue;
                if (j - i + 1 > max.Length)
                    max = res.Substring(i, j - i + 1);
                break;
            }
        }

        Console.WriteLine(max == "" ? "Нет подстроки" : $"Макс. подстрока: {max}");
    }

    static string Reverse(string s)
    {
        char[] a = s.ToCharArray();
        Array.Reverse(a);
        return new string(a);
    }
}