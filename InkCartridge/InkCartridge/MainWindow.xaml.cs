using InkCartridge.Views;
using Microsoft.Data.Sqlite;
using System.Windows;

namespace InkCartridge
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Обработчик события нажатия кнопки входа
        private void logIn_Click(object sender, RoutedEventArgs e)
        {
            // Создание и открытие соединения с базой данных
            using (var connection = new SqliteConnection("Data Source=cartridges.db"))
            {
                connection.Open();

                // Создание команды SQL для получения данных пользователя
                var command = connection.CreateCommand();
                command.CommandText = @"
                SELECT Users.*, Roles.NameRole 
                FROM Users 
                INNER JOIN Roles ON Users.idRole = Roles.idRole
                WHERE Username = @username AND Password = @password";

                // Добавление параметров в команду SQL
                command.Parameters.AddWithValue("@username", loginBox.Text);
                command.Parameters.AddWithValue("@password", passBox.Password);

                // Выполнение команды и обработка результатов
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read()) // Если пользователь найден
                    {
                        // Получение роли пользователя
                        var role = reader.GetString(4);

                        // Открытие соответствующего окна в зависимости от роли пользовател
                        switch (role)
                        {
                            case "Админ":
                                // Открываем окно для админа
                                AdminWin adminWin = new AdminWin();
                                adminWin.Show();
                                Close();
                                break;
                            case "Редактор":
                                // Открываем окно для редактора
                                EditorWin editorWin = new EditorWin();
                                editorWin.Show();
                                Close();
                                break;
                        }
                    }
                    else // Если пользователь не найден
                    {
                        // Вывод сообщения об ошибке
                        MessageBox.Show("Неверный логин или пароль");
                    }
                }
            }
        }

        // Обработчик события нажатия кнопки входа как гость
        private void logInGuest_Click(object sender, RoutedEventArgs e)
        {
            GuestWin guestWin = new GuestWin();
            guestWin.Show();
            Close();
        }

        // Обработчик события нажатия кнопки выхода
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
