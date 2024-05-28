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
            string serialNumber = SerialNumberTextBox.Text; // Получение серийного номера из текстового поля
            List<Cartridge> cartridges = DatabaseManager.LoadCartridges(); // Получение списка картриджей из базы данных

            // Поиск картриджа по серийному номеру
            Cartridge foundCartridge = cartridges.Find(c => c.SerialNumber == serialNumber);

            if (foundCartridge != null) // Если картридж найден
            {
                // Подсветка картриджа в списке
                int index = cartridges.IndexOf(foundCartridge);
                CartridgesDataGrid.SelectedIndex = index;
            }
            else // Если картридж не найден
            {
                MessageBox.Show("Картридж с таким серийным номером не найден.");
            }
        }
    }
}