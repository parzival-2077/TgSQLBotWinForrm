using Telegram.Bot;
using Telegram.Bot.Args;
using Microsoft.Data.SqlClient;
using Telegram.Bot.Types;
using System.Threading;

namespace TestSQLBot
{

    public partial class Form1 : Form
    {
        private static string Token { get; set; } = "5864093331:AAEUBNuFIRlNaXEMlT1AuObkKX7rS2qBzM4"; //токен
        private static TelegramBotClient client;
        private static string connectionString = "Server=(localdb)\\mssqllocaldb;Database=tgServer;Trusted_Connection=True;"; //БД
        SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString);
        private int collumCount = 0;
        private static string nc;
        public int create = 0;


        public Form1()
        {
            InitializeComponent();
            button2.Visible = false;
            openFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            button4.BackColor = Color.Red;

        }
        private void button1_Click(object sender, EventArgs e)
        {

            button2.Visible = true;
            MessageBox.Show("Start bot");
            button4.BackColor = Color.LimeGreen;
            client = new TelegramBotClient(Token);
            client.StartReceiving();
            client.OnMessage += OnMassegeHandler;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.Visible = false;
            MessageBox.Show("Stop bot");
            button4.BackColor = Color.Red;
            client.StopReceiving();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            collumCount = Convert.ToInt32(numericUpDown1.Value);
        }

        private async void OnMassegeHandler(object? sender, MessageEventArgs e)
        {
            string commMessage = "";
            var msg = e.Message;
            collumCount = Convert.ToInt32(numericUpDown1.Value);

            if (msg.Text != null) { }

            if (msg.Text == "/start")
            {
                msg = await client.SendTextMessageAsync(msg.Chat.Id, "Напишите /new:");
            }

            if (msg.Text == "/new")
            {
                //Console.WriteLine(collumCount);

                msg = await client.SendTextMessageAsync(msg.Chat.Id, "Введите назавание команды:");

                // обработка следующего сообщения
                client.OnMessage += async (object sender, MessageEventArgs e) =>
                {

                    var nextMsg = e.Message;
                    if (nextMsg.Text != null)
                    {
                        //Console.WriteLine($"Новое сообщение: {nextMsg.Text}");
                        if (collumCount == 0)
                        {
                            // если получено следующее сообщение, добавляем его в базу данных
                            commMessage = nextMsg.Text;
                            string sqlExpression = $"INSERT INTO Users (Name,q1,q2,q3,q4) VALUES (N'{commMessage}','0','0','0','0')";
                            using (SqlConnection connection = new SqlConnection(connectionString))
                            {
                                await connection.OpenAsync();

                                // добавление
                                SqlCommand command = new SqlCommand(sqlExpression, connection);
                                int number = await command.ExecuteNonQueryAsync();
                                //Console.WriteLine($"Добавлено объектов: {number}");
                                //collumCount++;
                            }

                        }
                        else if (collumCount <= 4)
                        {
                            string s = $"q{collumCount}";
                            string nextMessage = nextMsg.Text;
                            msg = await client.SendTextMessageAsync(msg.Chat.Id, "OK");
                            string sqlExpression = $"UPDATE  Users SET {s} = N'{nextMessage}' WHERE Name = N'{commMessage}'";
                            using (SqlConnection connection = new SqlConnection(connectionString))
                            {
                                await connection.OpenAsync();
                                // добавление
                                SqlCommand command = new SqlCommand(sqlExpression, connection);
                                int number = await command.ExecuteNonQueryAsync();
                                //Console.WriteLine($"Добавлено ответов  объектов: {number}");
                                //collumCount++;
                            }
                        }
                    }
                };
            }

            else if (msg.Text == "/admin")
            {
                string query = "SELECT * FROM Users"; // ваш SQL-запрос

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Получение значений полей каждой строки
                                int id = reader.GetInt32(0);
                                string name = reader.GetString(1);
                                // продолжайте для остальных полей
                                msg = await client.SendTextMessageAsync(msg.Chat.Id, $" Name: {name}");
                                // Вывод данных на консоль
                                //Console.WriteLine($"ID: {id}, Name: {name}");
                            }
                        }
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            // получаем выбранный файл
            string filename = openFileDialog1.FileName;
            listBox1.Items.Clear();
            using (System.IO.StreamReader sr = new System.IO.StreamReader(filename))
            {
                while (!sr.EndOfStream)
                {
                    listBox1.Items.Add(sr.ReadLine());
                }
            }
            label4.Text = listBox1.Items.Count.ToString();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            create++;
            if (create == 3)
            {
                MessageBox.Show("Автор идеи: Курбатов parzival_2077 Алексей \n" +
                                "Альфа тестировщики: Катаев Иван, Светлова Ирина, Бойправ Вадим \n" +
                                "Идейный душнила: Павлов Андрей\n" +
                                "Технические консультанты: Шишкина Марина, Мартьянов Максим, Тимонов Алексей\n"+
                                "Год релиза - 2023", "Пасхалка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                create = 0;
            }
        }
    }
}