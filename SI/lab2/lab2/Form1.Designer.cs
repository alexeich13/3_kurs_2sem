namespace lab2
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            button1 = new Button();
            button3 = new Button();
            textBox1 = new TextBox();
            button4 = new Button();
            label2 = new Label();
            button2 = new Button();
            button5 = new Button();
            button6 = new Button();
            button7 = new Button();
            label3 = new Label();
            button8 = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(152, 37);
            label1.Name = "label1";
            label1.Size = new Size(79, 20);
            label1.TabIndex = 0;
            label1.Text = "Задание 1";
            // 
            // button1
            // 
            button1.Location = new Point(21, 81);
            button1.Name = "button1";
            button1.Size = new Size(147, 62);
            button1.TabIndex = 1;
            button1.Text = "Вывести данные файлов";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button3
            // 
            button3.Location = new Point(241, 81);
            button3.Name = "button3";
            button3.Size = new Size(147, 62);
            button3.TabIndex = 3;
            button3.Text = "Конвертировать файл .txt";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(603, 34);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(670, 629);
            textBox1.TabIndex = 4;
            // 
            // button4
            // 
            button4.Location = new Point(1279, 63);
            button4.Name = "button4";
            button4.Size = new Size(136, 62);
            button4.TabIndex = 5;
            button4.Text = "Очистить вывод";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(152, 180);
            label2.Name = "label2";
            label2.Size = new Size(79, 20);
            label2.TabIndex = 6;
            label2.Text = "Задание 2";
            // 
            // button2
            // 
            button2.Location = new Point(21, 213);
            button2.Name = "button2";
            button2.Size = new Size(147, 68);
            button2.TabIndex = 7;
            button2.Text = "Распределение частотных свойств .txt";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button5
            // 
            button5.Location = new Point(241, 213);
            button5.Name = "button5";
            button5.Size = new Size(147, 68);
            button5.TabIndex = 8;
            button5.Text = "Распределение частотных свойств base64";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // button6
            // 
            button6.Location = new Point(21, 313);
            button6.Name = "button6";
            button6.Size = new Size(147, 68);
            button6.TabIndex = 9;
            button6.Text = "Энтропии и избыточность .txt";
            button6.UseVisualStyleBackColor = true;
            button6.Click += button6_Click;
            // 
            // button7
            // 
            button7.Location = new Point(241, 313);
            button7.Name = "button7";
            button7.Size = new Size(147, 68);
            button7.TabIndex = 10;
            button7.Text = "Энтропии и избыточность base64";
            button7.UseVisualStyleBackColor = true;
            button7.Click += button7_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(152, 413);
            label3.Name = "label3";
            label3.Size = new Size(79, 20);
            label3.TabIndex = 11;
            label3.Text = "Задание 3";
            // 
            // button8
            // 
            button8.Location = new Point(122, 456);
            button8.Name = "button8";
            button8.Size = new Size(147, 68);
            button8.TabIndex = 12;
            button8.Text = "XOR";
            button8.UseVisualStyleBackColor = true;
            button8.Click += button8_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1427, 735);
            Controls.Add(button8);
            Controls.Add(label3);
            Controls.Add(button7);
            Controls.Add(button6);
            Controls.Add(button5);
            Controls.Add(button2);
            Controls.Add(label2);
            Controls.Add(button4);
            Controls.Add(textBox1);
            Controls.Add(button3);
            Controls.Add(button1);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button button1;
        private Button button3;
        private TextBox textBox1;
        private Button button4;
        private Label label2;
        private Button button2;
        private Button button5;
        private Button button6;
        private Button button7;
        private Label label3;
        private Button button8;
    }
}