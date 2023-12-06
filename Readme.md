# GameMovieEngine 3

Простой движок для создания игрофильмов

- Вся логика проекта находится в конструкторе класса Main
- Все функции, ресурсы и ивенты находятся в полях класса GameWindow

Пример регистрации ресурса:
```csharp
//Project.Resources.cs +=

        public Resource ICON_github = new Resource
            (
                Environment.CurrentDirectory + "\\github-mark-white.ico", // Прямой путь к ресурсу (Локально)
                "https://cdn.discordapp.com/attachments/836269521483595796/1179068945907597312/github-mark-white.ico" // Прямая ссылка на ресурс с сервера
            );
```

Пример создания выбора:
```csharp
GameWindow window = new GameWindow();
```