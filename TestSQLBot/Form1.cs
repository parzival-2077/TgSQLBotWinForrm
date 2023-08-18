using Telegram.Bot;
using Telegram.Bot.Args;
using Microsoft.Data.SqlClient;
using Telegram.Bot.Types;

namespace TestSQLBot
{

    public partial class Form1 : Form
    {
        private static string Token { get; set; } = "5864093331:AAEUBNuFIRlNaXEMlT1AuObkKX7rS2qBzM4"; //�����
        private static TelegramBotClient client;
        private static string connectionString = "Server=(localdb)\\mssqllocaldb;Database=tgServer;Trusted_Connection=True;"; //��
        SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString);
        private int collumCount = 0;
        private static string nc;


        public Form1()
        {
            InitializeComponent();
            button2.Visible= false;

        }
        private void button1_Click(object sender, EventArgs e)
        {
            //button1Pressed = true;
            button2.Visible = true;
            MessageBox.Show("Start bot");
            client = new TelegramBotClient(Token);
            client.StartReceiving();
            client.OnMessage += OnMassegeHandler;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            button2.Visible = false;
            MessageBox.Show("Stop bot");
            client.StopReceiving();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            collumCount = Convert.ToInt32(numericUpDown1.Value);
        }

        private async void OnMassegeHandler(object? sender, MessageEventArgs e)
        {

            //  int collumCount = 0;
            string commMessage = "";
            var msg = e.Message;
            collumCount = Convert.ToInt32(numericUpDown1.Value);

            if (msg.Text != null)
            {
                // Console.WriteLine($"new massage {msg.Text}");
            }

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
                        if (collumCount == 0)
                        {
                            // ���� �������� ��������� ���������, ��������� ��� � ���� ������
                            commMessage = nextMsg.Text;
                            string sqlExpression = $"INSERT INTO Users (Name,q1,q2,q3,q4) VALUES (N'{commMessage}','0','0','0','0')";
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
                        else if (collumCount <= 4)
                        {
                            string s = $"q{collumCount}";
                            string nextMessage = nextMsg.Text;
                            msg = await client.SendTextMessageAsync(msg.Chat.Id, "OK");
                            string sqlExpression = $"UPDATE  Users SET {s} = N'{nextMessage}' WHERE Name = N'{commMessage}'";
                            // string sqlExpression = $"INSERT INTO Users ({s}) VALUES (N'{nextMessage}' WHERE Name = N'{commMessage}')";
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

            else if (msg.Text == "/admin")
            {
                //string connectionString = "���� ������ �����������";
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
    }


}