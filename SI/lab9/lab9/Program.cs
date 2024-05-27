using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Specialized;

namespace lZ
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Программа сжатия методом Лемпеля-Зива");
                string input = "drozd_alex";
                Console.WriteLine("Исходное сообщение: " + input );

                int dictionaryBuffer = 0;
                int slidingWindowDataBuffer = 0;

                while (dictionaryBuffer == 0)
                {
                    Console.Write("Размер словаря: ");
                    dictionaryBuffer = Convert.ToInt32(Console.ReadLine());
                }

                while (slidingWindowDataBuffer == 0)
                {
                    Console.Write("Размер буфера: ");
                    slidingWindowDataBuffer = Convert.ToInt32(Console.ReadLine());
                }

                string mainDictionary = new string('0', dictionaryBuffer);

                string slidingWindowMessage = input.Substring(0, slidingWindowDataBuffer);

                string afterSlidingWindowMessage = input.Substring(slidingWindowDataBuffer, input.Length - slidingWindowDataBuffer);

                Console.WriteLine("\nСжатие методом Лемпеля-Зива:\n");

                string encodedInput = string.Empty;
                char theNextSymbolWithoutRepeating;

                int indexStartExtention = 0;
                int extentionLength = 0;

                Stopwatch encodeTimer = new Stopwatch();

                encodeTimer.Start();

                while (slidingWindowMessage != string.Empty)
                {
                    Functionality.ToSearchSymbols(mainDictionary, slidingWindowMessage, out indexStartExtention, out extentionLength, out theNextSymbolWithoutRepeating);

                    Functionality.ToAddSymbols(extentionLength + 1, ref mainDictionary, ref slidingWindowMessage);

                    Functionality.toCheckBufferSize(dictionaryBuffer, ref mainDictionary);

                    Functionality.ToAddSymbols(extentionLength + 1, ref slidingWindowMessage, ref afterSlidingWindowMessage);

                    Functionality.toCheckBufferSize(slidingWindowDataBuffer, ref slidingWindowMessage);

                    Console.Write(mainDictionary + "|\t");

                    Console.Write(slidingWindowMessage + "\t|");

                    Console.Write(indexStartExtention + "" + extentionLength + "" + theNextSymbolWithoutRepeating + "\n");

                    encodedInput += indexStartExtention.ToString() + extentionLength.ToString() + theNextSymbolWithoutRepeating.ToString();
                }

                encodeTimer.Stop();

                Console.WriteLine("\nИтоговое сообщение: " + encodedInput + "\n");

                Console.WriteLine("Распаковка\n");

                Stopwatch decodeTimer = new Stopwatch();
                decodeTimer.Start();

                string UnwrapedMessage = string.Empty;
                string tempStr = string.Empty;
                mainDictionary = new string('0', dictionaryBuffer);

                for (int i = 0; i < encodedInput.Length / 3; i++)
                {
                    indexStartExtention = int.Parse(encodedInput[3 * i].ToString());


                    extentionLength = int.Parse(encodedInput[3 * i + 1].ToString());

                    theNextSymbolWithoutRepeating = encodedInput[3 * i + 2];

                    if (extentionLength == 0 && indexStartExtention == 0)
                    {
                        UnwrapedMessage += theNextSymbolWithoutRepeating;
                        mainDictionary += theNextSymbolWithoutRepeating;
                    }

                    else
                    {
                        tempStr = mainDictionary.Substring((indexStartExtention - 1), extentionLength) + theNextSymbolWithoutRepeating;
                        UnwrapedMessage += tempStr;
                        mainDictionary += tempStr;
                    }

                    Functionality.toCheckBufferSize(dictionaryBuffer, ref mainDictionary);

                    Console.Write(indexStartExtention + "" + extentionLength + "" + theNextSymbolWithoutRepeating + "" + "|\t");
                    Console.Write(mainDictionary + "\t|");
                    Console.Write(UnwrapedMessage + "\n");
                }

                decodeTimer.Stop();



                Console.WriteLine($"\nРазмер буфера: {dictionaryBuffer}");
                Console.WriteLine($"Размер словаря: {slidingWindowDataBuffer}");
                Console.WriteLine($"Время сжатия: {encodeTimer.ElapsedMilliseconds} мс");
                Console.WriteLine($"Время распаковки: {decodeTimer.ElapsedMilliseconds} мс");


                Console.WriteLine("\nИсходное сообщение: " + input.Length + " символов");
                Console.WriteLine("Сжатое сообщение: " + encodedInput.Length + " символов");
                double r = (Convert.ToDouble(input.Length) / Convert.ToDouble(encodedInput.Length)) * 100;
                Console.WriteLine($"Эффективность применения метода Лемпеля-Зива составляет: {r}%");

            }

            catch
            {
                Console.WriteLine("Ошибка при выполнении программы. Попробуйте ввести другие значения для буфера.");
            }
        }
    }
    public class Functionality
    {
        public static void ToAddSymbols(int symbolAmount, ref string buffer, ref string slidingWindow)
        {
            if (symbolAmount > slidingWindow.Length)
            {
                symbolAmount = slidingWindow.Length;
            }

            if (symbolAmount > 0)
            {
                buffer += slidingWindow.Substring(0, symbolAmount);
                slidingWindow = slidingWindow.Substring(symbolAmount, slidingWindow.Length - symbolAmount);
            }
        }

        public static void toCheckBufferSize(int size, ref string bufferAfterShift)
        {
            if (bufferAfterShift.Length > size)
            {
                bufferAfterShift = bufferAfterShift.Substring(bufferAfterShift.Length - size, bufferAfterShift.Length - (bufferAfterShift.Length - size));
            }
        }

        public static void ToSearchSymbols(string buffer, string slidingWindow, out int indeedSet, out int coincideLength, out char theNextSymbol)
        {
            coincideLength = 0;

            theNextSymbol = slidingWindow[0];

            indeedSet = 0;



            while (buffer.Contains(slidingWindow.Substring(0, (coincideLength + 1))))
            {


                indeedSet = buffer.IndexOf(slidingWindow.Substring(0, (coincideLength + 1))) + 1;

                coincideLength++;

                if (slidingWindow.Length == coincideLength)
                {
                    theNextSymbol = ' ';
                    break;
                }

                else
                {
                    theNextSymbol = slidingWindow[coincideLength];
                }
            }
        }
    }
}