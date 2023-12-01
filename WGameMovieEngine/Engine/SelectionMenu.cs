using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using WGameMovieEngine.Engine.UI;

namespace WGameMovieEngine.Engine
{
    /// <summary>
    /// Стиль появления / изчезновения SelectionMenu:
    ///     Default - Анимация прозрачности и ширины
    ///     Opacity - Анимация прозрачности
    ///     WithoutAnimation - Анимация отсутствует
    /// </summary>
    public enum SelectionMenuAnimationStyle
    {
        Default,
        Opacity,
        WithoutAnimation
    }

    /// <summary>
    /// Режим отображения SelectionMenu:
    ///     List - Список слева
    ///     CenterScreenList - Список по центру
    /// </summary>
    public enum SelectionMenuStyle
    {
        List,
        CenterScreenList
    }

    /// <summary>
    /// Элемент выбора для SelectionMenu
    /// </summary>
    public class SelectionMenuItem
    {
        public string Text { get; set; }
        public ImageSource Icon { get; set; }
        public int Index { get; set; }

        public Brush Color = new SolidColorBrush(Colors.White);

        public SelectionMenuItem() { }
        public SelectionMenuItem(int index) { Index = index; }
        public SelectionMenuItem(int index, string text) { Text = text; Index = index; }
        public SelectionMenuItem(string text) { Text = text; }
        public SelectionMenuItem(int index, string text, ImageSource icon) { Text = text; Icon = icon; Index = index; }
        public SelectionMenuItem(string text, ImageSource icon) { Text = text; Icon = icon; }
        public SelectionMenuItem(int index, string text, ImageSource icon, Brush color) { Text = text; Icon = icon; Color = color; Index = index; }
        public SelectionMenuItem(string text, ImageSource icon, Brush color) { Text = text; Icon = icon; Color = color; }
        public SelectionMenuItem(int index, string text, string imageUrl)
        {
            Text = text;
            Icon = new BitmapImage(new Uri(imageUrl));
            Index = index;
        }
        public SelectionMenuItem(string text, string imageUrl) 
        { 
            Text = text; 
            Icon = new BitmapImage(new Uri(imageUrl)); 
        }
    }

    /// <summary>
    /// Меню выбора
    /// </summary>
    public class SelectionMenu
    {
        public GameWindow GameWindow { private get; set; }
        public SelectionMenuAnimationStyle AnimationStyle = SelectionMenuAnimationStyle.Default;
        public SelectionMenuStyle MenuStyle = SelectionMenuStyle.List;
        public List<SelectionMenuItem> SelectionMenuItems = new List<SelectionMenuItem>();
        public bool HidePanelAfterSelecting = true;
        public bool ShowPanelBeginSelecting = true;

        /// <summary>
        /// Отображает меню выбора в указанном экземпляре игрового окна
        /// </summary>
        public async Task<SelectionMenuItem> ShowQuestion(GameWindow gameWindow)
        {
            SelectionMenuItem answer = null;
            GameWindow = gameWindow;

            gameWindow.engineUI_ChoiseListBox.Items.Clear();
            foreach (SelectionMenuItem menuItem in SelectionMenuItems)
            {
                SelectionMenuItemStyle1 selectionMenuItemStyle1 = new SelectionMenuItemStyle1();
                selectionMenuItemStyle1.Index = menuItem.Index;
                selectionMenuItemStyle1.gwUI_ChoiceIcon.Source = menuItem.Icon;
                selectionMenuItemStyle1.gwUI_ChoiceLabel.Content = menuItem.Text;
                selectionMenuItemStyle1.gwUI_ChoiceLabel.Foreground = menuItem.Color;
                selectionMenuItemStyle1.MouseDown += (_s, _e) =>
                {
                    SelectionMenuItemStyle1 selectionMenuItemStyle = _s as SelectionMenuItemStyle1;
                    answer = new SelectionMenuItem(
                        selectionMenuItemStyle.Index,
                        selectionMenuItemStyle.gwUI_ChoiceLabel.Content.ToString(),
                        selectionMenuItemStyle.gwUI_ChoiceIcon.Source,
                        selectionMenuItemStyle.gwUI_ChoiceLabel.Foreground
                        );
                    new Animations().CubicAnimation(selectionMenuItemStyle, SelectionMenuItemStyle1.OpacityProperty, 1, 0, 0.2);
                };
                gameWindow.engineUI_ChoiseListBox.Items.Add(selectionMenuItemStyle1);
            }

            if (MenuStyle == SelectionMenuStyle.List)
            {
                gameWindow.engineUI_ChoiseListBox.HorizontalAlignment = HorizontalAlignment.Left;
                gameWindow.engineUI_ChoiseListBox.Width = 0;
            }
            else if (MenuStyle == SelectionMenuStyle.CenterScreenList)
            {
                gameWindow.engineUI_ChoiseListBox.HorizontalAlignment = HorizontalAlignment.Stretch;
            }

            if (ShowPanelBeginSelecting)
            {
                Show();
            }

            while (answer == null) { await System.Threading.Tasks.Task.Delay(50); }

            if (HidePanelAfterSelecting)
            {
                Hide();
            }

            return answer;
        }

