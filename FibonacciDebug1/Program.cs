using System;
using System.Collections.Generic;
using System.Linq;
/// Быков и Денис. 3ИСИП323 группа

class Program
{
    /// шифрование методом ADFGVX
    static char[] symbols = { 'A', 'D', 'F', 'G', 'V', 'X' };

    static void Main()
    {
        string key = "SECRET";
        string keyword = "KEYWORD";
        string text = "HOUSE";

        char[,] matrix = GenerateMatrix(keyword);

        Console.WriteLine("Матрица:");
        PrintMatrix(matrix);

        string encrypted = Encrypt(text, matrix, key);
        Console.WriteLine("Зашифровано: " + encrypted);
        if (!text.All(char.IsLetterOrDigit))
        {
            Console.WriteLine("Ошибка: только буквы и цифры!");
            return;
        }
    }

  
    static char[,] GenerateMatrix(string keyword)
    {
        string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        string unique = "";

        foreach (char c in keyword + alphabet)
        {
            if (!unique.Contains(c))
                unique += c;
        }

        char[,] matrix = new char[6, 6];
        int index = 0;

        for (int i = 0; i < 6; i++)
            for (int j = 0; j < 6; j++)
                matrix[i, j] = unique[index++];

        return matrix;
    }

   
    static void PrintMatrix(char[,] matrix)
    {
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
                Console.Write(matrix[i, j] + " ");
            Console.WriteLine();
        }
    }

    static string Encrypt(string text, char[,] matrix, string key)
    {
        string result = "";

    
        foreach (char c in text)
        {
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    if (matrix[i, j] == c)
                    {
                        result += symbols[i].ToString() + symbols[j];
                    }
                }
            }
        }

       
        int cols = key.Length;
        int rows = (int)Math.Ceiling((double)result.Length / cols);

        char[,] table = new char[rows, cols];
        int k = 0;

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (k < result.Length)
                    table[i, j] = result[k++];
                else
                    table[i, j] = 'X';
            }
        }

     
        var order = key
            .Select((c, i) => new { c, i })
            .OrderBy(x => x.c)
            .ToList();

        string final = "";

        foreach (var item in order)
        {
            for (int i = 0; i < rows; i++)
                final += table[i, item.i];
        }

        return final;
    }
}
