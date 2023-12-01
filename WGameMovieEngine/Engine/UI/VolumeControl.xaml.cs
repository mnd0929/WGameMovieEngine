using System.Windows.Controls;
using System.Windows.Shapes;

namespace WGameMovieEngine.Engine.UI
{
    /// <summary>
    /// Логика взаимодействия для VolumeControl.xaml
    /// </summary>
    public partial class VolumeControl : UserControl
    {
        public VolumeControl()
        {
            InitializeComponent();
        }

        public void ChangeProgress(double percent)
        {
            double onePercent = LoadPanelUI_BorderRectangle.Width / 100;
            double newWidth = onePercent * percent;

            new Animations().CubicAnimation(LoadPanelUI_ProgressRectangle, Rectangle.WidthProperty, LoadPanelUI_ProgressRectangle.Width, newWidth, 0.5);
        }

        public void Show()
        {
            if (this.Opacity <= 0)
            {
                new Animations().CubicAnimation(this, Rectangle.OpacityProperty, this.Opacity, 0.7, 0.5);
            }
        }

        public void Hide()
        {
            if (this.Opacity >= 0.7)
            {
                new Animations().CubicAnimation(this, Rectangle.OpacityProperty, this.Opacity, 0, 0.5);
            }
        }
    }
}
