using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GameEngine
{
    class MyButton
    {
        public static Button CreateButton(string inContent)
        {

            Button button = new Button();
            button.Margin = new Thickness(10, 5, 10, 5);
            Color color = Color.FromRgb((byte)200, (byte)200, (byte)70);
            button.Background = new SolidColorBrush(color);
            button.MaxWidth = 100;
            button.Content = inContent;
            return button;
        }
    }
}
