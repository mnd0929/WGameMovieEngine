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
SelectionMenuItem selectionMenuItem2 = await gameWindow.Functions.GetAnswerFromSelectionMenu(new SelectionMenu() { }

                        .AddSelectionMenuItem(
                            new SelectionMenuItem(0, "Новая игра", gameWindow.Resources.ICON_github.FullName
                        ))

                        .AddSelectionMenuItem(
                            new SelectionMenuItem(1, "Настройки", gameWindow.Resources.ICON_github.FullName
                        ))

                        .AddSelectionMenuItem(
                            new SelectionMenuItem(2, "Выход", gameWindow.Resources.ICON_github.FullName
                        ))

                    );

            if (selectionMenuItem2.Index == 0)
            {
                // Start Game
                return;
            }
            else if (selectionMenuItem2.Index == 1)
            {
                // Settings
                return;
            }
            else if (selectionMenuItem2.Index == 2)
            {
                new Animations().CubicAnimation(gameWindow.engineUI_Player, MediaElement.OpacityProperty, 1, 0, 1);
                await Task.Delay(1000);
                Environment.Exit(0);
            }
```

Требование у пользователя нажать на определенную кнопку в течение определенного времени:
```csharp
if ((await gameWindow.Functions.KeyResponse(
Key.O, // Требуемая кнопка
1.100, // Время, в течение которого требуется нажать на кнопку
new Point(1, 1))) // Позиция оверлея на экране
 == KeyResponseResult.Unsuccessful)
{
    // Пользователь не успел нажать
    return;
}
else 
{
    // Пользователь успел нажать
}
```