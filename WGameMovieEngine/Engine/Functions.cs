using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using WGameMovieEngine.Engine.UI;
using System.Diagnostics;
using System.Net;
using WGameMovieEngine.Engine.Resources;
using Microsoft.Win32;

namespace WGameMovieEngine.Engine
{
    /// <summary>
    /// Пользователь успел / не успел нажать на кнопку
    /// </summary>
    public enum KeyResponseResult
    {
        Successfully,
        Unsuccessful
    }

    /// <summary>
    /// Возврат синхронных операций
    /// </summary>
    public class SyncAnswer 
    { 
        public object Answer { get; set; }
        public SyncAnswer() { }
        public SyncAnswer(object answer) { Answer = answer; }
    }

    /// <summary>
    /// Функции движка и игрового окна
    /// </summary>
    public class Functions
    {
        public GameWindow GameWindow { get; private set; }
        public Functions(GameWindow gameWindow) { GameWindow = gameWindow; }

        /// <summary>
        /// Синхронное / асинхронное воспроизведение видео
        /// </summary>
        public async Task<SyncAnswer> PlayVideo(Resource source, bool IsAsync = false)
        {
            GameWindow.engineUI_Player.Source = new Uri(source.FullName, UriKind.RelativeOrAbsolute);
            GameWindow.engineUI_Player.Play();

            if (!IsAsync)
            {
                bool IsPlaying = true;
                GameWindow.engineUI_Player.MediaEnded += (_e, _s) => { IsPlaying = false; };
                while (IsPlaying) { await Task.Delay(1); }
            }

            return new SyncAnswer();
        }

        /// <summary>
        /// Aсинхронное воспроизведение аудио
        /// </summary>
        public MediaElement PlayAudio(Resource source)
        {
            MediaElement mediaElement = new MediaElement();
            mediaElement.LoadedBehavior = MediaState.Manual;
            mediaElement.UnloadedBehavior = MediaState.Manual;
            mediaElement.Opacity = 0;
            mediaElement.Source = new Uri(source.FullName, UriKind.RelativeOrAbsolute);
            mediaElement.Play();

            return mediaElement;
        }

        /// <summary>
        /// Временная остановка видео
        /// </summary>
        public void Pause()
        {
            GameWindow.engineUI_Player.Pause();
        }

        /// <summary>
        /// Завершение воспроизведения
        /// </summary>
        public void Stop()
        {
            GameWindow.engineUI_Player.Stop();
        }

        /// <summary>
        /// Изменение скорости воспроизведения основного плеера (x)
        /// </summary>
        public void ChangeSpeed(double newSpeed)
        {
            GameWindow.engineUI_Player.SpeedRatio = newSpeed;
        }

        /// <summary>
        /// Изменение громкости воспроизведения основного плеера (0,00 - 1,00)
        /// </summary>
        public void ChangeVolume(double newVolume)
        {
            GameWindow.engineUI_Player.Volume = newVolume;
        }

