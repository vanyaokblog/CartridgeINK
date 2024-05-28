using InkCartridge.Views.Fonts;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace InkCartridge.Views
{
    /// <summary>
    /// Логика взаимодействия для AdminWin.xaml
    /// </summary>
    public partial class AdminWin : Window
    {
        public AdminWin()
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

        // Обработчик события окончания редактирования ячейки в DataGrid
        private void CartridgesDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            // Если редактируемая ячейка - это ячейка "Комментарий"
            if (e.Column.Header.ToString() == "Комментарий")
            {
                // Получение редактируемого картриджа
                var cartridge = (Cartridge)e.Row.Item;

                // Получение нового комментария
                var newComment = ((TextBox)e.EditingElement).Text;

                // Обновление комментария в базе данных
                DatabaseManager.UpdateCartridgeComment(cartridge, newComment);
            }
        }

        // Обработчик события нажатия кнопки удаления картриджа
        private void DeleteCartridge_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Получение выбранного картриджа
                var selectedCartridge = (Cartridge)CartridgesDataGrid.SelectedItem;

                if (selectedCartridge != null)
                {
                    // Удаление картриджа из базы данных
                    DatabaseManager.DeleteCartridge(selectedCartridge);

                    // Удаление картриджа из списка
                    ((List<Cartridge>)CartridgesDataGrid.ItemsSource).Remove(selectedCartridge);
                    CartridgesDataGrid.Items.Refresh();
                }
            }
            catch
            {
                MessageBox.Show("Выбрана пустая строка");
            }
        }

        // Обработчик события нажатия кнопки установки статуса
        private void SetStatus_Click(object sender, RoutedEventArgs e)
        {
            // Получение выбранного картриджа
            var selectedCartridge = (Cartridge)CartridgesDataGrid.SelectedItem;

            if (selectedCartridge != null)
            {
                // Получение выбранного пункта меню
                var menuItem = e.OriginalSource as MenuItem;

                // Получение нового статуса
                var newStatusName = menuItem.Header.ToString();

                // Обновление статуса в базе данных
                DatabaseManager.UpdateCartridgeStatus(selectedCartridge, newStatusName);
                CartridgesDataGrid.Items.Refresh();
            }
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

        // Обработчик события нажатия кнопки добавления пользователя
        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            // Создание и открытие окна добавления пользователя
            AddUserWindow addUserWindow = new AddUserWindow();
            addUserWindow.Show();
        }

        // Обработчик события нажатия кнопки добавления картриджа
        private void AddCartridge_Click(object sender, RoutedEventArgs e)
        {
            // Создание и открытие окна добавления картриджа
            AddCartridgeWindow addCartridgeWindow = new AddCartridgeWindow();
            addCartridgeWindow.Show();
            Close();
        }
    }
}
