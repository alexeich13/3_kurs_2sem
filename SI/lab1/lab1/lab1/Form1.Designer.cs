namespace lab1
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
            button2 = new Button();
            textbox = new TextBox();
            button3 = new Button();
            button4 = new Button();
            button5 = new Button();
            label2 = new Label();
            button6 = new Button();
            button7 = new Button();
            label3 = new Label();
            button8 = new Button();
            button9 = new Button();
            label4 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(183, 28);
            label1.Name = "label1";
            label1.Size = new Size(81, 20);
            label1.TabIndex = 0;
            label1.Text = "Задание А";
            label1.Click += label1_Click;
            // 
            // button1
            // 
            button1.Location = new Point(12, 69);
            button1.Name = "button1";
            button1.Size = new Size(183, 29);
            button1.TabIndex = 1;
            button1.Text = "Энтропия итальянского";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(254, 69);
            button2.Name = "button2";
            button2.Size = new Size(206, 29);
            button2.TabIndex = 2;
            button2.Text = "Энтропия монгольского";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // textbox
            // 
            textbox.Location = new Point(656, 28);
            textbox.Multiline = true;
            textbox.Name = "textbox";
            textbox.ScrollBars = ScrollBars.Vertical;
            textbox.Size = new Size(610, 568);
            textbox.TabIndex = 3;
            // 
            // button3
            // 
            button3.Location = new Point(1307, 79);
            button3.Name = "button3";
            button3.Size = new Size(94, 29);
            button3.TabIndex = 4;
            button3.Text = "Очистить";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.Location = new Point(269, 194);
            button4.Name = "button4";
            button4.Size = new Size(217, 29);
            button4.TabIndex = 7;
            button4.Text = "Энтропия монгольского bin";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button5
            // 
            button5.Location = new Point(12, 194);
            button5.Name = "button5";
            button5.Size = new Size(211, 29);
            button5.TabIndex = 6;
            button5.Text = "Энтропия итальянского bin";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(183, 153);
            label2.Name = "label2";
            label2.Size = new Size(80, 20);
            label2.TabIndex = 5;
            label2.Text = "Задание Б";
            // 
            // button6
            // 
            button6.Location = new Point(254, 317);
            button6.Name = "button6";
            button6.Size = new Size(192, 71);
            button6.TabIndex = 10;
            button6.Text = "Количество информации монгольского";
            button6.UseVisualStyleBackColor = true;
            button6.Click += button6_Click;
            // 
            // button7
            // 
            button7.Location = new Point(12, 317);
            button7.Name = "button7";
            button7.Size = new Size(183, 71);
            button7.TabIndex = 9;
            button7.Text = "Количество информации итальянского";
            button7.UseVisualStyleBackColor = true;
            button7.Click += button7_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(183, 276);
            label3.Name = "label3";
            label3.Size = new Size(80, 20);
            label3.TabIndex = 8;
            label3.Text = "Задание В";
            // 
            // button8
            // 
            button8.Location = new Point(254, 461);
            button8.Name = "button8";
            button8.Size = new Size(206, 51);
            button8.TabIndex = 13;
            button8.Text = "Ошибочная вероятность монгольского";
            button8.UseVisualStyleBackColor = true;
            button8.Click += button8_Click;
            // 
            // button9
            // 
            button9.Location = new Point(12, 461);
            button9.Name = "button9";
            button9.Size = new Size(183, 68);
            button9.TabIndex = 12;
            button9.Text = "Ошибочная вероятность итальянского";
            button9.UseVisualStyleBackColor = true;
            button9.Click += button9_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(183, 420);
            label4.Name = "label4";
            label4.Size = new Size(78, 20);
            label4.TabIndex = 11;
            label4.Text = "Задание Г";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1444, 617);
            Controls.Add(button8);
            Controls.Add(button9);
            Controls.Add(label4);
            Controls.Add(button6);
            Controls.Add(button7);
            Controls.Add(label3);
            Controls.Add(button4);
            Controls.Add(button5);
            Controls.Add(label2);
            Controls.Add(button3);
            Controls.Add(textbox);
            Controls.Add(button2);
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
        private Button button2;
        private TextBox textbox;
        private Button button3;
        private Button button4;
        private Button button5;
        private Label label2;
        private Button button6;
        private Button button7;
        private Label label3;
        private Button button8;
        private Button button9;
        private Label label4;
    }
}