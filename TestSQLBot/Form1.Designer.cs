namespace TestSQLBot
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            button1 = new Button();
            button2 = new Button();
            numericUpDown1 = new NumericUpDown();
            label1 = new Label();
            label2 = new Label();
            panel1 = new Panel();
            button4 = new Button();
            panel2 = new Panel();
            button5 = new Button();
            listBox1 = new ListBox();
            linkLabel1 = new LinkLabel();
            textBox1 = new TextBox();
            label5 = new Label();
            button3 = new Button();
            label3 = new Label();
            openFileDialog1 = new OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // button1
            // 
            button1.BackColor = SystemColors.Control;
            button1.Cursor = Cursors.Hand;
            button1.FlatStyle = FlatStyle.System;
            button1.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            button1.ForeColor = SystemColors.InfoText;
            button1.Location = new Point(35, 66);
            button1.Name = "button1";
            button1.Size = new Size(85, 31);
            button1.TabIndex = 0;
            button1.Text = "START";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Cursor = Cursors.Hand;
            button2.FlatStyle = FlatStyle.System;
            button2.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            button2.Location = new Point(192, 66);
            button2.Name = "button2";
            button2.Size = new Size(85, 31);
            button2.TabIndex = 1;
            button2.Text = "STOP";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // numericUpDown1
            // 
            numericUpDown1.Cursor = Cursors.Hand;
            numericUpDown1.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            numericUpDown1.Location = new Point(189, 133);
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(88, 33);
            numericUpDown1.TabIndex = 2;
            numericUpDown1.ValueChanged += numericUpDown1_ValueChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label1.ForeColor = Color.Black;
            label1.Location = new Point(35, 135);
            label1.Name = "label1";
            label1.Size = new Size(148, 25);
            label1.TabIndex = 3;
            label1.Text = "Номер вопроса";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
            label2.ForeColor = Color.Black;
            label2.Location = new Point(123, 16);
            label2.Name = "label2";
            label2.Size = new Size(60, 32);
            label2.TabIndex = 4;
            label2.Text = "БОТ";
            label2.UseMnemonic = false;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Silver;
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(button4);
            panel1.Controls.Add(button1);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(button2);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(numericUpDown1);
            panel1.Location = new Point(8, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(321, 426);
            panel1.TabIndex = 5;
            // 
            // button4
            // 
            button4.BackColor = Color.LimeGreen;
            button4.Location = new Point(3, 396);
            button4.Name = "button4";
            button4.Size = new Size(25, 25);
            button4.TabIndex = 5;
            button4.UseVisualStyleBackColor = false;
            button4.Click += button4_Click;
            // 
            // panel2
            // 
            panel2.BackColor = Color.Silver;
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.Controls.Add(button5);
            panel2.Controls.Add(listBox1);
            panel2.Controls.Add(linkLabel1);
            panel2.Controls.Add(textBox1);
            panel2.Controls.Add(label5);
            panel2.Controls.Add(button3);
            panel2.Controls.Add(label3);
            panel2.Location = new Point(381, 12);
            panel2.Name = "panel2";
            panel2.Size = new Size(321, 426);
            panel2.TabIndex = 6;
            panel2.Paint += panel2_Paint;
            // 
            // button5
            // 
            button5.AutoSize = true;
            button5.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            button5.Location = new Point(21, 336);
            button5.Name = "button5";
            button5.Size = new Size(280, 35);
            button5.TabIndex = 14;
            button5.Text = "СПИСОК КОМАНД";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // listBox1
            // 
            listBox1.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 25;
            listBox1.Location = new Point(21, 166);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(280, 154);
            listBox1.TabIndex = 13;
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.Location = new Point(3, 406);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(69, 15);
            linkLabel1.TabIndex = 12;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "куда жмать";
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            // 
            // textBox1
            // 
            textBox1.Cursor = Cursors.IBeam;
            textBox1.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            textBox1.Location = new Point(21, 94);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(137, 33);
            textBox1.TabIndex = 11;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            label5.Location = new Point(21, 66);
            label5.Name = "label5";
            label5.Size = new Size(213, 25);
            label5.TabIndex = 10;
            label5.Text = "Количество вопросов:";
            // 
            // button3
            // 
            button3.Cursor = Cursors.Hand;
            button3.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            button3.Location = new Point(164, 94);
            button3.Name = "button3";
            button3.Size = new Size(137, 35);
            button3.TabIndex = 8;
            button3.Text = "ДОБАВИТЬ ФАЙЛ ВОПРОСОВ";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
            label3.ForeColor = Color.Black;
            label3.Location = new Point(95, 16);
            label3.Name = "label3";
            label3.Size = new Size(139, 32);
            label3.TabIndex = 5;
            label3.Text = "Настройка";
            label3.UseMnemonic = false;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(714, 450);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximumSize = new Size(730, 489);
            MinimumSize = new Size(730, 489);
            Name = "Form1";
            Text = "Telegram Bot [v0.1]";
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        private Button button2;
        private NumericUpDown numericUpDown1;
        private Label label1;
        private Label label2;
        private Panel panel1;
        private Panel panel2;
        private Label label3;
        private Button button3;
        private OpenFileDialog openFileDialog1;
        private Label label5;
        private Button button4;
        private TextBox textBox1;
        private LinkLabel linkLabel1;
        private Button button5;
        private ListBox listBox1;
    }
}