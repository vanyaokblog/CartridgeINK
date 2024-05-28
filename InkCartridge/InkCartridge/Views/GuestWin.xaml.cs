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
    }
}