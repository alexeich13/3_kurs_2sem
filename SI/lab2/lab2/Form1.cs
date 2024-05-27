using System.Text;

namespace lab2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (StreamReader reader = new StreamReader(openFileDialog.FileName))
                    {
                        textBox1.Text = reader.ReadToEnd();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "All files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    byte[] fileBytes = File.ReadAllBytes(openFileDialog.FileName);

                    string base64String = Convert.ToBase64String(fileBytes);

                    string saveFilePath = Path.Combine(Path.GetDirectoryName(openFileDialog.FileName), Path.GetFileNameWithoutExtension(openFileDialog.FileName) + "_base64.txt");

                    File.WriteAllText(saveFilePath, base64String);

                    MessageBox.Show("Файл успешно сконвертирован и сохранен в " + saveFilePath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
        }
        private void AddTextToTextBox(string newText)
        {
            textBox1.Text += newText + Environment.NewLine;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            char[] alphabet = {
                'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm',
                'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'
            };
            string text = File.ReadAllText("C:\\BSTU\\3 kurs\\3 kurs 2 sem\\SI\\lab2\\lab2\\latyn.txt");
            Dictionary<char, double> probabilities = CalculateProbabilities(text.ToLower(), alphabet);

            foreach (var kvp in probabilities)
            {
                AddTextToTextBox($"Буква: {kvp.Key}, Вероятность: {kvp.Value}");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            char[] alphabet = {
                'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm',
                'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'
            };
            string text = File.ReadAllText("C:\\BSTU\\3 kurs\\3 kurs 2 sem\\SI\\lab2\\lab2\\latyn_base64.txt");
            Dictionary<char, double> probabilities = CalculateProbabilities(text.ToLower(), alphabet);

            foreach (var kvp in probabilities)
            {
                AddTextToTextBox($"Буква: {kvp.Key}, Вероятность: {kvp.Value}");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            char[] alphabet = {
                'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm',
                'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'
            };
            string text = File.ReadAllText("C:\\BSTU\\3 kurs\\3 kurs 2 sem\\SI\\lab2\\lab2\\latyn.txt");
            double hartleyEntropy = CalculateHartleyEntropy(text, alphabet);
            double shannonEntropy = CalculateShannonEntropy(text, alphabet);
            double redundency = CalculateRedundency(shannonEntropy, hartleyEntropy);
            string answer1 = "Энтропия Хартли: " + hartleyEntropy.ToString();
            string answer2 = "Энтропия Шеннона: " + shannonEntropy.ToString();
            string answer3 = "Избыточность: " + redundency.ToString();
            AddTextToTextBox(answer1);
            AddTextToTextBox(answer2);
            AddTextToTextBox(answer3);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            char[] alphabet = {
                'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm',
                'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'
            };
            string text = File.ReadAllText("C:\\BSTU\\3 kurs\\3 kurs 2 sem\\SI\\lab2\\lab2\\latyn_base64.txt");
            double hartleyEntropy = CalculateHartleyEntropy(text, alphabet);
            double shannonEntropy = CalculateShannonEntropy(text, alphabet);
            double redundency = CalculateRedundency(shannonEntropy, hartleyEntropy);
            string answer1 = "Энтропия Хартли: " + hartleyEntropy.ToString();
            string answer2 = "Энтропия Шеннона: " + shannonEntropy.ToString();
            string answer3 = "Избыточность: " + redundency.ToString();
            AddTextToTextBox(answer1);
            AddTextToTextBox(answer2);
            AddTextToTextBox(answer3);
        }
        public static Dictionary<char, double> CalculateProbabilities(string text, char[] alphabet)
        {
            int[] charCount = new int[alphabet.Length];
            int textLength = text.Length;

            foreach (char c in text)
            {
                int index = Array.IndexOf(alphabet, c);
                if (index != -1)
                {
                    charCount[index]++;
                }
            }

            Dictionary<char, double> probabilities = new Dictionary<char, double>();

            for (int i = 0; i < alphabet.Length; i++)
            {
                int count = charCount[i];
                double probability = (double)count / textLength;
                probabilities.Add(alphabet[i], probability);
            }

            return probabilities;
        }
        static double CalculateRedundency(double shan, double hart)
        {
            double redundency = ((hart - shan) / hart) * 100;
            return redundency;
        }
        static double CalculateHartleyEntropy(string text, char[] alphabet)
        {
            HashSet<char> symbols = new HashSet<char>(text);
            int numSymbols = symbols.Count;
            return Math.Log(numSymbols, 2);
        }
        static double CalculateShannonEntropy(string text, char[] alphabet)
        {
            Dictionary<char, int> symbolCount = new Dictionary<char, int>();
            foreach (char symbol in text)
            {
                if (symbolCount.ContainsKey(symbol))
                    symbolCount[symbol]++;
                else
                    symbolCount[symbol] = 1;
            }

            int totalSymbols = text.Length;
            double entropy = 0;

            foreach (var count in symbolCount.Values)
            {
                double probability = (double)count / totalSymbols;
                entropy -= probability * Math.Log(probability, 2);
            }
            return entropy;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            UTF8Encoding encoder = new UTF8Encoding();
            byte[] a = encoder.GetBytes("Дрозд");
            byte[] b = encoder.GetBytes("Алексей");
            // Дополняем буферы нулями, если они разного размера
            if (a.Length != b.Length)
            {
                int length = Math.Max(a.Length, b.Length);
                a = a.Concat(Enumerable.Repeat((byte)0, length - a.Length)).ToArray();
                b = b.Concat(Enumerable.Repeat((byte)0, length - b.Length)).ToArray();
            }

            byte[] xorResult = XorBuffers(a, b);
            string xorResultHex = BitConverter.ToString(xorResult).Replace("-", "");
            string answer1 = "Результат операции XOR ASCII (hex): " + xorResultHex;
            string answer2 = "Результат операции XOR base64: " + Convert.ToBase64String(xorResult).ToString();
            AddTextToTextBox(answer1);
            AddTextToTextBox(answer2);
            byte[] aXORbXORb = XorBuffers(xorResult, b);
            string aXORbXORbHex = BitConverter.ToString(aXORbXORb).Replace("-", "");
            string answer3 = "Вычисление aXORbXORb ASCII (hex): " + xorResultHex;
            string answer4 = "Вычисление aXORbXORb base64: " + Convert.ToBase64String(aXORbXORb).ToString();
            AddTextToTextBox(answer3);
            AddTextToTextBox(answer4);
        }
        static byte[] XorBuffers(byte[] a, byte[] b)
        {
            int length = Math.Max(a.Length, b.Length);
            byte[] result = new byte[length];

            for (int i = 0; i < length; i++)
            {
                result[i] = (byte)(a.ElementAtOrDefault(i) ^ b.ElementAtOrDefault(i));
            }

            return result;
        }
    }
}