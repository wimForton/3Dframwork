using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace GameEngine
{
    class KeyFrameSlider : AnimationControl, IAnimationControl
    {

        private double sliderValue;

        public KeyFrameSlider(int Id, string name, double inValue, double inMinimum, double inMaximum, double inTickFrequency)
        {
            AnimCtrlGrid = new Grid();
            AnimCtrlGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(70) });
            AnimCtrlGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(60) });
            AnimCtrlGrid.ColumnDefinitions.Add(new ColumnDefinition());
            AnimCtrlGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(40) });
            Color color = Color.FromRgb((byte)90, (byte)90, (byte)90);
            AnimCtrlGrid.Background = new SolidColorBrush(color);
            AnimCtrlGrid.Margin = new Thickness(5, 5, 5, 5);
            Border border = new Border();
            border.BorderThickness = new Thickness(1, 1, 1, 1);
            border.BorderBrush = Brushes.Black;
            border.CornerRadius = new CornerRadius(4, 4, 4, 4);

            Grid.SetColumn(border, 0);
            Grid.SetRow(border, 0);
            Grid.SetColumnSpan(border, 4);
            Grid.SetRowSpan(border, 4);
            AnimCtrlGrid.Children.Add(border);

            TextBlock myName = new TextBlock();
            myName.Margin = new Thickness(5, 5, 2, 5);
            myName.Text = name;
            myName.FontSize = 16;
            myName.Foreground = new SolidColorBrush(Color.FromRgb((byte)255, (byte)255, (byte)255));

            TextBox text = new TextBox();
            Grid.SetColumn(text, 1);
            Grid.SetRow(text, 0);
            text.Width = 50;
            text.Margin = new Thickness(2, 5, 2, 5);

            mySlider = new MySlider(inValue, inMinimum, inMaximum, inTickFrequency);
            mySlider.ValueChanged += MySlider_ValueChanged;
            sliderValue = inValue;
            Grid.SetColumn(mySlider, 2);
            mySlider.Name = "name";
            mySlider.Margin = new Thickness(2, 5, 2, 5);
            Binding b = new Binding();
            b.Source = mySlider;
            b.Path = new PropertyPath("Value", mySlider.Value);
            b.Mode = BindingMode.TwoWay;
            b.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            text.SetBinding(TextBox.TextProperty, b);
            AnimCtrlGrid.Children.Add(myName);
            AnimCtrlGrid.Children.Add(text);
            AnimCtrlGrid.Children.Add(mySlider);

            SetKeyButton = MyButton.CreateButton("Key");
            SetKeyButton.Tag = Id;
            SetKeyButton.Width = 25;
            SetKeyButton.Margin = new Thickness(2, 5, 2, 5);
            Grid.SetColumn(SetKeyButton, 3);
            AnimCtrlGrid.Children.Add(SetKeyButton);
        }

        private void MySlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            sliderValue = mySlider.Value;
        }
    }
}
