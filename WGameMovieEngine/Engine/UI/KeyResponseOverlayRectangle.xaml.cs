using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WGameMovieEngine.Engine.UI
{
    /// <summary>
    /// Логика взаимодействия для KeyResponseOverlay.xaml
    /// </summary>
    public partial class KeyResponseOverlayRectangle : UserControl
    {
        public int FinalWidth = 100;
        public int FinalHeight = 50;
        public KeyResponseOverlayRectangle(string key)
        {
            InitializeComponent();

            this.kroUI_KeyLabel.Content = key;

            ShowAnimation();
        }
        public void ShowAnimation()
        {
            new Animations().CubicAnimation(this.kroUI_KeyRectangle, Rectangle.WidthProperty, 290, FinalWidth, 0.4);
            new Animations().CubicAnimation(this.kroUI_KeyRectangle, Rectangle.HeightProperty, 290, FinalHeight, 0.4);
            new Animations().CubicAnimation(this.kroUI_KeyRectangle, Rectangle.OpacityProperty, 0, 1, 0.4);
        }
        public void KeyDownAnimation(bool IsTrue)
        {
            new Animations().CubicAnimation(this.kroUI_KeyRectangle, Rectangle.WidthProperty, FinalWidth, FinalWidth / 4, 0.2);
            new Animations().CubicAnimation(this.kroUI_KeyRectangle, Rectangle.HeightProperty, FinalHeight, FinalHeight / 4, 0.2);
        }
        public void KeyUpAnimation()
        {
            new Animations().CubicAnimation(this.kroUI_KeyRectangle, Rectangle.WidthProperty, FinalWidth / 4, FinalWidth, 0.2);
            new Animations().CubicAnimation(this.kroUI_KeyRectangle, Rectangle.HeightProperty, FinalHeight / 4, FinalHeight, 0.2);
        }
        public void TrueKeyPressedAnimation()
        {
            new Animations().CubicAnimation(this, Rectangle.OpacityProperty, 1, 0, 0.7);
        }
    }
}
