using System.Threading.Tasks;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using WGameMovieEngine.Engine;
using WGameMovieEngine.Engine.UI;

namespace WGameMovieEngine
{
    public class Main
    {
        GameWindow gameWindow = new GameWindow();

        public const string TName = "Убеги от шампуня чтобы выжить! (Beta Engine)";

        /// <summary>
        /// Главная логика проекта
        /// </summary>
        public Main() 
        {
            gameWindow.engineUI_DebugLabel.MouseDown += (_s, _e) =>
            {
                gameWindow.engineUI_DebugLabel.Content = gameWindow.engineUI_Player.Position.TotalMilliseconds;
                Clipboard.SetText(gameWindow.engineUI_DebugLabel.Content.ToString());
            };

            Test();
        }

        public async void Test()
        {
            gameWindow.Title = TName;
            gameWindow.Functions.ChangeSpeed(1);
            gameWindow.Functions.ChangeVolume(1);
            gameWindow.Show();

            await gameWindow.Functions.RestoreResources();

            GamePause();

            gameWindow.KeyUp += (_s, _e) =>
            {
                if (_e.Key == Key.Escape)
                {
                    GamePause();
                }
            };
        }
        public async void StartGame()
        {
            await gameWindow.Functions.PlayVideo(gameWindow.Resources.VIDEO_SUNP001);
            await gameWindow.Functions.PlayVideo(gameWindow.Resources.VIDEO_SUNP002, true);

            await Task.Delay(40000);
            if ((await gameWindow.Functions.KeyResponse(Key.X, 2, new Point(1, 1))) == KeyResponseResult.Unsuccessful)
            {
                Fail();
                return;
            }
            await Task.Delay(4000);

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
                        await gameWindow.Functions.PlayVideo(gameWindow.Resources.VIDEO_SUNP004, true);
                        if ((await gameWindow.Functions.KeyResponse(Key.H, 2, new Point(1, 1))) == KeyResponseResult.Unsuccessful)
                        {
                            Fail();
                            return;
                        }
                        await Task.Delay(13000);
                        Fail();
                        return;
                    }

                case 1:
                    {
                        await gameWindow.Functions.PlayVideo(gameWindow.Resources.VIDEO_SUNP003);

                        SelectionMenuItem selectionMenuItem1 = await gameWindow.Functions.GetAnswerFromSelectionMenu(new SelectionMenu()

                            .AddSelectionMenuItem(
                                new SelectionMenuItem(0, "Выйти из комнаты", gameWindow.Resources.ICON_github.FullName
                            ))

                            .AddSelectionMenuItem(
                                new SelectionMenuItem(1, "Остаться", gameWindow.Resources.ICON_github.FullName
                            ))

                        );

                        switch (selectionMenuItem1.Index)
                        {
                            case 0:
                                {
                                    await gameWindow.Functions.PlayVideo(gameWindow.Resources.VIDEO_SUNP005, true);

                                    //
                                    // ---
                                    if ((await gameWindow.Functions.KeyResponse(Key.O, 1.648, new Point(1, 1))) == KeyResponseResult.Unsuccessful)
                                    {
                                        Fail();
                                        return;
                                    }
                                    await Task.Delay(6456);

                                    //
                                    // ---
                                    if ((await gameWindow.Functions.KeyResponse(Key.E, 1.793, new Point(1, 1))) == KeyResponseResult.Unsuccessful)
                                    {
                                        Fail();
                                        return;
                                    }
                                    await Task.Delay(14050);

                                    //
                                    // ---
                                    if ((await gameWindow.Functions.KeyResponse(Key.O, 1.053, new Point(1, 1))) == KeyResponseResult.Unsuccessful)
                                    {
                                        Fail();
                                        return;
                                    }
                                    await Task.Delay(2000);

                                    //
                                    // ---
                                    if ((await gameWindow.Functions.KeyResponse(Key.O, 1.100, new Point(1, 1))) == KeyResponseResult.Unsuccessful)
                                    {
                                        Fail();
                                        return;
                                    }
                                    await Task.Delay(5500);

                                    //
                                    // ---
                                    if ((await gameWindow.Functions.KeyResponse(Key.C, 1.786, new Point(1, 1))) == KeyResponseResult.Unsuccessful)
                                    {
                                        Fail();
                                        return;
                                    }
                                    await Task.Delay(20078);

                                    //
                                    // ---
                                    if ((await gameWindow.Functions.KeyResponse(Key.O, 1.894, new Point(1, 1))) == KeyResponseResult.Unsuccessful)
                                    {
                                        Fail();
                                        return;
                                    }
                                    await Task.Delay(20170);

                                    await gameWindow.Functions.GetAnswerFromSelectionMenu(
                                        new SelectionMenu()

                                        .AddSelectionMenuItem(
                                            new SelectionMenuItem(0, "Завершить", gameWindow.Resources.ICON_github.FullName
                                        ))

                                    );

                                    Environment.Exit(0);
                                }
                                break;

                            case 1:
                                {
                                    await gameWindow.Functions.PlayVideo(gameWindow.Resources.VIDEO_SUNP006);
                                    Fail();
                                    return;
                                }
                        }
                    }
                    break;
            }
        }
        public async void GamePause()
        {
            gameWindow.Functions.Stop();

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
                StartGame();
                return;
            }
            else if (selectionMenuItem2.Index == 1)
            {
                Test();
                return;
            }
            else if (selectionMenuItem2.Index == 2)
            {
                new Animations().CubicAnimation(gameWindow.engineUI_Player, MediaElement.OpacityProperty, 1, 0, 1);
                await Task.Delay(1000);
                Environment.Exit(0);
            }
        }
        public async void Fail()
        {
            for (double i = 1; i > 0; i -= 0.01)
            {
                gameWindow.Functions.ChangeSpeed(i);
                await Task.Delay((int)(10 * i));
            }
            gameWindow.Functions.Pause();

            SelectionMenuItem selectionMenuItem = await gameWindow.Functions.GetAnswerFromSelectionMenu(new SelectionMenu()

                .AddSelectionMenuItem(
                    new SelectionMenuItem(0, "Вы проиграли", gameWindow.Resources.ICON_github.FullName) { Color = new SolidColorBrush(Colors.Red) }
                )

                .AddSelectionMenuItem(
                    new SelectionMenuItem(1, "Заново"
                ))

            );

            Test();
            return;
        }
    }
}
