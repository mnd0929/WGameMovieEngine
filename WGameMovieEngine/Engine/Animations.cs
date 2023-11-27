using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows;
using WGameMovieEngine.Engine.UI;
using System.Windows.Media;

namespace WGameMovieEngine.Engine
{
    public class Animations
    {
        public void CubicAnimation(UIElement uiElement, DependencyProperty dependencyProperty, double startValue, double finalValue, double duration)
        {
            // Создание анимации
            DoubleAnimation animation = new DoubleAnimation();
            animation.From = startValue; // начальное значение ширины
            animation.To = finalValue; // конечное значение ширины
            animation.Duration = new Duration(TimeSpan.FromSeconds(duration)); // длительность анимации

            // Добавление эффекта плавности
            animation.EasingFunction = new CubicEase();

            uiElement.BeginAnimation(dependencyProperty, animation);
        }
        public void LinearAnimation(UIElement uiElement, DependencyProperty dependencyProperty, double startValue, double finalValue, double duration)
        {
            // Создание анимации
            DoubleAnimation animation = new DoubleAnimation();
            animation.From = startValue; // начальное значение ширины
            animation.To = finalValue; // конечное значение ширины
            animation.Duration = new Duration(TimeSpan.FromSeconds(duration)); // длительность анимации

            uiElement.BeginAnimation(dependencyProperty, animation);
        }
    }
}
