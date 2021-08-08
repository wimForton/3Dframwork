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
    class TimeSlider : DockPanel
    {
        //double Time;
        MySlider sliderTime = new MySlider(0,0,200,1);

        public TimeSlider()
        {
            //ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
            //RowDefinitions.Add(new RowDefinition());
            Margin = new Thickness(4, 4, 4, 4);
            //Color color = Color.FromRgb((byte)30, (byte)30, (byte)80);
            //Background = new SolidColorBrush(color);
            Border border = new Border();
            border.BorderThickness = new Thickness(1, 1, 1, 1);
            border.BorderBrush = Brushes.Black;
            border.CornerRadius = new CornerRadius(15, 15, 15, 15);
            //SetColumn(border, 0);
            //SetRow(border, 0);
            //SetColumnSpan(border, 3);
            //SetRowSpan(border, 3);
            Children.Add(border);
            sliderTime.TickPlacement = System.Windows.Controls.Primitives.TickPlacement.BottomRight;
            sliderTime.ValueChanged += new RoutedPropertyChangedEventHandler<double>(Sliders_ValueChanged);
            //Grid sliderTimeGrid = MySlider.CreateSliderGrid(sliderTime, "Time");
            Children.Add(sliderTime);
        }

        private void Sliders_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //Wimapp3D.MainWindow.AppWindow.TimeText.Text = "FrameNr: " + Convert.ToString(sliderTime.Value);
            AnimationTime.Instance.Time = Math.Round(sliderTime.Value) / 25;
        }
    }
}
