# Информационная система “Система управления картриджами”.
### Название приложения: CartridgeINK
### Описание приложения:
**Система «CartridgeINK» – это программное обеспечение, которое позволяет пользователям управлять и использовать картриджи в различных устройствах.**

# Функции, реализованные в программе:
1. **Просмотр картриджей.** На главном экране представлен список всех картриджей. Пользователь может увидеть тип, модель, серийный номер, дату установки и статус каждого картриджа.
2. **Добавление новых картриджей.** Администратор может добавить в базу новый картридж, перейдя в соответствующее окно посредством нажатия кнопки "Добавить картридж".
3. **Удаление картриджа.** Выберите картридж из списка и в контекстном меню нажмите «Удалить картридж».
4. **Установка статуса картриджа.** Через контекстное меню можно установить один из трех статусов (в использовании, пустой, в ремонте) для выбранного картриджа.
5. **Добавление комментариев.** Двойным нажатием на колонку «Комментарий», можно написать комментарий к картриджу, изменения сохраняются в базе данных и отображаются в листе.
6. **Добавление новых пользователей.** Нажатием кнопки «Добавить пользователя» открывается соответствующее окно, в котором нужно заполните поля данными и выбрать роль для нового пользователя.

# Технологии, которые были использованы для разработки приложения:
- **C#** - объектно-ориентированный язык программирования.
- **Visual Studio Community 2022** - интегрированная среда разработки (IDE), которая обеспечивает удобное создание, отладку и развертывание приложений.
- **Windows Presentation Foundation** - система для построения клиентских приложений Windows с визуально привлекательными возможностями взаимодействия с пользователем, графическая подсистема в составе .NET Framework, использующая язык XAML.
- **SQLite** - компактная встраиваемая СУБД.

# Описание базы данных:
#### Файл базы данных называется cartridges.db <br/>
Файл базы данных расположен локально в проекте по пути **InkCartridge\InkCartridge\bin\Debug** </br>
В базе данных находятся 5 таблиц: _Users_, _Roles_, _TypeCartridge_, _Status_, _Cartridges_.

- **Таблица «Users»** _(idUser, Username, Password, idRole)_ содержит информацию о пользователях
- **Таблица «Roles»** _(idRole, NameRole)_ содержит информацию о ролях пользователей
- **Таблица «TypeCartridge»** _(idType, NameType)_ содержит информацию о типах картриджей
- **Таблица «Status»** _(idStatus, NameStatus)_ содержит информацию о состоянии картриджей
- **Таблица «Cartridges»** _(idCartridge, idType, Model, SerialNumber, InstallationDate, idStatus, Comment)_ содержит информацию о картриджах


# Скриншоты приложения:

<p align="center">
  <img <img src="https://github.com/vanyaokblog/CartridgeINK/blob/main/Screenshots/MainWindow.png">
</br>Окно авторизации
</br> </br> </br>
</p>

<p align="center">
  <img <img src="https://github.com/vanyaokblog/CartridgeINK/blob/main/Screenshots/AdminWin.png">
</br>Окно администратора
</br> </br> </br>
</p>

<p align="center">
  <img <img src="https://github.com/vanyaokblog/CartridgeINK/blob/main/Screenshots/EditorWin.png">
</br>Окно редактора
</br> </br> </br>
</p>

<p align="center">
  <img <img src="https://github.com/vanyaokblog/CartridgeINK/blob/main/Screenshots/GuestWin.png">
</br>Окно гостя
</br> </br> </br>
</p>

<p align="center">
  <img <img src="https://github.com/vanyaokblog/CartridgeINK/blob/main/Screenshots/AddCartridgeWindow.png">
</br>Добавление нового картриджа
</br> </br> </br>
</p>

<p align="center">
  <img <img src="https://github.com/vanyaokblog/CartridgeINK/blob/main/Screenshots/Status.png">
</br>Установка статуса
</br> </br> </br>
</p>

<p align="center">
  <img <img src="https://github.com/vanyaokblog/CartridgeINK/blob/main/Screenshots/AddUserWindow.png">
</br>Добавление нового пользователя
</br> </br> </br>
</p>
