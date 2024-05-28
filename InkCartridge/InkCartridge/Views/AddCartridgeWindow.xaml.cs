using Microsoft.Data.Sqlite;
using System;
using System.Windows;

namespace InkCartridge.Views.Fonts
{
    public partial class AddCartridgeWindow : Window
    {
        public AddCartridgeWindow()
        {
            InitializeComponent();
            LoadTypes();
            LoadStatuses();
        }

        // Метод для загрузки типов картриджей из базы данных
        private void LoadTypes()
        {
            // Создание и открытие соединения с базой данных
            using (var connection = new SqliteConnection("Data Source=cartridges.db"))
            {
                connection.Open();

                // Создание команды SQL для получения всех типов картриджей
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM TypeCartridge";

                // Выполнение команды и обработка результатов
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Создание объекта TypeCartridge и добавление его в ComboBox
                        var type = new TypeCartridge
                        {
                            IdType = reader.GetInt32(0),
                            NameType = reader.GetString(1)
                        };

                        TypeComboBox.Items.Add(type);
                    }
                }
            }
        }

        // Метод для загрузки статусов картриджей из базы данных
        private void LoadStatuses()
        {
            // Создание и открытие соединения с базой данных
            using (var connection = new SqliteConnection("Data Source=cartridges.db"))
            {
                connection.Open();

                // Создание команды SQL для получения всех статусов картриджей
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Status";

                // Выполнение команды и обработка результатов
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Создание объекта Status и добавление его в ComboBox
                        var status = new Status
                        {
                            IdStatus = reader.GetInt32(0),
                            NameStatus = reader.GetString(1)
                        };

                        StatusComboBox.Items.Add(status);
                    }
                }
            }
        }

        // Обработчик события нажатия кнопки добавления картриджа
        private void AddCartridgeButton_Click(object sender, RoutedEventArgs e)
        {
            // Получение данных из полей ввода
            var model = ModelTextBox.Text;
            var serialNumber = SerialNumberTextBox.Text;
            var installationDate = InstallationDatePicker.SelectedDate ?? DateTime.Now;
            var type = (TypeCartridge)TypeComboBox.SelectedItem;
            var status = (Status)StatusComboBox.SelectedItem;
            var comment = CommentTextBox.Text;

            // Создание и открытие соединения с базой данных
            using (var connection = new SqliteConnection("Data Source=cartridges.db"))
            {
                connection.Open();

                // Создание команды SQL для добавления нового картриджа
                var command = connection.CreateCommand();
                command.CommandText = @"
                    INSERT INTO Cartridges (IdType, Model, SerialNumber, InstallationDate, IdStatus, Comment)
                    VALUES (@idType, @model, @serialNumber, @installationDate, @idStatus, @comment)";

                // Добавление параметров в команду SQL
                command.Parameters.AddWithValue("@idType", type.IdType);
                command.Parameters.AddWithValue("@model", model);
                command.Parameters.AddWithValue("@serialNumber", serialNumber);
                command.Parameters.AddWithValue("@installationDate", installationDate.ToString("yyyy-MM-dd HH:mm:ss"));
                command.Parameters.AddWithValue("@idStatus", status.IdStatus);
                command.Parameters.AddWithValue("@comment", comment);

                // Выполнение команды
                command.ExecuteNonQuery();
            }

            // Вывод сообщения об успешном добавлении картриджа и закрытие окна
            MessageBox.Show("Картридж добавлен");
            AdminWin adminWin = new AdminWin();
            adminWin.Show();
            Close();
        }
    }
}
