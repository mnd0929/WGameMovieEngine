using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WGameMovieEngine.Engine.UI
{
    /// <summary>
    /// Логика взаимодействия для GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        /// <summary>
        /// События GameWindow
        /// </summary>
        public Events Events { get; set; }

        /// <summary>
        /// Функции GameWindow
        /// </summary>
        public Functions Functions { get; set; }

        /// <summary>
        /// Ресурсы GameWindow
        /// </summary>
        public new Project.Resources Resources { get; set; }

        /// <summary>
        /// Главное игровое окно
        /// </summary>
        public GameWindow(bool InGameVolumeControl = true)
        {
            InitializeComponent();

            Events = new Events(this);
            Functions = new Functions(this);
            Resources = new Project.Resources();

            if (InGameVolumeControl)
            {
                Engine.UI.VolumeControl volumeControl = new VolumeControl();
                volumeControl.Margin = new Thickness(10, 10, 0, 0);
                volumeControl.HorizontalAlignment = HorizontalAlignment.Center;
                volumeControl.VerticalAlignment = VerticalAlignment.Top;
                volumeControl.Hide();

                this.MouseWheel += async (_e, _s) => 
                {
                    volumeControl.Show();

                    if (_s.Delta > 0)
                    {
                        if (this.engineUI_Player.Volume + 0.1 <= 1)
                        {
                            this.Functions.ChangeVolume(this.engineUI_Player.Volume + 0.1);
                        }
                    }
                    else
                    {
                        if (this.engineUI_Player.Volume - 0.1 >= 0)
                        {
                            this.Functions.ChangeVolume(this.engineUI_Player.Volume - 0.1);
                        }
                    }

                    volumeControl.ChangeProgress((double)(this.engineUI_Player.Volume * 100));

                    await Task.Delay(2000);

                    volumeControl.Hide();
                };

                this.engineUI_Grid.Children.Add(volumeControl);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Hide();
            Environment.Exit(0);
        }
    }
}
