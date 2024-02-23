using Telegram.Bot;
using Telegram.Bot.Args;
using Microsoft.Data.SqlClient;
using Telegram.Bot.Types;
using System.Threading;
using System.Diagnostics;
using Microsoft.Office.Interop.Excel;
using System.Data;

namespace TestSQLBot
{

    public partial class Form1 : Form
    {
        private static string Token { get; set; } = "5864093331:AAEUBNuFIRlNaXEMlT1AuObkKX7rS2qBzM4"; //�����
        private static TelegramBotClient client;
        private static string connectionString = "Server=(localdb)\\mssqllocaldb;Database=tgServer;Trusted_Connection=True;"; //��
        SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString);
        private int collumCount = 0;//������� ��������
        private int collumCount1 = 0;//������� ��������
        private static string nc;
        public int create = 0;//������� ��� ��������
        private int collumStop = 0;//������������ ��������� 
        public bool mood = false;
        private Form2 F2 = new Form2();
        string tableName;

        Microsoft.Office.Interop.Excel.Application xlApp;
        Microsoft.Office.Interop.Excel.Worksheet xlSheet;
        Microsoft.Office.Interop.Excel.Range xlSheetRange;



        public Form1()
        {
            InitializeComponent();
            panel1.Visible = false;//���� ����� "���"
            button2.Visible = false;//������ ���� 
            //openFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*"; //�������� ������������ ��� �������� �������� �� � ��� �� ����� 
            button4.BackColor = Color.Red;//������ �������

        }
        private void button1_Click(object sender, EventArgs e) //������ ����� 
        {

            button2.Visible = true;
            MessageBox.Show("Start bot");
            button4.BackColor = Color.LimeGreen;
            client = new TelegramBotClient(Token);//���������� ����� 
            client.StartReceiving();//�������� 
            client.OnMessage += OnMassegeHandler;//����� ��������� 

        }

