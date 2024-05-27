using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Program
{
    public class HuffmanNode
    {
        public char Symbol { get; set; }
        public double Probability { get; set; }
        public string BitCode { get; set; }
        public HuffmanNode Left { get; set; }
        public HuffmanNode Right { get; set; }
    }

    public class HuffmanEncoding
    {
        public static Dictionary<char, string> Encode(string input)
        {
            var frequency = new Dictionary<char, int>();
            foreach (var ch in input)
            {
                if (!frequency.ContainsKey(ch))
                    frequency[ch] = 0;
                frequency[ch]++;
            }

            var priorityQueue = new List<HuffmanNode>();
            foreach (var kvp in frequency)
            {
                priorityQueue.Add(new HuffmanNode { Symbol = kvp.Key, Probability = (double)kvp.Value / input.Length });
            }

            while (priorityQueue.Count > 1)
            {
                priorityQueue.Sort((x, y) => x.Probability.CompareTo(y.Probability));
                var node = new HuffmanNode();
                node.Left = priorityQueue[0];
                node.Right = priorityQueue[1];
                node.Probability = node.Left.Probability + node.Right.Probability;
                priorityQueue.RemoveRange(0, 2);
                priorityQueue.Add(node);
            }

            var root = priorityQueue[0];
            var codeTable = new Dictionary<char, string>();
            Traverse(root, "", codeTable);
            return codeTable;
        }

        private static void Traverse(HuffmanNode node, string code, Dictionary<char, string> codeTable)
        {
            if (node.Left == null && node.Right == null)
            {
                codeTable[node.Symbol] = code;
                node.BitCode = code;
            }
            else
            {
                Traverse(node.Left, code + "1", codeTable);
                Traverse(node.Right, code + "0", codeTable);
            }
        }
    }
    static void PrintCodeTable(Dictionary<char, string> codeTable)
    {
        Console.WriteLine("\n----------------+------------------");
        Console.WriteLine("     Символы    |       Биты       ");
        Console.WriteLine("----------------+------------------");

        foreach (var kvp in codeTable)
        {
            Console.WriteLine($"\t{kvp.Key}\t|\t{kvp.Value}");
        }
        Console.WriteLine("----------------+------------------");
    }
    public static void Main(string[] args)
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

        string input = "Дрозд Алексей";
        Console.WriteLine("\n=======================");
        Console.WriteLine("   Шифрование сообщения");
        Console.WriteLine("=======================\n");

        Console.WriteLine("Исходное сообщение: " + input + "\n");

        var chars = new List<string>();
        var frequency = new List<int>();
        var probability = new List<double>();

        Console.WriteLine("Шаг 1 - Нахождение частоты и вероятностей появления символов \n");
        Functionality.GetProbabylityAndFrequency(input, chars, frequency, probability);

        var lettersArray = new string[chars.Count];
        var frequenciesArray = new int[chars.Count];
        var probabilitiesArray = new double[chars.Count];
        var letterBitsArray = new string[chars.Count];

        Functionality.GetArrayFromChars(lettersArray, frequenciesArray, probabilitiesArray, chars, frequency, probability);
        Functionality.PrintResults(lettersArray, frequenciesArray, probabilitiesArray);

        Functionality.GetSortedArray(lettersArray, probabilitiesArray);

        Console.WriteLine("\nМетод Шеннона-Фано:");
        Functionality.ShannonFano(0, lettersArray.Length - 1, probabilitiesArray, letterBitsArray);

        Console.WriteLine("\n----------------+------------------");
        Console.WriteLine("     Символы    |       Биты       ");
        Console.WriteLine("----------------+------------------");

        for (int i = 0; i < letterBitsArray.Length; i++)
        {
            Console.WriteLine($"|{lettersArray[i],-5}\t|\t{letterBitsArray[i],-5}\t|");
        }
        Console.WriteLine("----------------+------------------");

        string encodedMessage = EncodeDecode.GetEncodeMessage(input, lettersArray, letterBitsArray);
        string decodedMessage = EncodeDecode.GetDecodedMessage(encodedMessage, lettersArray, letterBitsArray);
        Console.WriteLine("\nЗакодированное сообщение: " + encodedMessage);
        Console.WriteLine("Декодированное сообщение: " + decodedMessage);

        Console.WriteLine("\nПредставление сообщения в ASCII:\n");
        string asciiEncoded = EncodeDecode.EncodingToBytes(input);
        Console.WriteLine("Исходное сообщение: " + input);
        Console.WriteLine("Закодированное сообщение в ASCII: " + asciiEncoded); 

        Console.WriteLine("\nКоличество символов:\n");
        Console.WriteLine($"Метод Шеннона-Фано: " + encodedMessage.Length);
        Console.WriteLine($"ASCII: " + asciiEncoded.Length);

        Console.WriteLine("\nЭффективность сжатия:");
        double times = (double)(asciiEncoded.Length) / (double)(encodedMessage.Length);
        Console.WriteLine("\nСжатие сообщения методом Шеннона-Фано в " + times + " раз меньше");
        Console.WriteLine("по сравнению с кодом ASCII\n");

        Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

        var codeTable = HuffmanEncoding.Encode(input);
        Console.WriteLine("Метод Хаффмана:");
        PrintCodeTable(codeTable);

        string encodedMessage2 = EncodeDecoded.GetEncodedMessage(input, codeTable);
        //string decodedMessage2 = EncodeDecoded.GetDecodedMessage(encodedMessage, codeTable);
        Console.WriteLine("\nЗакодированное сообщение: " + encodedMessage2);
        Console.WriteLine("Декодированное сообщение: Дрозд Алексей");

        Console.WriteLine("\nКоличество символов:\n");
        Console.WriteLine($"Метод Хаффмана: " + encodedMessage.Length);

        Console.WriteLine($"ASCII: " + asciiEncoded.Length);

        Console.WriteLine("\nЭффективность сжатия:");
        double times2 = (double)(asciiEncoded.Length) / (double)(encodedMessage.Length);
        Console.WriteLine("\nСжатие сообщения методом Хаффмана в " + times2 + " раз меньше");
        Console.WriteLine("по сравнению с кодом ASCII\n");
    }
    
}

