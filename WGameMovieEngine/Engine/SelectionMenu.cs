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
    public enum SelectionMenuAnimationStyle
    {
        Default
    }
    public enum SelectionMenuStyle
    {
        List
    }
    public class SelectionMenuItem
    {
        public string Text { get; set; }
        public ImageSource Icon { get; set; }
        public Brush Color = new SolidColorBrush(Colors.White);
        public SelectionMenuItem() { }
        public SelectionMenuItem(string text) { Text = text; }
        public SelectionMenuItem(string text, ImageSource icon) { Text = text; Icon = icon;  }
        public SelectionMenuItem(string text, ImageSource icon, Brush color) { Text = text; Icon = icon; Color = color; }
        public SelectionMenuItem(string text, string imageUrl) { Text = text; Icon = new BitmapImage(new Uri(imageUrl)); }
    }
    public class SelectionMenu
    {
        public GameWindow GameWindow { private get; set; }
        public SelectionMenuAnimationStyle AnimationStyle = SelectionMenuAnimationStyle.Default;
        public SelectionMenuStyle MenuStyle = SelectionMenuStyle.List;
        public List<SelectionMenuItem> SelectionMenuItems = new List<SelectionMenuItem>();
        public bool HidePanelAfterSelecting = true;
        public bool ShowPanelBeginSelecting = true;
        public async Task<SelectionMenuItem> ShowQuestion(GameWindow gameWindow)
        {
            SelectionMenuItem answer = null;
            GameWindow = gameWindow;

            gameWindow.engineUI_ChoiseListBox.Items.Clear();
            foreach (SelectionMenuItem menuItem in SelectionMenuItems)
            {
                SelectionMenuItemStyle1 selectionMenuItemStyle1 = new SelectionMenuItemStyle1();
                selectionMenuItemStyle1.gwUI_ChoiceIcon.Source = menuItem.Icon;
                selectionMenuItemStyle1.gwUI_ChoiceLabel.Content = menuItem.Text;
                selectionMenuItemStyle1.gwUI_ChoiceLabel.Foreground = menuItem.Color;
                selectionMenuItemStyle1.MouseDown += (_s, _e) =>
                {
                    SelectionMenuItemStyle1 selectionMenuItemStyle = _s as SelectionMenuItemStyle1;
                    answer = new SelectionMenuItem(
                        selectionMenuItemStyle.gwUI_ChoiceLabel.Content.ToString(),
                        selectionMenuItemStyle.gwUI_ChoiceIcon.Source,
                        selectionMenuItemStyle.gwUI_ChoiceLabel.Foreground
                        );
                    new Animations().CubicAnimation(selectionMenuItemStyle, SelectionMenuItemStyle1.OpacityProperty, 1, 0, 0.2);
                };
                gameWindow.engineUI_ChoiseListBox.Items.Add(selectionMenuItemStyle1);
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
        public void Show()
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
        public void Hide()
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
        public SelectionMenu AddSelectionMenuItem(SelectionMenuItem selectionMenuItem)
        {
            SelectionMenuItems.Add(selectionMenuItem);

            return this;
        }
        public SelectionMenu() { }
        public SelectionMenu( GameWindow gameWindow ) { GameWindow = gameWindow; }
    }
}
