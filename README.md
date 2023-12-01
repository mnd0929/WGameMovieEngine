# GameMovieEngine
Простой движок для создания игрофильмов

- Вся логика размещается в Main.cs
- Функции, ивенты, ресурсы проекта вызываются / добываются через экземпляр Engine.UI.GameWindow

Пример создания выбора:
```csharp
SelectionMenuItem selectionMenuItem = await gameWindow.Functions.GetAnswerFromSelectionMenu(new SelectionMenu()

    .AddSelectionMenuItem(
        new SelectionMenuItem(0, "Ударить", gameWindow.Resources.ICON_github.FullName
    ))

    .AddSelectionMenuItem(
        new SelectionMenuItem(1, "Передумать", gameWindow.Resources.ICON_github.FullName
    ))

);

switch (selectionMenuItem.Index)
{
    case 0:
        {
            // Ударить
        }
        break;

    case 1:
        {
            // Передумать
        }
        break;
}
```

Пример создание KeyResponse:
```csharp
if ((await gameWindow.Functions.KeyResponse(Key.H, 2, new Point(1, 1))) == KeyResponseResult.Successfully)
{
    // Игрок успел нажать на кнопку H за 2 секунды
}
else
{
    // Игрок не успел
}
```
