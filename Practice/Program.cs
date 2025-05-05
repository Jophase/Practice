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

        // Выбор сортировки
        Console.Write("Выберите сортировку (1 - QuickSort, 2 - TreeSort): ");
        string sortType = Console.ReadLine();

        string sorted = sortType == "1" ? QuickSort(res) :
                       sortType == "2" ? TreeSort(res) :
                       "Неверный выбор";

        Console.WriteLine($"Отсортировано: {sorted}");
    }

    static string Reverse(string s)
    {
        char[] a = s.ToCharArray();
        Array.Reverse(a);
        return new string(a);
    }

    // Быстрая сортировка
    static string QuickSort(string s)
    {
        if (s.Length < 2) return s;
        char pivot = s[s.Length / 2];
        string left = "", mid = "", right = "";
        foreach (char c in s)
        {
            if (c < pivot) left += c;
            else if (c > pivot) right += c;
            else mid += c;
        }
        return QuickSort(left) + mid + QuickSort(right);
    }

    // Сортировка деревом
    static string TreeSort(string s)
    {
        Node root = null;
        foreach (char c in s)
            Insert(ref root, c);
        return Traverse(root);
    }

    class Node
    {
        public char Value;
        public Node Left, Right;
        public Node(char v) => Value = v;
    }

    static void Insert(ref Node node, char c)
    {
        if (node == null) node = new Node(c);
        else if (c <= node.Value) Insert(ref node.Left, c);
        else Insert(ref node.Right, c);
    }

    static string Traverse(Node node)
    {
        if (node == null) return "";
        return Traverse(node.Left) + node.Value + Traverse(node.Right);
    }
}