        /// <summary>
        /// Анимация появления
        /// </summary>
        public void Show()
        {
            if (AnimationStyle == SelectionMenuAnimationStyle.Opacity)
            {
                GameWindow.engineUI_ChoiseListBox.Width = 400;

                new Engine.Animations().CubicAnimation(
                                GameWindow.engineUI_ChoiseListBox,
                                ListBox.OpacityProperty,
                                0,
                                0.7,
                                0.5
                            );
            }
            else if (AnimationStyle == SelectionMenuAnimationStyle.Default)
            {
                new Engine.Animations().CubicAnimation(
                            GameWindow.engineUI_ChoiseListBox,
                            ListBox.WidthProperty,
                            0,
                            400,
                            0.5
                        );

                new Engine.Animations().CubicAnimation(
                                GameWindow.engineUI_ChoiseListBox,
                                ListBox.OpacityProperty,
                                0,
                                0.7,
                                0.5
                            );
            }
            else if (AnimationStyle == SelectionMenuAnimationStyle.WithoutAnimation)
            {
                GameWindow.engineUI_ChoiseListBox.Width = 400;
                GameWindow.engineUI_ChoiseListBox.Opacity = 0.7;
            }
        }

        /// <summary>
        /// Анимация изчезновения
        /// </summary>
        public void Hide()
        {
            if (AnimationStyle == SelectionMenuAnimationStyle.Opacity)
            {
                GameWindow.engineUI_ChoiseListBox.Width = 400;

                new Engine.Animations().CubicAnimation(
                                GameWindow.engineUI_ChoiseListBox,
                                ListBox.OpacityProperty,
                                0.7,
                                0,
                                0.5
                            );
            }
            else if (AnimationStyle == SelectionMenuAnimationStyle.Default)
            {
                new Engine.Animations().CubicAnimation(
                        GameWindow.engineUI_ChoiseListBox,
                        ListBox.WidthProperty,
                        200,
                        0,
                        0.5
                    );

                new Engine.Animations().CubicAnimation(
                             GameWindow.engineUI_ChoiseListBox,
                             ListBox.OpacityProperty,
                             0.7,
                             0,
                             0.5
                         );
            }
            else if (AnimationStyle == SelectionMenuAnimationStyle.WithoutAnimation)
            {
                GameWindow.engineUI_ChoiseListBox.Width = 0;
                GameWindow.engineUI_ChoiseListBox.Opacity = 0;
            }
        }

        /// <summary>
        /// Добавляет экземляр SelectionMenuItem в коллекцию и возвращает результат
        /// </summary>
        public SelectionMenu AddSelectionMenuItem(SelectionMenuItem selectionMenuItem)
        {
            SelectionMenuItems.Add(selectionMenuItem);

            return this;
        }
        public SelectionMenu() { }
        public SelectionMenu( GameWindow gameWindow ) { GameWindow = gameWindow; }
    }
}