        private void button2_Click(object sender, EventArgs e) //������ ����
        {
            button2.Visible = false;
            MessageBox.Show("Stop bot");
            button4.BackColor = Color.Red;
            client.StopReceiving();//������� ����
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)//������������ ��������
        {
            collumCount = Convert.ToInt32(numericUpDown1.Value);//�������� 
            if (collumCount > collumStop)//�������� �� ������ 
            {
                MessageBox.Show("�������� ������������� �������� ������, ��� ����� ��������",
                "������",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
                numericUpDown1.Value = collumStop;
            }


        }

        private async void OnMassegeHandler(object? sender, MessageEventArgs e) //��� ����� �� ������ � ����������� 
        {

            string commMessage = "";
            var msg = e.Message;//���������� 

            collumCount = Convert.ToInt32(numericUpDown1.Value);//����� ������ 

            if (msg.Text != null) { }

            if (msg.Text == "/start")
            {
                msg = await client.SendTextMessageAsync(msg.Chat.Id, "�������� /new:");
            }

            if (msg.Text == "/new")
            {
                //Console.WriteLine(collumCount);

                msg = await client.SendTextMessageAsync(msg.Chat.Id, "������� ��������� �������:");

                // ��������� ���������� ���������
                client.OnMessage += async (object sender, MessageEventArgs e) =>
                {

                    var nextMsg = e.Message;
                    if (nextMsg.Text != null)
                    {
                        //Console.WriteLine($"����� ���������: {nextMsg.Text}");
                        if (collumCount == 0)//���������� �������� ������� � ������� NAME 
                        {
                            // ���� �������� ��������� ���������, ��������� ��� � ���� ������
                            commMessage = nextMsg.Text;
                            string sqlExpression = $"INSERT INTO {tableName} (Name,q1,q2,q3,q4) VALUES (N'{commMessage}','0','0','0','0')";
                            using (SqlConnection connection = new SqlConnection(connectionString))
                            {
                                await connection.OpenAsync();

                                // ����������
                                SqlCommand command = new SqlCommand(sqlExpression, connection);
                                int number = await command.ExecuteNonQueryAsync();
                                //Console.WriteLine($"��������� ��������: {number}");
                                //collumCount++;
                            }

                        }
                        else if (collumCount <= collumStop) //���������� ������� ������������ 
                        {
                            string s = $"q{collumCount}";
                            string nextMessage = nextMsg.Text;
                            msg = await client.SendTextMessageAsync(msg.Chat.Id, "OK");
                            string sqlExpression = $"UPDATE  {tableName} SET {s} = N'{nextMessage}' WHERE Name = N'{commMessage}'";
                            using (SqlConnection connection = new SqlConnection(connectionString))
                            {
                                await connection.OpenAsync();
                                // ����������
                                SqlCommand command = new SqlCommand(sqlExpression, connection);
                                int number = await command.ExecuteNonQueryAsync();
                                //Console.WriteLine($"��������� �������  ��������: {number}");
                                //collumCount++;
                            }
                        }
                    }
                };
            }

            else if (msg.Text == "/admin")//����� ������ 
            {
                string query = $"SELECT * FROM {tableName}"; // ��� SQL-������

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // ��������� �������� ����� ������ ������
                                int id = reader.GetInt32(0);
                                string name = reader.GetString(1);
                                // ����������� ��� ��������� �����
                                msg = await client.SendTextMessageAsync(msg.Chat.Id, $" Name: {name}");
                                // ����� ������ �� �������
                                //Console.WriteLine($"ID: {id}, Name: {name}");
                            }
                        }
                    }
                }
            }


        }
        private async void button3_Click(object sender, EventArgs e)//���� ���������� ��������
        {
            F2.ShowDialog();
            tableName = F2.textBoxValue;
            collumStop = F2.numberUpDownValue;
            panel1.Visible = true;

            /*DialogResult result = MessageBox.Show(
                $"���������� �������� {textBox1.Text}?",
                "���������",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
            {
                collumStop = Convert.ToInt32(textBox1.Text);
                panel1.Visible = true;

            }
            string sqlExpression = "TRUNCATE TABLE Users";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                // ����������
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                int number = await command.ExecuteNonQueryAsync();
            }*/

            //MessageBox.Show(
            //    "������� ����� ������",
            //    "������",
            //    MessageBoxButtons.OK,
            //    MessageBoxIcon.Stop,
            //    MessageBoxDefaultButton.Button1
            //);

            //if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
            //    return;
            //// �������� ��������� ����
            //string filename = openFileDialog1.FileName;
            ////listBox1.Items.Clear();
            //using (System.IO.StreamReader sr = new System.IO.StreamReader(filename))
            //{
            //    while (!sr.EndOfStream)
            //    {
            //        //listBox1.Items.Add(sr.ReadLine());
            //    }
            //}
            //label4.Text = listBox1.Items.Count.ToString();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)//��������
        {
            create++;
            if (create == 3)
            {
                MessageBox.Show("����� ����: �������� parzival_2077 ������� \n" +
                                "����� ������������: ������ ����, �������� �����, ������� ����� \n" +
                                "������� �������: ������ ������\n" +
                                "����������� ������������: ������� ������, ��������� ������, ������� �������\n" +
                                "��� ������ - 2023", "��������", MessageBoxButtons.OK, MessageBoxIcon.Information);
                create = 0;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)//�������� �� ��� 
        {
            System.Diagnostics.Process.Start(new ProcessStartInfo { FileName = "https://github.com/parzival-2077/TgSQLBotWinForrm", UseShellExecute = true });
        }

        private void button5_Click(object sender, EventArgs e)//��������� ������ �� ������� ������ �������� ������ 
        {
            listBox1.Items.Clear();
            string query = "SELECT * FROM Users"; // ��� SQL-������

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // ��������� �������� ����� ������ ������
                            int id = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            listBox1.Items.Add(name + "\n");

                        }
                    }
                }
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            mood = true;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            mood = false;
        }
        private System.Data.DataTable GetData()
        {
            //������ ����������


            SqlConnection con = new SqlConnection(connectionString);

            System.Data.DataTable dt = new System.Data.DataTable();
            try
            {
                string query = $"SELECT * FROM {tableName}";
                SqlCommand comm = new SqlCommand(query, con);

                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(comm);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
            return dt;
        }
        private void button6_Click(object sender, EventArgs e)
        {
            xlApp = new Microsoft.Office.Interop.Excel.Application();

            try
            {
                //��������� �����
                xlApp.Workbooks.Add(Type.Missing);

                //������ �������� ���������� ��������
                xlApp.Interactive = false;
                xlApp.EnableEvents = false;

                //�������� ���� �� ������� ����� �������� (���� 1)
                xlSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlApp.Sheets[1];
                //�������� �����
                xlSheet.Name = "������";

                //�������� ������
                System.Data.DataTable dt = GetData();

                int collInd = 0;
                int rowInd = 0;
                string data = "";

                //�������� �������
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    data = dt.Columns[i].ColumnName.ToString();
                    xlSheet.Cells[1, i + 1] = data;

                    //�������� ������ ������
                    xlSheetRange = xlSheet.get_Range("A1:Z1", Type.Missing);

                    //������ ���������� ����� � ������� ����
                    xlSheetRange.WrapText = true;
                    xlSheetRange.Font.Bold = true;
                }

                //��������� ������
                for (rowInd = 0; rowInd < dt.Rows.Count; rowInd++)
                {
                    for (collInd = 0; collInd < dt.Columns.Count; collInd++)
                    {
                        data = dt.Rows[rowInd].ItemArray[collInd].ToString();
                        xlSheet.Cells[rowInd + 2, collInd + 1] = data;
                    }
                }

                //�������� ��� ������� ������
                xlSheetRange = xlSheet.UsedRange;

                //����������� ������ � ������� �� �� �����������
                xlSheetRange.Columns.AutoFit();
                xlSheetRange.Rows.AutoFit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                //���������� ������
                xlApp.Visible = true;

                xlApp.Interactive = true;
                xlApp.ScreenUpdating = true;
                xlApp.UserControl = true;

                //������������� �� Excel
                releaseObject(xlSheetRange);
                releaseObject(xlSheet);
                releaseObject(xlApp);
            }
        }
        void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show(ex.ToString(), "������!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                GC.Collect();
            }
        }
    }
}