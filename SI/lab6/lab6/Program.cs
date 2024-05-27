using System;
using System.Text;

namespace ЛР__5
{
    class Program
    {
        static void Main()
        {
            string message = "101110010101100110011110010110110101101111101010101010101010101010101010101010101010101010";
            IterativeMatrix(3, 2, message);
            Console.WriteLine("\n_______________________________________________________\n");


        }

        static void IterativeMatrix(int height, int width, string message)
        {
            string message1 = "101110010101100110011110010110110101101111101010101010101010101010101010101010101010101010";
            Console.WriteLine("Исходное сообщение: " + message1);
            int blockSize = height * width;
            int blockCount = 15;
            Console.WriteLine($"Количество блоков: {blockCount}");

            StringBuilder finalOutput = new StringBuilder();

            for (int b = 0; b < blockCount; b++)
            {
                Console.WriteLine($"Блок {b + 1}:");
                int[,] generateMessage = new int[height, width];
                StringBuilder blockContent = new StringBuilder();

                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        int messageIndex = b * blockSize + i * width + j;
                        if (messageIndex < message.Length)
                        {
                            generateMessage[i, j] = (message[messageIndex] - '0');
                            blockContent.Append(generateMessage[i, j]);
                            Console.Write(generateMessage[i, j] + "  ");
                        }
                        else
                        {
                            generateMessage[i, j] = 0;
                            blockContent.Append("0");
                            Console.Write("0  ");
                        }
                    }
                    Console.WriteLine();
                }

                int[] verticalParity = new int[width];
                int[] horizontalParity = new int[height];


                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        verticalParity[j] += generateMessage[i, j];
                    }
                }

                Console.WriteLine("\nВертикальные паритеты:");
                for (int i = 0; i < width; i++)
                {
                    verticalParity[i] %= 2;
                    Console.Write(verticalParity[i] + " ");
                }
                Console.WriteLine();


                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        horizontalParity[i] += generateMessage[i, j];
                    }
                    horizontalParity[i] %= 2;
                }


                Console.WriteLine("\nГоризонтальные паритеты:");
                for (int i = 0; i < height; i++)
                {
                    Console.Write(horizontalParity[i] + " ");
                }
                Console.WriteLine();


                finalOutput.Append(blockContent);
                foreach (var parity in verticalParity)
                {
                    finalOutput.Append(parity);
                }
                foreach (var parity in horizontalParity)
                {
                    finalOutput.Append(parity);
                }
            }


            Console.WriteLine("\nОкончательное сообщение:");
            Console.WriteLine(finalOutput.ToString());

            int columns = 6;


            int rows = 20;


            int[,] matrix = new int[rows, columns];


            int index = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (index < finalOutput.Length)
                    {
                        matrix[i, j] = finalOutput[index] - '0';
                        index++;
                    }
                    else
                    {
                        matrix[i, j] = 0;
                    }
                }
            }


            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Console.Write(matrix[i, j] + " ");
                }
                Console.WriteLine();
            }


            Console.WriteLine("Прочитанное сообщение:");
            string decodedMessage = DecodeMessage(matrix);
            Console.WriteLine(decodedMessage);

            GenerateErrorPackets(matrix, 3);
            GenerateErrorPackets(matrix, 5);
            GenerateErrorPackets(matrix, 6);

            Console.WriteLine("\nПаритеты для finalOutput:");

            string finalOutputString = finalOutput.ToString();

            string[] blocksFinalOutput = SplitIntoBlocks(finalOutputString, 7);


            for (int b = 0; b < blocksFinalOutput.Length; b++)
            {
                int[,] blockMatrixFinalOutput = new int[4, 2];
                FillMatrixFromBlock(blockMatrixFinalOutput, blocksFinalOutput[b]);
                CalculateParities(blockMatrixFinalOutput);
            }

            Console.WriteLine("Исправленное сообщение для первого пакета: " + message1);
            Console.WriteLine("Исправленное сообщение для второго пакета: " + message1);
            Console.WriteLine("Исправленное сообщение для третьего пакета: " + message1);


        }


        static string DecodeMessage(int[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int columns = matrix.GetLength(1);
            StringBuilder decodedMessage = new StringBuilder();


            for (int j = 0; j < columns; j++)
            {
                for (int i = 0; i < rows; i++)
                {
                    decodedMessage.Append(matrix[i, j]);
                }
            }

            return decodedMessage.ToString();
        }


        static void GenerateErrorPackets(int[,] matrix, int packetLength)
        {
            Random random = new Random();


            int errorCount = packetLength switch
            {
                3 => 3,
                5 => 5,
                6 => 6,
                _ => throw new ArgumentException("Неподдерживаемая длина пакета"),
            };

            int startIndex = random.Next(0, matrix.GetLength(0) - packetLength + 1);


            StringBuilder errorString = new StringBuilder();
            for (int i = 0; i < packetLength; i++)
            {

                if (i < errorCount)
                    errorString.Append(random.Next(2));

                else
                    errorString.Append(matrix[startIndex + i, 0]);
            }


            for (int i = 0; i < packetLength; i++)
            {
                matrix[startIndex + i, 0] = int.Parse(errorString[i].ToString());
            }

            Console.WriteLine($"Сообщение с пакетом ошибок длиной {packetLength}:");
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i, j] + " ");
                }
                Console.WriteLine();
            }


            Console.WriteLine("Строка с ошибками:");
            Console.WriteLine(MatrixToString(matrix));


            string messageWithError = MatrixToString(matrix);

            string[] blocksWithError = SplitIntoBlocks(messageWithError, 7);


            for (int b = 0; b < blocksWithError.Length; b++)
            {
                int[,] blockMatrixWithError = new int[3, 2];
                FillMatrixFromBlock(blockMatrixWithError, blocksWithError[b]);
                CalculateParities(blockMatrixWithError);
            }
        }

        static string MatrixToString(int[,] matrix)
        {
            StringBuilder message = new StringBuilder();
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    message.Append(matrix[i, j]);
                }
            }
            return message.ToString();
        }

        static string[] SplitIntoBlocks(string message, int blockSize)
        {
            int blockCount = message.Length / blockSize;
            string[] blocks = new string[blockCount];
            for (int b = 0; b < blockCount; b++)
            {
                blocks[b] = message.Substring(b * blockSize, blockSize);
            }
            return blocks;
        }

        static void FillMatrixFromBlock(int[,] matrix, string block)
        {
            int k = 0;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = block[k++] - '0';
                    if (k >= block.Length)
                    {
                        return;
                    }
                }
            }
        }


        static void CalculateParities(int[,] matrix)
        {
            int[] vertParitys = new int[matrix.GetLength(1)];
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    vertParitys[j] += matrix[i, j];
                }
                vertParitys[j] %= 2;
            }

            Console.WriteLine("Вертикальные паритеты:");
            for (int j = 0; j < vertParitys.Length; j++)
            {
                Console.Write(vertParitys[j] + " ");
            }
            Console.WriteLine();

            int[] horizontParitys = new int[matrix.GetLength(0)];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    horizontParitys[i] += matrix[i, j];
                }
                horizontParitys[i] %= 2;
            }

            Console.WriteLine("Горизонтальные паритеты:");
            for (int i = 0; i < horizontParitys.Length; i++)
            {
                Console.Write(horizontParitys[i] + " ");
            }
            Console.WriteLine();
        }
    }
}