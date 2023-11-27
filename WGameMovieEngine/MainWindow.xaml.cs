using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WGameMovieEngine.Engine;
using WGameMovieEngine.Engine.UI;

namespace WGameMovieEngine
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GameWindow gameWindow = new GameWindow();
        public MainWindow()
        {
            this.Hide();

            gameWindow.engineUI_DebugLabel.MouseDown += (_s, _e) =>
            {
                gameWindow.engineUI_DebugLabel.Content = gameWindow.engineUI_Player.Position.TotalMilliseconds;
                Clipboard.SetText(gameWindow.engineUI_DebugLabel.Content.ToString());
            };

            Test();

            //InitializeComponent();
        }

        public const string TName = "Убеги от шампуня чтобы выжить! (Beta Engine)";

        public async void Test()
        {
            gameWindow.Title = TName;
            gameWindow.Functions.ChangeSpeed(1);
            gameWindow.Functions.ChangeVolume(1);
            gameWindow.Show();

            SelectionMenuItem selectionMenuItem2 = await gameWindow.Functions.GetAnswerFromSelectionMenu(new SelectionMenu()

                    .AddSelectionMenuItem(
                        new SelectionMenuItem("Начать", @"C:\Users\Minedroid\Downloads\github-mark-white.ico"
                    ))

                    .AddSelectionMenuItem(
                        new SelectionMenuItem("Настройки", @"C:\Users\Minedroid\Downloads\github-mark-white.ico"
                    ))

                    .AddSelectionMenuItem(
                        new SelectionMenuItem("Выход", @"C:\Users\Minedroid\Downloads\github-mark-white.ico"
                    ))

                );

            if (selectionMenuItem2.Text == "Выход")
            {
                new Animations().CubicAnimation(gameWindow.engineUI_Player, MediaElement.OpacityProperty, 1, 0, 1);
                await Task.Delay(1000);
                Environment.Exit(0);
            }

            await gameWindow.Functions.PlayVideo("SUNP001.mp4");
            await gameWindow.Functions.PlayVideo("SUNP002.mp4", true);

            await Task.Delay(40000);
            if ((await gameWindow.Functions.KeyResponse(Key.X, 2, new Point(1, 1))) == KeyResponseResult.Unsuccessful)
            {
                Fail();
                return;
            }
            await Task.Delay(4000);

            SelectionMenuItem selectionMenuItem = await gameWindow.Functions.GetAnswerFromSelectionMenu(new SelectionMenu()

                .AddSelectionMenuItem(
                    new SelectionMenuItem("Take", @"C:\Users\Minedroid\Downloads\github-mark-white.ico"
                ))

                .AddSelectionMenuItem(
                    new SelectionMenuItem("Я даун", @"C:\Users\Minedroid\Downloads\github-mark-white.ico"
                ))

            );

            switch (selectionMenuItem.Text)
            {
                case "Take":
                    {
                        await gameWindow.Functions.PlayVideo(@"SUNP004.mp4", true);
                        if ((await gameWindow.Functions.KeyResponse(Key.H, 2, new Point(1, 1))) == KeyResponseResult.Unsuccessful)
                        {
                            Fail();
                            return;
                        }
                        await Task.Delay(13000);
                        Fail();
                        return;
                    }

                case "Я даун":
                    {
                        await gameWindow.Functions.PlayVideo(@"SUNP003.mp4");

                        SelectionMenuItem selectionMenuItem1 = await gameWindow.Functions.GetAnswerFromSelectionMenu(new SelectionMenu()

                            .AddSelectionMenuItem(
                                new SelectionMenuItem("Exit", @"C:\Users\Minedroid\Downloads\github-mark-white.ico"
                            ))

                            .AddSelectionMenuItem(
                                new SelectionMenuItem("D", @"C:\Users\Minedroid\Downloads\github-mark-white.ico"
                            ))

                        );

                        switch (selectionMenuItem1.Text)
                        {
                            case "Exit":
                                {
                                    await gameWindow.Functions.PlayVideo(@"SUNP005.mp4", true);

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
                                    if ((await gameWindow.Functions.KeyResponse(Key.O, 2.649, new Point(1, 1))) == KeyResponseResult.Unsuccessful)
                                    {
                                        Fail();
                                        return;
                                    }
                                    await Task.Delay(46569);

                                    //
                                    // ---
                                    if ((await gameWindow.Functions.KeyResponse(Key.O, 2.106, new Point(1, 1))) == KeyResponseResult.Unsuccessful)
                                    {
                                        Fail();
                                        return;
                                    }
                                    await Task.Delay(3148);

                                    //
                                    // ---
                                    if ((await gameWindow.Functions.KeyResponse(Key.O, 1.894, new Point(1, 1))) == KeyResponseResult.Unsuccessful)
                                    {
                                        Fail();
                                        return;
                                    }
                                    await Task.Delay(201170);

                                    await gameWindow.Functions.GetAnswerFromSelectionMenu(
                                        new SelectionMenu()

                                        .AddSelectionMenuItem(
                                            new SelectionMenuItem("Завершить", @"C:\Users\Minedroid\Downloads\github-mark-white.ico"
                                        ))

                                    );

                                    Environment.Exit(0);
                                }
                                break;

                            case "D":
                                {
                                    await gameWindow.Functions.PlayVideo(@"SUNP006.mp4");
                                    Fail();
                                    return;
                                }
                        }
                    }
                    break;
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
                    new SelectionMenuItem("Вы проиграли", @"C:\Users\Minedroid\Downloads\github-mark-white.ico") { Color = new SolidColorBrush(Colors.Red) }
                )

                .AddSelectionMenuItem(
                    new SelectionMenuItem("Заново"
                ))

            );

            Test();
            return;
        }
    }
}
