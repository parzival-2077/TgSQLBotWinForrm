using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Excel = Microsoft.Office.Interop.Excel;

namespace TestSQLBot
{
    public partial class Form2 : Form
    {
        string name;
        string connString = @"Server=(localdb)\mssqllocaldb;Database=tgServer;Trusted_Connection=True;";
        string q;
        public string textBoxValue
        
        {
            get { return textBox1.Text; }
            set { textBox1.Text = value; }
        }
        public int numberUpDownValue

        {
            get { return Convert.ToInt32(numericUpDown1.Value); }
            set { numericUpDown1.Value = value; }
        }

        public Form2()
        {
            InitializeComponent();

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                $"Количество вопросов {numericUpDown1.Text}?",
                "Сообщение",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
            {
                int quest = Convert.ToInt32(numericUpDown1.Value);
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    await connection.OpenAsync();

                    SqlCommand command = new SqlCommand();
                    command.CommandText = $"CREATE TABLE {name}(Id INT            IDENTITY (1, 1) NOT NULL,Name NVARCHAR (100) NULL, PRIMARY KEY CLUSTERED ([Id] ASC))";
                    command.Connection = connection;
                    await command.ExecuteNonQueryAsync();

                    // Console.WriteLine("Таблица Users создана");
                }
                for (int i = 1; i <= quest; i++)
                {
                    q = "q";
                    q = q + Convert.ToString(i);
                    using (SqlConnection connection = new SqlConnection(connString))
                    {
                        await connection.OpenAsync();

                        SqlCommand command = new SqlCommand();
                        command.CommandText = $"ALTER TABLE {name} ADD {q} NVARCHAR(20) DEFAULT ((0)) NULL";
                        command.Connection = connection;
                        await command.ExecuteNonQueryAsync();

                        // Console.WriteLine("Таблица Users создана");
                    }

                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            name = textBox1.Text;
        }
    }
}
