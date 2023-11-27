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
    public partial class KeyResponseOverlay : UserControl
    {
        public KeyResponseOverlay(string key)
        {
            InitializeComponent();

            this.kroUI_KeyLabel.Content = key;
        }
        public void KeyDownAnimation(bool IsTrue)
        {
            new Animations().CubicAnimation(this.kroUI_KeyEllipse, Ellipse.WidthProperty, 80, 20, 0.2);
            new Animations().CubicAnimation(this.kroUI_KeyEllipse, Ellipse.HeightProperty, 80, 20, 0.2);
        }
        public void KeyUpAnimation()
        {
            new Animations().CubicAnimation(this.kroUI_KeyEllipse, Ellipse.WidthProperty, 20, 80, 0.2);
            new Animations().CubicAnimation(this.kroUI_KeyEllipse, Ellipse.HeightProperty, 20, 80, 0.2);
        }
        public void TrueKeyPressedAnimation()
        {
            new Animations().CubicAnimation(this, Ellipse.OpacityProperty, 1, 0, 0.7);
        }
    }
}