        /// <summary>
        /// Автоматическое восстановление ресурсов, если это необходимо
        /// </summary>
        public async Task RestoreResources()
        {
            Project.Resources resources = new Project.Resources();

            BindingFlags bindingFlags = BindingFlags.Public |
                            BindingFlags.NonPublic |
                            BindingFlags.Instance |
                            BindingFlags.Static;

            List<Uri> downloadUris = new List<Uri>();
            List<string> downloadNames = new List<string>();

            foreach (FieldInfo field in typeof(Project.Resources).GetFields(bindingFlags))
            {
                Resource resource = (field.GetValue(resources) as Resource);

                if (!File.Exists(resource.FullName))
                {
                    downloadUris.Add(new Uri(resource.ServerUrl));
                    downloadNames.Add(resource.Name);
                }
            }

            if (downloadNames.Count() > 0)
            {
                if (await IsConnectionAvailable("https://google.com/"))
                {
                    await DownloadResources(downloadUris, downloadNames, null);
                }
                else
                {
                    SelectionMenuItem selectionMenuItem = await GetAnswerFromSelectionMenu(new SelectionMenu()

                        .AddSelectionMenuItem(
                            new SelectionMenuItem("Скачать ресурсы с сервера"
                        ))

                        .AddSelectionMenuItem(
                            new SelectionMenuItem("Выбрать архив с ресурсами"
                        ))

                    );

                    if (selectionMenuItem.Text == "Скачать ресурсы с сервера")
                    {
                        await GameWindow.Functions.RestoreResources();
                        return;
                    }
                    else if (selectionMenuItem.Text == "Выбрать архив с ресурсами")
                    {
                        OpenFileDialog openFileDialog = new OpenFileDialog();
                        if (openFileDialog.ShowDialog().Value)
                        {
                            await ZipExtractWithProgress(openFileDialog.FileName, Environment.CurrentDirectory);

                            await RestoreResources();
                            return;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Проверка связи с заданным сервером
        /// </summary>
        public static async Task<bool> IsConnectionAvailable(string strServer)
        {
            try
            {
                HttpWebRequest httpReq = null;
                HttpWebResponse httpWeb = null;

                await Task.Run(() =>
                {
                    httpReq = (HttpWebRequest)HttpWebRequest.Create(strServer);
                    httpWeb = (HttpWebResponse)httpReq.GetResponse();
                });

                if (HttpStatusCode.OK == httpWeb.StatusCode)
                {
                    httpWeb.Close();
                    return true;
                }
                else
                {
                    httpWeb.Close();
                    return false;
                }
            }
            catch (WebException)
            {
                return false;
            }
        }

        /// <summary>
        /// Возвращает список Resources
        /// </summary>
        public static Project.Resources[] GetAllResources()
        {
            var statics = typeof(Project.Resources).GetFields(BindingFlags.Static | BindingFlags.Public);
            var strategies = statics.Where(f => f.FieldType == typeof(Project.Resources));
            var values = strategies.Select(s => s.GetValue(null));
            return values.Cast<Project.Resources>().ToArray();
        }

        /// <summary>
        /// Отображает на экране кнопку, которую нужно нажать в течении определенного кол-ва секунд
        /// </summary>
        public async Task<KeyResponseResult> KeyResponse(Key key, double duration, Point pointOnScreen)
        {
            // Создание оверлея по заданным координатам
            KeyResponseOverlay keyResponseOverlay = new KeyResponseOverlay(key.ToString());
            keyResponseOverlay.Margin = new Thickness(pointOnScreen.X, pointOnScreen.Y, 0, 0);
            GameWindow.engineUI_Grid.Children.Add(keyResponseOverlay);

            KeyResponseResult result = KeyResponseResult.Unsuccessful;

            // Создание событий
            KeyEventHandler GameWindowKeyDown = (_s, _e) =>
            {
                if (_e.Key == key)
                {
                    keyResponseOverlay.kroUI_KeyEllipse.Fill = new SolidColorBrush(Colors.Green);
                    keyResponseOverlay.KeyDownAnimation(true);
                    keyResponseOverlay.TrueKeyPressedAnimation();
                    result = KeyResponseResult.Successfully;
                }
                else
                {
                    keyResponseOverlay.KeyDownAnimation(false);
                }
            };
            KeyEventHandler GameWindowKeyUp = (_s, _e) =>
            {
                keyResponseOverlay.KeyUpAnimation();
            };

            // Привязка событий на оверлей
            GameWindow.KeyDown += GameWindowKeyDown;
            GameWindow.KeyUp += GameWindowKeyUp;

            // Даю время на подумать
            await Task.Delay((int)(duration * 1000));

            // Отвязка событий
            GameWindow.KeyDown -= GameWindowKeyDown;
            GameWindow.KeyUp -= GameWindowKeyUp;

            if (result == KeyResponseResult.Unsuccessful)
            {
                keyResponseOverlay.kroUI_KeyEllipse.Fill = new SolidColorBrush(Colors.Red);
                keyResponseOverlay.TrueKeyPressedAnimation();
            }

            return result;
        }

        /// <summary>
        /// Распаковка архива в указанную директорию с прогрессом на экране
        /// </summary>
        public async Task ZipExtractWithProgress(string archivePath, string finalDirectory)
        {
            Engine.UI.LoadPanel loadPanel = new LoadPanel();
            loadPanel.LoadPanelUI_TitleLabel.Content = null;
            loadPanel.LoadPanelUI_Subtitle.Content = null;
            loadPanel.HorizontalAlignment = HorizontalAlignment.Center;
            loadPanel.VerticalAlignment = VerticalAlignment.Center;
            loadPanel.ChangeProgress(0);
            loadPanel.Show();

            GameWindow.engineUI_Grid.Children.Add(loadPanel);

            loadPanel.LoadPanelUI_TitleLabel.Content = $"Распаковка {Path.GetFileName(archivePath)}...";

            await Task.Run(() =>
            {
                using (var archive = ZipFile.OpenRead(archivePath))
                {
                    int entriesCount = archive.Entries.Count;

                    for (int i = 0; i < entriesCount; i++)
                    {
                        string path = finalDirectory + "\\" + archive.Entries[i].FullName;

                        if (path.EndsWith("/"))
                        {
                            Directory.CreateDirectory(path.TrimEnd('/'));
                        }
                        else
                        {
                            archive.Entries[i].ExtractToFile(archive.Entries[i].FullName);
                        }

                        loadPanel.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            try
                            {
                                double progress = GetPercent(entriesCount, i);

                                loadPanel.ChangeProgress(progress);
                            }
                            catch { }

                        }), null);
                    }


                }
            });

            loadPanel.Hide();
            loadPanel = null;
        }

        /// <summary>
        /// Скачивает файл с прогрессом в тек. дерикторию
        /// </summary>
        public async Task<bool> DownloadResources(List<Uri> uris, List<string> names, string subtitle)
        {
            try
            {
                Engine.UI.LoadPanel loadPanel = new LoadPanel();
                loadPanel.LoadPanelUI_TitleLabel.Content = "Инициализация загрузчика";
                loadPanel.LoadPanelUI_Subtitle.Content = subtitle;
                loadPanel.HorizontalAlignment = HorizontalAlignment.Center;
                loadPanel.VerticalAlignment = VerticalAlignment.Center;
                loadPanel.ChangeProgress(0);
                loadPanel.Show();

                GameWindow.engineUI_Grid.Children.Add(loadPanel);

                for (int i = 0; i < uris.Count(); i++)
                {
                    loadPanel.LoadPanelUI_TitleLabel.Content = $"Скачивание {names[i]}";
                    loadPanel.ChangeProgress(0);

                    bool isFileDownloaded = false;
                    WebClient webClient = new WebClient();
                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();
                    webClient.DownloadProgressChanged += (sender, e) =>
                    {
                        if (stopwatch.Elapsed.TotalMilliseconds > 1000)
                        {
                            if (subtitle == null)
                            {
                                loadPanel.LoadPanelUI_Subtitle.Content =
                                $"{e.BytesReceived / 1024 / 1024} / {e.TotalBytesToReceive / 1024 / 1024} MB";
                            }

                            loadPanel.ChangeProgress(e.ProgressPercentage);
                            stopwatch.Restart();
                        }
                    };
                    webClient.DownloadFileCompleted += (sender, e) =>
                    {
                        isFileDownloaded = true;
                    };
                    webClient.DownloadFileAsync(uris[i], names[i]);

                    while (!isFileDownloaded) { await Task.Delay(100); }

                    webClient.Dispose();
                }

                loadPanel.Hide();
                loadPanel = null;

                return true;
            }
            catch 
            {
                return false;
            }
        }

        /// <summary>
        /// Получает процент от числа
        /// </summary>
        private Int32 GetPercent(Int32 b, Int32 a)
        {
            if (b == 0) return 0;

            return (Int32)(a / (b / 100M));
        }

        /// <summary>
        /// Синхронно отображает на экране SelectionMenu и возвращает выбранный пункт
        /// </summary>
        public async Task<SelectionMenuItem> GetAnswerFromSelectionMenu(SelectionMenu selectionMenu)
        {
            return await selectionMenu.ShowQuestion(GameWindow);
        }
    }
}
