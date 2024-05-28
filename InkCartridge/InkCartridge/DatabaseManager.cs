using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;

namespace InkCartridge
{
    public class DatabaseManager
    {
        // Метод для загрузки информации о картриджах из базы данных
        public static List<Cartridge> LoadCartridges()
        {
            // Создание и открытие соединения с базой данных
            using (var connection = new SqliteConnection("Data Source=cartridges.db"))
            {
                connection.Open();

                // Создание команды SQL для получения информации о картриджах
                var command = connection.CreateCommand();
                command.CommandText = @"
                SELECT Cartridges.*, TypeCartridge.NameType, Status.NameStatus 
                FROM Cartridges 
                INNER JOIN TypeCartridge ON Cartridges.idType = TypeCartridge.idType
                INNER JOIN Status ON Cartridges.idStatus = Status.idStatus";

                // Выполнение команды и обработка результатов
                using (var reader = command.ExecuteReader())
                {
                    var cartridges = new List<Cartridge>();

                    while (reader.Read())
                    {
                        // Создание объекта Cartridge и добавление его в список
                        var cartridge = new Cartridge
                        {
                            IdCartridge = reader.GetInt32(0),
                            IdType = reader.GetInt32(1),
                            Model = reader.GetString(2),
                            SerialNumber = reader.GetString(3),
                            InstallationDate = DateTime.Parse(reader.GetString(4)),
                            IdStatus = reader.GetInt32(5),
                            Comment = reader.IsDBNull(6) ? null : reader.GetString(6),
                            TypeCartridge = new TypeCartridge { NameType = reader.GetString(7) },
                            Status = new Status { NameStatus = reader.GetString(8) }
                        };

                        cartridges.Add(cartridge);
                    }

                    return cartridges; // Возвращение списка картриджей
                }
            }
        }

        // Метод для обновления комментария картриджа в базе данных
        public static void UpdateCartridgeComment(Cartridge cartridge, string newComment)
        {
            // Создание и открытие соединения с базой данных
            using (var connection = new SqliteConnection("Data Source=cartridges.db"))
            {
                connection.Open();

                // Создание команды SQL для обновления комментария картриджа
                var command = connection.CreateCommand();
                command.CommandText = @"
                UPDATE Cartridges
                SET Comment = @comment
                WHERE idCartridges = @id";

                // Добавление параметров в команду SQL
                command.Parameters.AddWithValue("@comment", newComment);
                command.Parameters.AddWithValue("@id", cartridge.IdCartridge);

                // Выполнение команды
                command.ExecuteNonQuery();
            }

            cartridge.Comment = newComment; // Обновление комментария в объекте Cartridge
        }

        // Метод для удаления картриджа из базы данных
        public static void DeleteCartridge(Cartridge cartridge)
        {
            // Создание и открытие соединения с базой данных
            using (var connection = new SqliteConnection("Data Source=cartridges.db"))
            {
                connection.Open();

                // Создание команды SQL для удаления картриджа
                var command = connection.CreateCommand();
                command.CommandText = @"
                DELETE FROM Cartridges
                WHERE idCartridges = @id";

                // Добавление параметра в команду SQL
                command.Parameters.AddWithValue("@id", cartridge.IdCartridge);

                // Выполнение команды
                command.ExecuteNonQuery();
            }
        }

        // Метод для обновления статуса картриджа в базе данных
        public static void UpdateCartridgeStatus(Cartridge cartridge, string newStatusName)
        {
            long newStatusId; // Объявление переменной для нового ID статуса

            // Создание и открытие соединения с базой данных
            using (var connection = new SqliteConnection("Data Source=cartridges.db"))
            {
                connection.Open();

                // Создание команды SQL для получения ID нового статуса
                var command = connection.CreateCommand();
                command.CommandText = @"
                SELECT idStatus 
                FROM Status 
                WHERE NameStatus = @name";

                // Добавление параметра в команду SQL
                command.Parameters.AddWithValue("@name", newStatusName);

                // Выполнение команды и получение результата
                newStatusId = (long)command.ExecuteScalar();
            }

            // Создание и открытие соединения с базой данных
            using (var connection = new SqliteConnection("Data Source=cartridges.db"))
            {
                connection.Open();

                // Создание команды SQL для обновления статуса картриджа
                var command = connection.CreateCommand();
                command.CommandText = @"
                UPDATE Cartridges
                SET idStatus = @id
                WHERE idCartridges = @idCartridge";

                // Добавление параметров в команду SQL
                command.Parameters.AddWithValue("@id", newStatusId);
                command.Parameters.AddWithValue("@idCartridge", cartridge.IdCartridge);

                // Выполнение команды
                command.ExecuteNonQuery();
            }

            // Обновление статуса в объекте Cartridge
            cartridge.IdStatus = (int)newStatusId;
            cartridge.Status.NameStatus = newStatusName;
        }

        // Метод для поиска картриджа по серийному номеру в базе данных
        public static Cartridge FindCartridgeBySerialNumber(string serialNumber)
        {
            // Создание и открытие соединения с базой данных
            using (var connection = new SqliteConnection("Data Source=cartridges.db"))
            {
                connection.Open();

                // Создание команды SQL для поиска картриджа по серийному номеру
                var command = connection.CreateCommand();
                command.CommandText = @"
                SELECT Cartridges.*, TypeCartridge.NameType, Status.NameStatus 
                FROM Cartridges 
                INNER JOIN TypeCartridge ON Cartridges.idType = TypeCartridge.idType
                INNER JOIN Status ON Cartridges.idStatus = Status.idStatus
                WHERE SerialNumber = @serialNumber";

                // Добавление параметра в команду SQL
                command.Parameters.AddWithValue("@serialNumber", serialNumber);

                // Выполнение команды и обработка результатов
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Создание объекта Cartridge и заполнение его данными из базы данных
                        var cartridge = new Cartridge
                        {
                            IdCartridge = reader.GetInt32(0),
                            IdType = reader.GetInt32(1),
                            Model = reader.GetString(2),
                            SerialNumber = reader.GetString(3),
                            InstallationDate = DateTime.Parse(reader.GetString(4)),
                            IdStatus = reader.GetInt32(5),
                            Comment = reader.IsDBNull(6) ? null : reader.GetString(6),
                            TypeCartridge = new TypeCartridge { NameType = reader.GetString(7) },
                            Status = new Status { NameStatus = reader.GetString(8) }
                        };

                        return cartridge; // Возвращение найденного картриджа
                    }
                }
            }

            return null; // Возвращение null, если картридж не найден
        }

    }
}