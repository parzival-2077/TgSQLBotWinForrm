using Telegram.Bot;
using Telegram.Bot.Args;
using Microsoft.Data.SqlClient;
using Telegram.Bot.Types;

namespace TestSQLBot
{
    public partial class Form1 : Form
    {
        private static string Token { get; set; } = "5864093331:AAEUBNuFIRlNaXEMlT1AuObkKX7rS2qBzM4"; //токен
        private static TelegramBotClient client;
        private static string connectionString = "Server=(localdb)\\mssqllocaldb;Database=tgServer;Trusted_Connection=True;"; //БД
        SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString);
        public int collumCount = 0;
        private static string nc;
        private bool button1Pressed;

        public Form1()
        {
            InitializeComponent();

        }
        private void button1_Click(object sender, EventArgs e)
        {
            //button1Pressed = true;

             client = new TelegramBotClient(Token);
             client.StartReceiving();
             client.OnMessage += OnMassegeHandler;
             //Console.ReadLine();
             //client.StopReceiving();



        }
        private static async void OnMassegeHandler(object? sender, MessageEventArgs e)
        {
            int collumCount = 0;
            string commMessage = "";
            var msg = e.Message;

            if (msg.Text != null)
            {
               // Console.WriteLine($"new massage {msg.Text}");
            }

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
                                collumCount++;
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

                                // добавление
                                SqlCommand command = new SqlCommand(sqlExpression, connection);
                                int number = await command.ExecuteNonQueryAsync();
                                //Console.WriteLine($"Добавлено ответов  объектов: {number}");
                                collumCount++;
                            }
                        }
                    }

                };
            }

            else if (msg.Text == "/admin")
            {
                //string connectionString = "ваша строка подключения";
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

        private void button2_Click(object sender, EventArgs e)
        {
            client.StopReceiving();
        }


    }

    
}