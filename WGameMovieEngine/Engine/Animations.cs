using System;
using System.Windows.Media.Animation;
using System.Windows;

namespace WGameMovieEngine.Engine
{
    public class Animations
    {
        /// <summary>
        /// Кубическая анимация
        /// </summary>
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

        /// <summary>
        /// Линейная анимация
        /// </summary>
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
