﻿using System.Text;
using System.Diagnostics;

namespace lab7
{
    class lab7
    {
        static string[] CreateW1Matrix(string message)
        {
            string[] resultMatrix = new string[message.Count()];
            for (int i = 0; i < message.Count(); i++)
            {
                resultMatrix[i] = message;
                message = message.Substring(1) + message[0];

            }
            return resultMatrix;
        }


        static void ShowMatrix(string[] matrix)
        {

            foreach (var row in matrix)
            {
                Console.WriteLine(row);
            }
            Console.WriteLine();
        }

        static string GetLastMatrixColumn(string[] matrix)
        {
            string lastColumn = "";
            foreach (var row in matrix)
            {
                lastColumn += row[row.Length - 1];
            }
            return lastColumn;
        }

        static int GetInitMessagePlace(string initMessage, string[] matrix)
        {
            int initMessagePlace = -1;
            for (int i = 0; i < matrix.Count(); i++)
            {
                if (matrix[i] == initMessage)
                {
                    return i;
                }
            }
            return initMessagePlace;
        }

        static string[] AddMessageToMatrixFromLeft(string message, string[] matrix)
        {
            for (int i = 0; i < matrix.Length; i++)
            {
                matrix[i] = message[i] + matrix[i];
            }
            return matrix;
        }

        static string[] SortMatrix(string[] matrix)
        {
            return (matrix.OrderBy(x => x).ToArray());
        }

        static string[] CreateDecodingMatrix(string message)
        {
            string[] messageMatrix = new string[message.Length];

            for (int i = 0; i < message.Length; i++)
            {
                messageMatrix = AddMessageToMatrixFromLeft(message, messageMatrix);
                ShowMatrix(messageMatrix);
                messageMatrix = SortMatrix(messageMatrix);
            }
            return messageMatrix;
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            string initialMessageBytes = "экс";
            byte[] bytes = Encoding.ASCII.GetBytes(initialMessageBytes);
            int integ;
            string str = "";
            for (int i = 0; i < bytes.Length; i++)
            {
                integ = bytes[0];
                str += Convert.ToString(integ, 2);

            }
            str = "111011001111001111101011";


            string[] initMessages = new string[] { "ЛЁША", "ДРОЗД", "мультимиллионер", str };

            foreach (string initMessage in initMessages)
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();

                Console.WriteLine("Исходное сообщение: " + initMessage);
                Console.WriteLine();

                string[] W1 = CreateW1Matrix(initMessage);
                Console.WriteLine("W1");
                ShowMatrix(W1);

                string[] W2 = (W1.OrderBy(x => x).ToArray());
                Console.WriteLine("W2");
                ShowMatrix(W2);

                string encodedMessage = GetLastMatrixColumn(W2) + (GetInitMessagePlace(initMessage, W2));
                Console.WriteLine("Сжатое сообщение: " + GetLastMatrixColumn(W2) + (GetInitMessagePlace(initMessage, W2) + 1));
                string[] W2Dec = new string[encodedMessage.Length - (encodedMessage.Length - initMessage.Length)];
                string gettedMessage = encodedMessage.Substring(0, encodedMessage.Length - (encodedMessage.Length - initMessage.Length));
                Console.WriteLine("Сообщение для распаковки: " + gettedMessage + (GetInitMessagePlace(initMessage, W2) + 1));
                Console.WriteLine();

                W2Dec = CreateDecodingMatrix(gettedMessage);
                Console.WriteLine("W2Р");
                ShowMatrix(W2Dec);

                int numberOfInitialMessage = Int32.Parse((encodedMessage.Substring(initMessage.Length, (encodedMessage.Length - initMessage.Length))));
                Console.WriteLine($"Сообщение после распаковки:  " + W2Dec[numberOfInitialMessage]);

                sw.Stop();

            }

        }
    }
}