using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

class Program
{
    static readonly HttpClient client = new HttpClient();

    static async Task Main()
    {
        Console.Write("Введите строку: ");
        string input = Console.ReadLine();

        // Проверка на допустимые символы
        var errorChars = new List<char>();
        foreach (char c in input)
        {
            if (c < 'a' || c > 'z') errorChars.Add(c);
        }

        if (errorChars.Count > 0)
        {
            Console.WriteLine($"Ошибка: Недопустимые символы - {string.Join(", ", errorChars)}");
            return;
        }

        // Обработка строки
        string processed = ProcessString(input);
        Console.WriteLine($"\nОбработанная строка: {processed}");

        // Подсчёт символов
        var charCount = new Dictionary<char, int>();
        foreach (char c in processed)
        {
            if (charCount.ContainsKey(c)) charCount[c]++;
            else charCount[c] = 1;
        }

        Console.WriteLine("\nЧастота символов:");
        foreach (var pair in charCount)
        {
            Console.WriteLine($"'{pair.Key}': {pair.Value}");
        }

        // Поиск максимальной подстроки с гласными
        string maxSubstring = FindMaxVowelSubstring(processed);
        Console.WriteLine($"\nМаксимальная подстрока: {(maxSubstring == "" ? "не найдена" : maxSubstring)}");

        // Выбор и выполнение сортировки
        Console.Write("\nВыберите сортировку (1 - QuickSort, 2 - TreeSort): ");
        string choice = Console.ReadLine();

        string sorted = choice switch
        {
            "1" => QuickSort(processed),
            "2" => TreeSort(processed),
            _ => "Неверный выбор сортировки"
        };
        Console.WriteLine($"\nОтсортированная строка: {sorted}");

        // Удаление случайного символа
        if (processed.Length > 0)
        {
            int indexToRemove = await GetRandomIndexAsync(processed.Length);
            string trimmed = processed.Remove(indexToRemove, 1);
            Console.WriteLine($"\nУрезанная строка: {trimmed} (удалён символ на позиции {indexToRemove})");
        }
    }

    static async Task<int> GetRandomIndexAsync(int maxLength)
    {
        try
        {
            var response = await client.GetAsync(
                $"http://www.randomnumberapi.com/api/v1.0/random?min=0&max={maxLength - 1}");

            var json = await response.Content.ReadAsStringAsync();
            var numbers = JsonSerializer.Deserialize<int[]>(json);

            return numbers?.Length > 0 ? numbers[0] : new Random().Next(0, maxLength);
        }
        catch
        {
            return new Random().Next(0, maxLength);
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
            return Reverse(text) + text;
        }
    }

    static string Reverse(string s)
    {
        char[] arr = s.ToCharArray();
        Array.Reverse(arr);
        return new string(arr);
    }

    static string FindMaxVowelSubstring(string s)
    {
        string vowels = "aeiouy";
        string max = "";

        for (int start = 0; start < s.Length; start++)
        {
            if (!vowels.Contains(s[start])) continue;

            for (int end = s.Length - 1; end > start; end--)
            {
                if (!vowels.Contains(s[end])) continue;

                int length = end - start + 1;
                if (length > max.Length)
                {
                    max = s.Substring(start, length);
                }
                break;
            }
        }
        return max;
    }

    static string QuickSort(string s)
    {
        if (s.Length <= 1) return s;

        char pivot = s[s.Length / 2];
        string less = "", equal = "", greater = "";

        foreach (char c in s)
        {
            if (c < pivot) less += c;
            else if (c == pivot) equal += c;
            else greater += c;
        }

        return QuickSort(less) + equal + QuickSort(greater);
    }

    static string TreeSort(string s)
    {
        var root = new TreeNode(s[0]);
        for (int i = 1; i < s.Length; i++)
        {
            InsertIntoTree(root, s[i]);
        }
        return TraverseTree(root);
    }

    class TreeNode
    {
        public char Value;
        public TreeNode Left, Right;
        public TreeNode(char val) => Value = val;
    }

    static void InsertIntoTree(TreeNode node, char c)
    {
        if (c <= node.Value)
        {
            if (node.Left == null) node.Left = new TreeNode(c);
            else InsertIntoTree(node.Left, c);
        }
        else
        {
            if (node.Right == null) node.Right = new TreeNode(c);
            else InsertIntoTree(node.Right, c);
        }
    }

    static string TraverseTree(TreeNode node)
    {
        if (node == null) return "";
        return TraverseTree(node.Left) + node.Value + TraverseTree(node.Right);
    }
}