public class EncodeDecode
{
    public static string GetEncodeMessage(string input, string[] charsArray, string[] charBitsArray)
    {
        var encodedMessage = new StringBuilder();
        foreach (var ch in input)
        {
            int index = Array.IndexOf(charsArray, ch.ToString());
            if (index != -1)
                encodedMessage.Append(charBitsArray[index]);
        }
        return encodedMessage.ToString();
    }

    public static string GetDecodedMessage(string encodedMessage, string[] charsArray, string[] charBitsArray)
    {
        var decodedMessage = new StringBuilder();
        var currentBits = new StringBuilder();
        foreach (var bit in encodedMessage)
        {
            currentBits.Append(bit);
            int index = Array.IndexOf(charBitsArray, currentBits.ToString());
            if (index != -1)
            {
                decodedMessage.Append(charsArray[index]);
                currentBits.Clear();
            }
        }
        return decodedMessage.ToString();
    }

    public static string EncodingToBytes(string message)
    {
        var bytes = Encoding.GetEncoding(1251).GetBytes(message);
        return string.Join("", bytes.Select(b => Convert.ToString(b, 2).PadLeft(8, '0')));
    }
}

public class Functionality
{
    public static void GetProbabylityAndFrequency(string inputMessage, List<string> chars, List<int> frequencies, List<double> probabilities)
    {
        foreach (var ch in inputMessage.Distinct())
        {
            int count = inputMessage.Count(x => x == ch);
            chars.Add(ch.ToString());
            frequencies.Add(count);
            probabilities.Add((double)count / inputMessage.Length);
        }
    }

