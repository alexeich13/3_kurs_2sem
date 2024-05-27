using System;
using System.Diagnostics;
using System.Text;

namespace lab10
{
    public class lab10
    {
        // Метод для вывода информации об эффективности сжатия
        public static void CalculateCompressionEfficiency(string compressed, string original)
        {
            int compressedLength = Math.Min(compressed.Length, 4);
            double efficiency = (1 - (double)compressedLength / original.Length) * 100;
            Console.WriteLine($"\nЭффективность сжатия арифметическим методом: {efficiency:F2}%");
        }

        static void Main(string[] args)
        {
            {
                Console.OutputEncoding = Encoding.UTF8;

                string word = "мультимиллионер";
                int wordLength = word.Length;
                Compressor wordCompressor = new Compressor();

                wordCompressor.Build(word);
                Console.WriteLine("Анализ слова 'мультимиллионер':\n");

                // Вывод вероятностей символов
                Console.WriteLine("Вероятности:\n");
                foreach (var i in wordCompressor.Nodes)
                {
                    Console.WriteLine($"{i.Symbol}: {i.High - i.Low:F5}");
                }

                // Вывод интервалов символов
                Console.WriteLine("\nИнтервалы:\n");
                foreach (var i in wordCompressor.Nodes)
                {
                    Console.WriteLine($"{i.Symbol}: {i.Low:F5} - {i.High:F5}");
                }
                Console.WriteLine();

                // Измерение времени сжатия
                Stopwatch compressionTimer = new Stopwatch();
                compressionTimer.Start();

                var compressResult = wordCompressor.Compress(word);
                compressionTimer.Stop();

                Console.WriteLine("\nСжатие данных:");
                Console.WriteLine(InfoString.Sb.ToString());
                Console.WriteLine($"Результат сжатия:\n{compressResult}\n");

                // Измерение времени распаковки
                Stopwatch decompressionTimer = new Stopwatch();
                decompressionTimer.Start();
                var decompressResult = wordCompressor.Decompress(compressResult, wordLength, wordLength / 2 + 1);
                decompressionTimer.Stop();

                Console.WriteLine("Распаковка данных:");
                Console.WriteLine(InfoString.Sb.ToString());
                Console.WriteLine($"Результат распаковки:\n{decompressResult}");

                // Проверка на переполнение при сжатии
                CheckCompressionOverflow(compressResult);
                CalculateCompressionEfficiency(compressResult.ToString(), word);

                Console.WriteLine($"\nВремя сжатия: {compressionTimer.ElapsedMilliseconds} мс");
                Console.WriteLine($"Время распаковки: {decompressionTimer.ElapsedMilliseconds} мс");
            }

            Console.WriteLine("\n\n");

            {
                string word = "мультимиллионерсеменохранилще";
                int wordLength = word.Length;
                Compressor wordCompressor = new Compressor();

                wordCompressor.Build(word);
                Console.WriteLine("Анализ слова 'мультимиллионерсеменохранилще':\n");

                // Вывод вероятностей символов
                Console.WriteLine("Вероятности:\n");
                foreach (var i in wordCompressor.Nodes)
                {
                    Console.WriteLine($"{i.Symbol}: {i.High - i.Low:F5}");
                }

                // Вывод интервалов символов
                Console.WriteLine("\nИнтервалы:\n");
                foreach (var i in wordCompressor.Nodes)
                {
                    Console.WriteLine($"{i.Symbol}: {i.Low:F5} - {i.High:F5}");
                }

                var compressResult = wordCompressor.Compress(word);
                Console.WriteLine("\nСжатие данных:\n");
                Console.WriteLine(InfoString.Sb.ToString());
                Console.WriteLine($"Результат сжатия:\n{compressResult}\n");

                var decompressResult = wordCompressor.Decompress(compressResult, wordLength, wordLength / 2 + 1);
                Console.WriteLine("Распаковка данных:\n");
                Console.WriteLine(InfoString.Sb.ToString());
                Console.WriteLine($"Результат распаковки:\n{decompressResult}");

                CheckCompressionOverflow(compressResult);
                CalculateCompressionEfficiency(compressResult.ToString(), word);
            }

            Console.ReadLine();
        }

        // Проверка на переполнение при сжатии
        public static void CheckCompressionOverflow(decimal compressResult)
        {
            int maxBits = (int)Math.Log(int.MaxValue, 2) + 1;
            int bitsNeeded = Decimal.GetBits(compressResult).Length;
            if (bitsNeeded > maxBits)
            {
                Console.WriteLine("Возможно переполнение при сжатии данных.");
            }
            else
            {
                Console.WriteLine("Переполнение при сжатии данных маловероятно.");
            }
        }
    }

    public class Compressor
    {
        public List<Node> Nodes { get; set; }
        public Dictionary<char, decimal> Frequencies { get; set; }
        public Node ResultNode { get; set; }

        public void Build(string source)
        {
            Nodes = new List<Node>();
            decimal inc = 1 / (decimal)source.Length;
            Frequencies = new Dictionary<char, decimal>();
            for (int i = 0; i < source.Length; i++)
            {
                if (!Frequencies.ContainsKey(source[i]))
                {
                    Frequencies.Add(source[i], 0);
                }
                Frequencies[source[i]] += inc;
            }
            Frequencies = Frequencies.OrderBy(x => x.Value).ToDictionary(x => x.Key, y => y.Value);
            decimal low = 0;
            foreach (var item in Frequencies)
            {
                Nodes.Add(new Node { Symbol = item.Key, Low = Math.Round(low, 5), High = Math.Round(low + item.Value, 5) });
                low += item.Value;
            }
        }

        public decimal Compress(string source)
        {
            InfoString.Sb = new StringBuilder();
            ResultNode = new Node { Symbol = '*', High = 1, Low = 0 };
            foreach (var item in source)
            {
                decimal oldHigh = ResultNode.High;
                decimal oldLow = ResultNode.Low;
                InfoString.Sb.Append(ResultNode.ToString()).Append(Environment.NewLine);
                ResultNode.Symbol = '*';
                ResultNode.High = oldLow + (oldHigh - oldLow) * Nodes.Find(x => x.Symbol == item).High;
                ResultNode.Low = oldLow + (oldHigh - oldLow) * Nodes.Find(x => x.Symbol == item).Low;
            }
            InfoString.Sb.Append(ResultNode.ToString()).Append(Environment.NewLine);
            return ResultNode.Low;
        }

        public string Decompress(decimal compress, int leng, int t)
        {
            StringBuilder sb = new StringBuilder();
            InfoString.Sb = new StringBuilder();
            for (int i = 0; i < leng; i++)
            {
                char symbol = Nodes.Find(x => Math.Round(compress, t) >= x.Low && Math.Round(compress, t) < x.High).Symbol;
                InfoString.Sb.Append(compress.ToString() + $"\t-- {symbol}").Append(Environment.NewLine);
                sb.Append(symbol);
                Node tempNode = Nodes.Find(x => x.Symbol == symbol);
                compress = (compress - tempNode.Low) / (tempNode.High - tempNode.Low);
            }
            return sb.ToString();
        }
    }

    public class Node
    {
        public char Symbol { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }

        public override string ToString()
        {
            return string.Format("Low: {0:F25} | High: {1:F25}", Low, High);
        }
    }

    public class InfoString
    {
        public static StringBuilder Sb { get; set; }
    }
}
