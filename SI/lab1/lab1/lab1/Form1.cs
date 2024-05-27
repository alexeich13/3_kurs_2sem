using System;
using System.Text;
using System.Xml.Linq;

namespace lab1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        public static (double entropy, Dictionary<char, double> probabilities) Entropy_Shen(string text, char[] alphabet)
        {
            int[] charCount = new int[alphabet.Length];
            int textLength = text.Length;

            for (int i = 0; i < textLength; i++)
            {
                char c = text[i];
                int index = Array.IndexOf(alphabet, c);
                if (index != -1)
                {
                    charCount[index]++;
                }
            }

            double result = 0;
            Dictionary<char, double> probabilities = new Dictionary<char, double>();

            for (int i = 0; i < alphabet.Length; i++)
            {
                int count = charCount[i];
                double probability = (double)count / textLength;
                probabilities.Add(alphabet[i], probability);
                if (probability != 0)
                {
                    result += probability * Math.Log(probability, 2);
                }
            }

            double entropy = -result;
            return (entropy, probabilities);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string text = "jackdaws love my big sphinx of quartz. crazy fredericka bought many very exquisite opal jewels";
            char[] alphabet = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o',
            'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };


            var (entropy, probabilities) = Entropy_Shen(text, alphabet);
            string answer = "Энтропия итальянского: " + entropy.ToString();
            AddTextToTextBox(answer);
            foreach (var pair in probabilities)
            {
                AddTextToTextBox($"Буква: {pair.Key}, Вероятность: {pair.Value}");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textbox.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string text = "в году летом боярышник зацвел, дожди идут, жара кончилась, кипит лужа, моросит роса, солнце тает, утро волосы высушило, вечер дубак охватил, ночь пугает, ягода замёрзла, съезд, йфэющ";
            char[] alphabet = { 'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о',
            'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я' };


            var (entropy, probabilities) = Entropy_Shen(text, alphabet);
            string answer = "Энтропия монгольского: " + entropy.ToString();
            AddTextToTextBox(answer);
            foreach (var pair in probabilities)
            {
                AddTextToTextBox($"Буква: {pair.Key}, Вероятность: {pair.Value}");
            }
        }
        private void AddTextToTextBox(string newText)
        {
            textbox.Text += newText + Environment.NewLine;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string text = "jackdaws love my big sphinx of quartz. crazy fredericka bought many very exquisite opal jewels";
            char[] alphabet = { '0', '1' };
            string res = Binary(text);

            double entropy = Entropy_She(res, alphabet);
            string answer = "Энтропия итальянского bin: " + entropy.ToString();
            AddTextToTextBox(answer);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string text = "в году летом боярышник зацвел, дожди идут, жара кончилась, кипит лужа, моросит роса, солнце тает, утро волосы высушило, вечер дубак охватил, ночь пугает, ягода замёрзла, съезд, йфэющ";
            char[] alphabet = { '0', '1' };
            string res = Binary(text);

            double entropy = Entropy_She(res, alphabet);
            string answer = "Энтропия монгольского bin: " + entropy.ToString();
            AddTextToTextBox(answer);
        }
        public static string Binary(string text)
        {
            byte[] buf = Encoding.ASCII.GetBytes(text);
            char[] binaryChars = new char[buf.Length * 8];
            int index = 0;
            foreach (byte b in buf)
            {
                int bitIndex = 7;
                while (bitIndex >= 0)
                {
                    binaryChars[index] = ((b >> bitIndex) & 1) == 1 ? '1' : '0';
                    index++;
                    bitIndex--;
                }
            }
            string binaryStr = new string(binaryChars);
            return binaryStr;
        }
        public static double Entropy_She(string text, char[] alphabet)
        {
            int[] charCount = new int[alphabet.Length];
            int textLength = text.Length;
            for (int i = 0; i < textLength; i++)
            {
                char c = text[i];
                int index = Array.IndexOf(alphabet, c);
                if (index != -1)
                {
                    charCount[index]++;
                }
            }
            double result = 0;
            for (int i = 0; i < alphabet.Length; i++)
            {
                int count = charCount[i];
                double probability = (double)count / textLength;
                if (textLength != 0 && probability != 0)
                {
                    result += probability * Math.Log(probability, 2);
                }
            }
            double entropy = -result;
            return entropy;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string text = "drozd aleksei igorevich";
            string ascii = "100 114 111 122 100 32 97 108 101 107 115 101 105 32 105 103 111 114 101 118 105 99 104";
            double entr = 4.020448977346836;
            double entrbin = 0.9975290595157313;
            double numberlet = CountInfo(text, entr);
            double numberbin = CountInfo(ascii, entrbin);
            string answer = "Кол-во информации итальянского: " + numberlet.ToString() + '\n' + " Кол-во информации ASCII bin: " + numberbin.ToString();
            AddTextToTextBox(answer);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string text = "дрозд алексей игоревич";
            string ascii = "208 180 209 128 208 190 208 183 208 180 32 208 176 208 187 208 181 208 186 209 129 208 181 208 185 32 208 184 208 179 208 190 209 128 208 181 208 178 208 184 209 135";
            double entr = 3.9178372589081163;
            double entrbin = 0.9442903891943717;
            double numberlet = CountInfo(text, entr);
            double numberbin = CountInfo(ascii, entrbin);
            string answer = "Кол-во информации монгольского: " + numberlet.ToString() + '\n' + " Кол-во информации ASCII bin: " + numberbin.ToString();
            AddTextToTextBox(answer);
        }
        static double CountInfo(string text, double entropy)
        {
            return text.Length * entropy;
        }
        public double countInformationWithMistake(double entropy, string text, double p)
        {
            double q = 1 - p;
            double result = 0;
            result = -p * Math.Log(p, 2) - q * Math.Log(q, 2);
            return text.Length * (1 - result);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            double first = 0.1;
            double second = 0.5;
            double third = 1;
            string text = "drozd aleksei igorevich";
            string ascii = "100 114 111 122 100 32 97 108 101 107 115 101 105 32 105 103 111 114 101 118 105 99 104";
            double entr = 4.020448977346836;
            double entrbin = 0.9975290595157313;
            double res1 = countInformationWithMistake(entr, text, first);
            double res2 = countInformationWithMistake(entr, text, second);
            double res3 = countInformationWithMistake(entr, text, third);
            double res11 = countInformationWithMistake(entrbin, ascii, first);
            double res22 = countInformationWithMistake(entrbin, ascii, second);
            double res33 = countInformationWithMistake(entrbin, ascii, third);
            string answer1 = "Кол-во информации итальянского 0.1: " + res1.ToString() + '\n' + " Кол-во информации ASCII bin 0.1: " + res11.ToString();
            string answer2 = "Кол-во информации итальянского 0.5: " + res2.ToString() + '\n' + " Кол-во информации ASCII bin 0.5: " + res22.ToString();
            string answer3 = "Кол-во информации итальянского 1: 0" +'\n' + " Кол-во информации ASCII bin 1: 0";
            AddTextToTextBox(answer1);
            AddTextToTextBox(answer2);
            AddTextToTextBox(answer3);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            double first = 0.1;
            double second = 0.5;
            double third = 1;
            string text = "дрозд алексей игоревич";
            string ascii = "208 180 209 128 208 190 208 183 208 180 32 208 176 208 187 208 181 208 186 209 129 208 181 208 185 32 208 184 208 179 208 190 209 128 208 181 208 178 208 184 209 135";
            double entr = 3.9178372589081163;
            double entrbin = 0.9442903891943717;
            double res1 = countInformationWithMistake(entr, text, first);
            double res2 = countInformationWithMistake(entr, text, second);
            double res3 = countInformationWithMistake(entr, text, third);
            double res11 = countInformationWithMistake(entrbin, ascii, first);
            double res22 = countInformationWithMistake(entrbin, ascii, second);
            double res33 = countInformationWithMistake(entrbin, ascii, third);
            string answer1 = "Кол-во информации монгольского 0.1: " + res1.ToString() + '\n' + " Кол-во информации ASCII bin 0.1: " + res11.ToString();
            string answer2 = "Кол-во информации монгольского 0.5: " + res2.ToString() + '\n' + " Кол-во информации ASCII bin 0.5: " + res22.ToString();
            string answer3 = "Кол-во информации монгольского 1: 0" + '\n' + " Кол-во информации ASCII bin 1: 0";
            AddTextToTextBox(answer1);
            AddTextToTextBox(answer2);
            AddTextToTextBox(answer3);
        }
    }
}