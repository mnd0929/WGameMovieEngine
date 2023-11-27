using System;
using System.Collections.Generic;
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

namespace WGameMovieEngine.Engine
{
    public enum KeyResponseResult
    {
        Successfully,
        Unsuccessful
    }
    public class SyncAnswer 
    { 
        public object Answer { get; set; }
        public SyncAnswer() { }
        public SyncAnswer(object answer) { Answer = answer; }
    }
    public class Functions
    {
        public GameWindow GameWindow;
        public Functions(GameWindow gameWindow) { GameWindow = gameWindow; }
        public async Task<SyncAnswer> PlayVideo(string source, bool IsAsync = false)
        {
            GameWindow.engineUI_Player.Source = new Uri(source, UriKind.RelativeOrAbsolute);
            GameWindow.engineUI_Player.Play();

            if (!IsAsync)
            {
                bool IsPlaying = true;
                GameWindow.engineUI_Player.MediaEnded += (_e, _s) => { IsPlaying = false; };
                while (IsPlaying) { await Task.Delay(1); }
            }

            return new SyncAnswer();
        }
        public MediaElement PlayAudio(string source)
        {
            MediaElement mediaElement = new MediaElement();
            mediaElement.LoadedBehavior = MediaState.Manual;
            mediaElement.UnloadedBehavior = MediaState.Manual;
            mediaElement.Opacity = 0;
            mediaElement.Source = new Uri(source, UriKind.RelativeOrAbsolute);
            mediaElement.Play();

            return mediaElement;
        }
        public void Pause()
        {
            GameWindow.engineUI_Player.Pause();
        }
        public void Stop()
        {
            GameWindow.engineUI_Player.Stop();
        }
        public void ChangeSpeed(double newSpeed)
        {
            GameWindow.engineUI_Player.SpeedRatio = newSpeed;
        }
        public void ChangeVolume(double newVolume)
        {
            GameWindow.engineUI_Player.Volume = newVolume;
        }
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
        public async Task<SelectionMenuItem> GetAnswerFromSelectionMenu(SelectionMenu selectionMenu)
        {
            return await selectionMenu.ShowQuestion(GameWindow);
        }
    }
}
