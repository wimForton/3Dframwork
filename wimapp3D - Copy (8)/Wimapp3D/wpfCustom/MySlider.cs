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
    public class MySlider : Slider

    {

        private Thumb _thumb = null;

        public MySlider(double inValue, double inMinimum, double inMaximum, double inTickFrequency)
        {
            Color color = Color.FromRgb((byte)70, (byte)70, (byte)70);
            Background = new SolidColorBrush(color);
            //Border border = new Border();
            //border.BorderThickness = new Thickness(1, 1, 1, 1);
            //border.BorderBrush = Brushes.Black;
            //border.CornerRadius = new CornerRadius(4, 4, 4, 4);
            //Add(border);
            Margin = new Thickness(10, 5, 10, 5);
            Value = inValue;
            Minimum = inMinimum;
            Maximum = inMaximum;
            SmallChange = 1;
            LargeChange = 1;
            TickFrequency = inTickFrequency;
            IsSnapToTickEnabled = true;
            IsMoveToPointEnabled = true;
        }
        public static Grid CreateSliderGrid(Slider mySlider, string name)
        {
            mySlider.Name = "name";
            Grid myGrid = new Grid();
            myGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(70) });
            myGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(60) });
            myGrid.ColumnDefinitions.Add(new ColumnDefinition());
            Color color = Color.FromRgb((byte)90, (byte)90, (byte)90);
            myGrid.Background = new SolidColorBrush(color);
            myGrid.Margin = new Thickness(5, 5, 5, 5);
            Border border = new Border();
            border.BorderThickness = new Thickness(1, 1, 1, 1);
            border.BorderBrush = Brushes.Black;
            border.CornerRadius = new CornerRadius(4, 4, 4, 4);

            Grid.SetColumn(border, 0);
            Grid.SetRow(border, 0);
            Grid.SetColumnSpan(border, 3);
            Grid.SetRowSpan(border, 3);
            myGrid.Children.Add(border);

            TextBlock myName = new TextBlock();
            myName.Margin = new Thickness(5, 5, 5, 5);
            myName.Text = name;
            myName.FontSize = 16;
            myName.Foreground = new SolidColorBrush(Color.FromRgb((byte)255, (byte)255, (byte)255));
            TextBox text = new TextBox();
            Grid.SetColumn(text, 1);
            Grid.SetColumn(mySlider, 2);
            Grid.SetRow(text, 0);
            text.Width = 50;
            text.Margin = new Thickness(5, 5, 5, 5);
            Binding b = new Binding();
            b.Source = mySlider;
            b.Path = new PropertyPath("Value", mySlider.Value);
            b.Mode = BindingMode.TwoWay;
            b.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            text.SetBinding(TextBox.TextProperty, b);
            myGrid.Children.Add(myName);
            myGrid.Children.Add(text);
            myGrid.Children.Add(mySlider);

            return myGrid;
        }

        public override void OnApplyTemplate()

        {
            base.OnApplyTemplate();
            if (_thumb != null)
            {
                _thumb.MouseEnter -= thumb_MouseEnter;
            }
            _thumb = (GetTemplateChild("PART_Track") as Track).Thumb;
            if (_thumb != null)
            {
                _thumb.MouseEnter += thumb_MouseEnter;
            }
        }
        private void thumb_MouseEnter(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                // the left button is pressed on mouse enter
                // so the thumb must have been moved under the mouse
                // in response to a click on the track.
                // Generate a MouseLeftButtonDown event.
                MouseButtonEventArgs args = new MouseButtonEventArgs(
                    e.MouseDevice, e.Timestamp, MouseButton.Left);
                args.RoutedEvent = MouseLeftButtonDownEvent;
                (sender as Thumb).RaiseEvent(args);
            }
        }
    }
}
