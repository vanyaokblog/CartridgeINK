using System.Collections.Generic;
using System.Windows;

namespace InkCartridge.Views
{
    /// <summary>
    /// Логика взаимодействия для GuestWin.xaml
    /// </summary>
    public partial class GuestWin : Window
    {
        public GuestWin()
        {
            InitializeComponent();
            LoadCartridgesIntoDataGrid();
        }
        // Метод для загрузки данных о картриджах в DataGrid
        private void LoadCartridgesIntoDataGrid()
        {
            // Получение списка картриджей из базы данных
            List<Cartridge> cartridges = DatabaseManager.LoadCartridges();

            // Установка списка картриджей в качестве источника данных для DataGrid
            CartridgesDataGrid.ItemsSource = cartridges;

            // Обновление элементов DataGrid
            CartridgesDataGrid.Items.Refresh();
        }

        // Обработчик события нажатия кнопки поиска
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            // Получение подстроки из текстового поля
            string substring = SearchTextBox.Text;

            // Поиск картриджей по подстроке
            List<Cartridge> foundCartridges = DatabaseManager.FindCartridgesBySubstring(substring);

            if (foundCartridges.Count == 0) // Если список пуст
            {
                MessageBox.Show("По вашему запросу ничего не найдено."); // Отображение сообщения
            }
            else
            {
                // Установка найденных картриджей в качестве источника данных для DataGrid
                CartridgesDataGrid.ItemsSource = foundCartridges;
                CartridgesDataGrid.Items.Refresh();
            }
        }
    }
}