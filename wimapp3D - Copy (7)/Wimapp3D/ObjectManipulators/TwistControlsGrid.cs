
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
    class TwistControlsGrid : Grid
    {
        public string TextboxName { get; set; } = "undefined";
        public bool NeedsUpdate { get; set; } = false;
        public MySlider sliderTwist = new MySlider(0, -8, 8, 0.2);
        public KeyFrameSlider myKeyFrameSlider = new KeyFrameSlider("testslider", 0, 0, 10, 0.5);
        Twist MyInputObject;
        public TwistControlsGrid(Twist inObject)
        {
            MyInputObject = inObject;
            RowDefinitions.Add(new RowDefinition() { Height = new GridLength(38) });
            RowDefinitions.Add(new RowDefinition());
            Margin = new Thickness(5, 5, 5, 5);
            Color color = Color.FromRgb((byte)80, (byte)80, (byte)80);
            Background = new SolidColorBrush(color);
            Border border = new Border();
            border.BorderThickness = new Thickness(1, 1, 1, 1);
            border.BorderBrush = Brushes.Black;
            //border.CornerRadius = new CornerRadius(15, 15, 15, 15);
            SetColumn(border, 0);
            SetRow(border, 0);
            SetColumnSpan(border, 3);
            SetRowSpan(border, 3);
            Children.Add(border);

            TextBlock myName = new TextBlock();
            myName.Margin = new Thickness(5, 5, 5, 5);
            myName.Text = inObject.Name;
            myName.FontSize = 20;
            myName.Foreground = new SolidColorBrush(Color.FromRgb((byte)255, (byte)255, (byte)255));
            myName.HorizontalAlignment = HorizontalAlignment.Left;
            Grid.SetRow(myName, 0);
            Children.Add(myName);

            Button Button_Close = MyButton.CreateButton("X");
            Button_Close.HorizontalAlignment = HorizontalAlignment.Right;
            Button_Close.Width = 20;
            Button_Close.Height = 20;
            Button_Close.Click += Button_Close_Click;
            Children.Add(Button_Close);

            StackPanel ControlsStackPanel = new StackPanel();
            //Grid.SetColumn(ControlsStackPanel, 0);
            Grid.SetRow(ControlsStackPanel, 1);

            sliderTwist.ValueChanged += new RoutedPropertyChangedEventHandler<double>(Sliders_ValueChanged);
            Grid sliderTwistGrid = MySlider.CreateSliderGrid(sliderTwist, "twist");
            ControlsStackPanel.Children.Add(sliderTwistGrid);

            
            ControlsStackPanel.Children.Add(myKeyFrameSlider);

            Children.Add(ControlsStackPanel);
        }
        private void Sliders_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            MyInputObject.TwistAmount = sliderTwist.Value;
            NeedsUpdate = true;
        }
        private void Button_Close_Click(object sender, RoutedEventArgs e)
        {
            int index = Wimapp3D.MainWindow.AppWindow.ProportieWindowStack.Children.IndexOf(this);
            if (index >= 0)
            {
                Wimapp3D.MainWindow.AppWindow.ProportieWindowStack.Children.RemoveAt(index);
            }
        }

    }
}
