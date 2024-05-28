using Microsoft.Data.Sqlite;
using System.Windows;

namespace InkCartridge.Views
{
    public partial class AddUserWindow : Window
    {
        public AddUserWindow()
        {
            InitializeComponent();
            LoadRoles();
        }

        // Метод для загрузки ролей пользователей из базы данных
        private void LoadRoles()
        {
            // Создание и открытие соединения с базой данных
            using (var connection = new SqliteConnection("Data Source=cartridges.db"))
            {
                connection.Open();

                // Создание команды SQL для получения всех ролей
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Roles";

                // Выполнение команды и обработка результатов
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Создание объекта Role и добавление его в ComboBox
                        var role = new Role
                        {
                            IdRole = reader.GetInt32(0),
                            NameRole = reader.GetString(1)
                        };

                        RoleComboBox.Items.Add(role);
                    }
                }
            }
        }

        // Обработчик события нажатия кнопки добавления пользователя
        private void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            // Получение данных из полей ввода
            var username = UsernameTextBox.Text;
            var password = PasswordBox.Password;
            var role = (Role)RoleComboBox.SelectedItem;

            // Создание и открытие соединения с базой данных
            using (var connection = new SqliteConnection("Data Source=cartridges.db"))
            {
                connection.Open();

                // Создание команды SQL для добавления нового пользователя
                var command = connection.CreateCommand();
                command.CommandText = @"
                    INSERT INTO Users (Username, Password, IdRole)
                    VALUES (@username, @password, @idRole)";

                // Добавление параметров в команду SQL
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);
                command.Parameters.AddWithValue("@idRole", role.IdRole);

                // Выполнение команды
                command.ExecuteNonQuery();
            }

            // Вывод сообщения об успешном добавлении пользователя и закрытие окна
            MessageBox.Show("Пользователь добавлен");
            Close();
        }
    }
}
