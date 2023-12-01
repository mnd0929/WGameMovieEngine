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
            await gameWindow.Functions.PlayVideo(gameWindow.Resources.VIDEO_SUNP001);
        }
        break;

    case 1:
        {
            await gameWindow.Functions.PlayVideo(gameWindow.Resources.VIDEO_SUNP002);
        }
        break;
}
```
