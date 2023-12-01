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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WGameMovieEngine.Engine.UI
{
    /// <summary>
    /// Логика взаимодействия для Logo.xaml
    /// </summary>
    public partial class Logo : UserControl
    {
        public Logo()
        {
            InitializeComponent();
        }

        public void ShowAnimation()
        {
            new Animations().CubicAnimation(this.LogoUI_Label, Label.OpacityProperty, 0, 1, 5000);
            new Animations().CubicAnimation(this.LogoUI_Label, Label.FontSizeProperty, 0, 20, 5000);
        }
    }
}