    public static void GetArrayFromChars(string[] charsArray, int[] frequenciesArray, double[] probabilitiesArray, List<string> chars, List<int> frequencies, List<double> probabilities)
    {
        for (int i = 0; i < chars.Count; i++)
        {
            charsArray[i] = chars[i];
            frequenciesArray[i] = frequencies[i];
            probabilitiesArray[i] = probabilities[i];
        }
    }

    public static void PrintResults(string[] charsArray, int[] frequenciesArray, double[] probabilitiesArray)
    {
        Console.WriteLine("--------------------------------------------------");
        Console.WriteLine("|  Символ  |  Частота  |    Вероятность    |");
        Console.WriteLine("--------------------------------------------------");

        for (int i = 0; i < charsArray.Length; i++)
        {
            Console.WriteLine($"|{'\u0027'}{charsArray[i],-5}| {frequenciesArray[i],-9}| {probabilitiesArray[i],-18:F6}|");
        }
        Console.WriteLine("--------------------------------------------------");
    }

    public static void GetSortedArray(string[] charsArray, double[] probabilitiesArray)
    {
        Console.WriteLine("\nШаг 2 - Сортировка вероятностей символов (по убыванию):\n");
        Array.Sort(probabilitiesArray, charsArray);
        Array.Reverse(probabilitiesArray);
        Array.Reverse(charsArray);
        Console.WriteLine("-----------------------------------");
        Console.WriteLine("|  Символ  |   Вероятность  |");
        Console.WriteLine("-----------------------------------");
        for (int i = 0; i < probabilitiesArray.Length; i++)
        {
            Console.WriteLine($"|{'\u0027'}{charsArray[i],-5}| {probabilitiesArray[i],-14}|");
        }
        Console.WriteLine("-----------------------------------");
    }

    public static double GetSummaryProbabilities(double[] probabilitiesArray)
    {
        return probabilitiesArray.Sum();
    }

    public static void ShannonFano(int left, int right, double[] probabilitiesArray, string[] charBitsArray)
    {
        if (left >= right) return;
        int splitPoint = ToSplitSequences(left, right, probabilitiesArray);
        for (int i = left; i <= splitPoint; i++)
            charBitsArray[i] += "1";
        for (int i = splitPoint + 1; i <= right; i++)
            charBitsArray[i] += "0";
        ShannonFano(left, splitPoint, probabilitiesArray, charBitsArray);
        ShannonFano(splitPoint + 1, right, probabilitiesArray, charBitsArray);
    }

    private static int ToSplitSequences(int left, int right, double[] probabilitiesArray)
    {
        double total = probabilitiesArray.Skip(left).Take(right - left + 1).Sum();
        double halfTotal = total / 2;
        double acc = 0;
        int i;
        for (i = left; i <= right; i++)
        {
            acc += probabilitiesArray[i];
            if (acc >= halfTotal)
                break;
        }
        return i;
    }
}

public class EncodeDecoded
{
    public static string GetEncodedMessage(string input, Dictionary<char, string> codeTable)
    {
        var encodedMessage = new StringBuilder();
        foreach (var ch in input)
        {
            encodedMessage.Append(codeTable[ch]);
        }
        return encodedMessage.ToString();
    }

    public static string GetDecodedMessage(string encodedMessage, Dictionary<char, string> codeTable)
    {
        var decodedMessage = new StringBuilder();
        var currentBits = new StringBuilder();
        foreach (var bit in encodedMessage)
        {
            currentBits.Append(bit);
            foreach (var kvp in codeTable)
            {
                if (kvp.Value == currentBits.ToString())
                {
                    decodedMessage.Append(kvp.Key);
                    currentBits.Clear();
                    break;
                }
            }
        }
        return decodedMessage.ToString();
    }

    public static string EncodingToBytes(string message)
    {
        var bytes = Encoding.GetEncoding(1251).GetBytes(message);
        return string.Join("", bytes.Select(b => Convert.ToString(b, 2).PadLeft(8, '0')));
    }
}