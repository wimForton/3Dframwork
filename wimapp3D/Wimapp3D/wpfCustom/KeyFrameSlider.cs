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
    class KeyFrameSlider : Grid
    {

        private AnimatableParameter MyAnimatableParameter = new AnimatableParameter();
        private MySlider mySlider;
        public KeyFrameSlider(string name, double inValue, double inMinimum, double inMaximum, double inTickFrequency)
        {
            //Grid myGrid = new Grid();
            ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(70) });
            ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(60) });
            ColumnDefinitions.Add(new ColumnDefinition());
            ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(40) });
            Color color = Color.FromRgb((byte)90, (byte)90, (byte)90);
            Background = new SolidColorBrush(color);
            Margin = new Thickness(5, 5, 5, 5);
            Border border = new Border();
            border.BorderThickness = new Thickness(1, 1, 1, 1);
            border.BorderBrush = Brushes.Black;
            border.CornerRadius = new CornerRadius(4, 4, 4, 4);

            Grid.SetColumn(border, 0);
            Grid.SetRow(border, 0);
            Grid.SetColumnSpan(border, 4);
            Grid.SetRowSpan(border, 4);
            Children.Add(border);

            TextBlock myName = new TextBlock();
            myName.Margin = new Thickness(5, 5, 5, 5);
            myName.Text = name;
            myName.FontSize = 16;
            myName.Foreground = new SolidColorBrush(Color.FromRgb((byte)255, (byte)255, (byte)255));

            AnimationTime.Instance.PropertyChanged += Instance_PropertyChanged;

            TextBox text = new TextBox();
            Grid.SetColumn(text, 1);
            Grid.SetRow(text, 0);
            text.Width = 50;
            text.Margin = new Thickness(5, 5, 5, 5);

            mySlider = new MySlider(inValue, inMinimum, inMaximum, inTickFrequency);
            mySlider.ValueChanged += MySlider_ValueChanged;
            Grid.SetColumn(mySlider, 2);
            mySlider.Name = "name";
            Binding b = new Binding();
            b.Source = mySlider;
            b.Path = new PropertyPath("Value", mySlider.Value);
            b.Mode = BindingMode.TwoWay;
            b.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            text.SetBinding(TextBox.TextProperty, b);
            Children.Add(myName);
            Children.Add(text);
            Children.Add(mySlider);

            

            Button SetKeyButton = MyButton.CreateButton("Key");
            SetKeyButton.Click += SetKeyFrame;
            Grid.SetColumn(SetKeyButton, 3);
            Children.Add(SetKeyButton);
        }

        private void Instance_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            //MessageBox.Show("time changed!");
            //Wimapp3D.MainWindow.AppWindow.TimeText.Text = "FrameNr: " + Convert.ToString(AnimationTime.Instance.Time);
            string testtext = "";
            foreach (var item in MyAnimatableParameter.indexList)
            {
                testtext += "key";
                testtext += Convert.ToString(item);
            }
            Wimapp3D.MainWindow.AppWindow.TimeText.Text = testtext;
            // work with pair.Key and pair.Value
            //Wimapp3D.MainWindow.AppWindow.TimeText.Text = "k: " + Convert.ToString("");

            mySlider.Value = MyAnimatableParameter.GetValueAtTime(AnimationTime.Instance.Time);
        }

        private void MySlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            MyAnimatableParameter.SetKeyAtTime(mySlider.Value, AnimationTime.Instance.Time);
        }

        public double Value
        {
            get
            {
                return MyAnimatableParameter.GetValueAtTime(AnimationTime.Instance.Time);
            }
            set
            {
                //MyAnimatableParameter.SetKeyAtTime(value, AnimationTime.Instance.Time);
            }
            
            //return MyAnimatableParameter.GetValueAtTime(inTime);
        }
        public Slider CreateSlider(double inValue, double inMinimum, double inMaximum, double inTickFrequency)
        {
            Slider mySlider = new Slider();
            Color color = Color.FromRgb((byte)70, (byte)70, (byte)70);
            mySlider.Background = new SolidColorBrush(color);
            mySlider.Margin = new Thickness(10, 5, 10, 5);
            mySlider.Value = inValue;
            mySlider.Minimum = inMinimum;
            mySlider.Maximum = inMaximum;
            mySlider.SmallChange = 1;
            mySlider.LargeChange = 1;
            mySlider.TickFrequency = inTickFrequency;
            mySlider.IsSnapToTickEnabled = true;
            mySlider.IsMoveToPointEnabled = true;
            return mySlider;
        }
        private static void SetKeyFrame(object sender, RoutedEventArgs e)
        {
            //MyAnimatableParameter.SetKeyAtTime
        }
    }
}
