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
    //class MultiPrimitiveControlGrid : Grid
    //{
    //    public string TextboxName { get; set; } = "undefined";
    //    public bool NeedsUpdate { get; set; } = false;

    //    MultiPrimitive MyInputObject;
    //    public StackPanel ControlsStackPanel;
    //    public MultiPrimitiveControlGrid(MultiPrimitive inObject)
    //    {
    //        MyInputObject = inObject;

    //        RowDefinitions.Add(new RowDefinition() { Height = new GridLength(38) });
    //        RowDefinitions.Add(new RowDefinition());
    //        Margin = new Thickness(5, 5, 5, 5);
    //        Color color = Color.FromRgb((byte)80, (byte)80, (byte)80);
    //        Background = new SolidColorBrush(color);
    //        Border border = new Border();
    //        border.BorderThickness = new Thickness(1, 1, 1, 1);
    //        border.BorderBrush = Brushes.Black;
    //        //border.CornerRadius = new CornerRadius(15, 15, 15, 15);
    //        SetColumn(border, 0);
    //        SetRow(border, 0);
    //        SetColumnSpan(border, 3);
    //        SetRowSpan(border, 3);
    //        Children.Add(border);

    //        TextBlock myName = new TextBlock();
    //        myName.Margin = new Thickness(5, 5, 5, 5);
    //        myName.Text = inObject.Name;
    //        myName.FontSize = 20;
    //        myName.Foreground = new SolidColorBrush(Color.FromRgb((byte)255, (byte)255, (byte)255));
    //        myName.HorizontalAlignment = HorizontalAlignment.Left;
    //        Grid.SetRow(myName, 0);
    //        Children.Add(myName);

    //        Button Button_Close = MyButton.CreateButton("X");
    //        Button_Close.HorizontalAlignment = HorizontalAlignment.Right;
    //        Button_Close.Width = 20;
    //        Button_Close.Height = 20;
    //        Button_Close.Click += Button_Close_Click;
    //        Children.Add(Button_Close);

    //        ControlsStackPanel = new StackPanel();
    //        //Grid.SetColumn(ControlsStackPanel, 0);
    //        Grid.SetRow(ControlsStackPanel, 1);



    //        //Button Button_presetTorus = MyButton.CreateButton("Torus Preset");
    //        //Button_presetTorus.Click += PresetTorus;
    //        //ControlsStackPanel.Children.Add(Button_presetTorus);

    //        //Button Button_presetSphere = MyButton.CreateButton("Sphere Preset");
    //        //Button_presetSphere.Click += PresetSphere;
    //        //ControlsStackPanel.Children.Add(Button_presetSphere);

    //        Children.Add(ControlsStackPanel);
    //    }
    //    private void Button_Close_Click(object sender, RoutedEventArgs e)
    //    {
    //        int index = Wimapp3D.MainWindow.AppWindow.ProportieWindowStack.Children.IndexOf(this);
    //        if (index >= 0)
    //        {
    //            Wimapp3D.MainWindow.AppWindow.ProportieWindowStack.Children.RemoveAt(index);
    //        }
    //    }
    //    //private void Sliders_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    //    //{
    //    //    MyInputObject.Rows = (int)sliderRows.Value;
    //    //    MyInputObject.Columns = (int)sliderColumns.Value;
    //    //    MyInputObject.WrapStart = sliderWrapStart.Value;
    //    //    MyInputObject.WrapEnd = sliderWrapEnd.Value;
    //    //    MyInputObject.RowWrapStart = sliderRowWrapStart.Value;
    //    //    MyInputObject.RowWrapEnd = sliderRowWrapEnd.Value;
    //    //    MyInputObject.Middle = sliderMiddle.Value;
    //    //    MyInputObject.Roll = sliderRoll.Value;
    //    //    MyInputObject.SphereRadius = sliderRadius.Value;
    //    //    NeedsUpdate = true;
    //    //}
    //    //private void PresetSphere(object sender, RoutedEventArgs e)
    //    //{
    //    //    //sliderColumns.Value = 20;
    //    //    //sliderRows.Value = 40;
    //    //    sliderWrapStart.Value = 0.0;
    //    //    sliderWrapEnd.Value = 1.0;
    //    //    sliderRowWrapStart.Value = 0.0;
    //    //    sliderRowWrapEnd.Value = 0.5;
    //    //    sliderMiddle.Value = 0;
    //    //    //NeedsUpdate = true;
    //    //}
    //    //private void PresetTorus(object sender, RoutedEventArgs e)
    //    //{
    //    //    //sliderColumns.Value = 20;
    //    //    //sliderRows.Value = 40;
    //    //    sliderWrapStart.Value = 0.0;
    //    //    sliderWrapEnd.Value = 1.0;
    //    //    sliderRowWrapStart.Value = 0.0;
    //    //    sliderRowWrapEnd.Value = 1.0;
    //    //    sliderMiddle.Value = 2;
    //    //    //NeedsUpdate = true;
    //    //}
    //}
}
