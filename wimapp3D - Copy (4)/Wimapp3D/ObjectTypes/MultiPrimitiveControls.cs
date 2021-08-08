using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace GameEngine
{
    class MultiPrimitiveControls : Window
    {
        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        public Grid RootGrid { get; private set; }
        public string TextboxName { get; set; } = "undefined";
        public double Rows { get; set; } = 20;
        public double Columns { get; set; } = 100;
        public double Pi { get; set; } = 3.14159265358979323846;
        public double WrapStart { get; set; } = 0;
        public double WrapEnd { get; set; } = 1.0;
        public double RowWrapStart { get; set; } = 0.0;
        public double RowWrapEnd { get; set; } = 1.0;
        public double Middle { get; set; } = 2;
        public double Roll { get; set; }
        public double SphereRadius { get; set; }
        public bool NeedsUpdate { get; set; } = false;
        public MySlider sliderRows = new MySlider(10, 2, 200, 1);
        public MySlider sliderColumns = new MySlider(10, 2, 200, 1);
        public MySlider sliderWrapStart = new MySlider(0, 0, 1, 0.01);
        public MySlider sliderWrapEnd = new MySlider(1, 0, 1, 0.01);
        public MySlider sliderRowWrapStart = new MySlider(0, 0, 1, 0.01);
        public MySlider sliderRowWrapEnd = new MySlider(1, 0, 1, 0.01);
        public MySlider sliderMiddle = new MySlider(0, 0, 5, 0.01);
        public MySlider sliderRoll = new MySlider(0, 0, 1, 0.01);
        public MySlider sliderRadius = new MySlider(1, 0, 1, 0.01);

        public MultiPrimitiveControls(MultiPrimitive inObject)
        {
            GotFocus += GotFocusMethod;
            GotKeyboardFocus += GotKeyFocusMethod;
            LostFocus += LostFocuMethod;
            LostKeyboardFocus += LostKeyFocusMethod;
            sliderRows.Value = inObject.Rows;
            sliderColumns.Value = inObject.Columns;
            sliderWrapStart.Value = inObject.WrapStart;
            sliderWrapEnd.Value = inObject.WrapEnd;
            sliderRowWrapStart.Value = inObject.RowWrapStart;
            sliderRowWrapEnd.Value = inObject.RowWrapEnd;
            sliderMiddle.Value = inObject.Middle;
            sliderRoll.Value = inObject.Roll;
            sliderRadius.Value = inObject.SphereRadius;
            Loaded += ToolWindow_Loaded;

            WindowStyle = WindowStyle.ThreeDBorderWindow;
            StackPanel ControlsStackPanel = new StackPanel();
            Grid.SetColumn(ControlsStackPanel, 0);
            Grid.SetRow(ControlsStackPanel, 0);
            RootGrid = new Grid()
            { HorizontalAlignment = HorizontalAlignment.Stretch, VerticalAlignment = VerticalAlignment.Stretch };
            Color color = Color.FromRgb((byte)10, (byte)50, (byte)100);
            Background = new SolidColorBrush(color);

            // Create a sqare grid with 20 pixel borders 
            RootGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(400) });
            RootGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(30) });

            RootGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(400) });

            Grid SliderGrid = new Grid();
            SliderGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(40) });
            SliderGrid.ColumnDefinitions.Add(new ColumnDefinition());

            Button Button_Close = CreateButton("Close");
            Button_Close.MaxWidth = 50;
            Button_Close.Click += Button_Close_Click;
            ControlsStackPanel.Children.Add(Button_Close);

            TextBlock objectName = new TextBlock();
            objectName.Text = inObject.Name;
            objectName.FontSize = 40;
            ControlsStackPanel.Children.Add(objectName);

            sliderRows.ValueChanged += new RoutedPropertyChangedEventHandler<double>(Sliders_ValueChanged);
            Grid sliderRowsGrid = MySlider.CreateSliderGrid(sliderRows, "rows");
            ControlsStackPanel.Children.Add(sliderRowsGrid);

            sliderColumns.ValueChanged += new RoutedPropertyChangedEventHandler<double>(Sliders_ValueChanged);
            Grid sliderColumnsGrid = MySlider.CreateSliderGrid(sliderColumns, "cols");
            ControlsStackPanel.Children.Add(sliderColumnsGrid);

            sliderWrapStart.ValueChanged += new RoutedPropertyChangedEventHandler<double>(Sliders_ValueChanged);
            Grid sliderWrapStartGrid = MySlider.CreateSliderGrid(sliderWrapStart, "Ystart");
            ControlsStackPanel.Children.Add(sliderWrapStartGrid);

            sliderWrapEnd.ValueChanged += new RoutedPropertyChangedEventHandler<double>(Sliders_ValueChanged);
            Grid sliderWrapEndGrid = MySlider.CreateSliderGrid(sliderWrapEnd, "Yend");
            ControlsStackPanel.Children.Add(sliderWrapEndGrid);

            sliderRowWrapStart.ValueChanged += new RoutedPropertyChangedEventHandler<double>(Sliders_ValueChanged);
            Grid sliderRowWrapStartGrid = MySlider.CreateSliderGrid(sliderRowWrapStart, "Xstart");
            ControlsStackPanel.Children.Add(sliderRowWrapStartGrid);

            sliderRowWrapEnd.ValueChanged += new RoutedPropertyChangedEventHandler<double>(Sliders_ValueChanged);
            Grid sliderRowWrapEndGrid = MySlider.CreateSliderGrid(sliderRowWrapEnd, "Xend");
            ControlsStackPanel.Children.Add(sliderRowWrapEndGrid);

            sliderMiddle.ValueChanged += new RoutedPropertyChangedEventHandler<double>(Sliders_ValueChanged);
            Grid sliderMiddleGrid = MySlider.CreateSliderGrid(sliderMiddle, "Mid");
            ControlsStackPanel.Children.Add(sliderMiddleGrid);

            sliderRoll.ValueChanged += new RoutedPropertyChangedEventHandler<double>(Sliders_ValueChanged);
            Grid sliderRollGrid = MySlider.CreateSliderGrid(sliderRoll, "Roll");
            ControlsStackPanel.Children.Add(sliderRollGrid);

            sliderRadius.ValueChanged += new RoutedPropertyChangedEventHandler<double>(Sliders_ValueChanged);
            Grid sliderRadiusGrid = MySlider.CreateSliderGrid(sliderRadius, "Radius");
            ControlsStackPanel.Children.Add(sliderRadiusGrid);
            //            public double SphereWrapStart { get; set; } = 0.0;
            //public double SphereWrapEnd { get; set; } = 1.0;
            //public double Middle { get; set; } = 2;
            //public double Roll { get; set; } = 1;
            //public double SphereRadius { get; set; } = 1;


            Button Button_presetTorus = CreateButton("Torus Preset");
            Button_presetTorus.Click += PresetTorus;
            ControlsStackPanel.Children.Add(Button_presetTorus);

            Button Button_presetSphere = CreateButton("Sphere Preset");
            Button_presetSphere.Click += PresetSphere;
            ControlsStackPanel.Children.Add(Button_presetSphere);



            RootGrid.Children.Add(ControlsStackPanel);
            Content = RootGrid;
            SizeToContent = SizeToContent.WidthAndHeight;
        }

        private void LostKeyFocusMethod(object sender, System.Windows.Input.KeyboardFocusChangedEventArgs e)
        {
            Color color = Color.FromRgb((byte)70, (byte)70, (byte)100);
            Background = new SolidColorBrush(color);
        }

        private void GotKeyFocusMethod(object sender, System.Windows.Input.KeyboardFocusChangedEventArgs e)
        {
            Color color = Color.FromRgb((byte)100, (byte)100, (byte)100);
            Background = new SolidColorBrush(color);
        }

        private void LostFocuMethod(object sender, RoutedEventArgs e)
        {
            Color color = Color.FromRgb((byte)200, (byte)255, (byte)0);
            Background = new SolidColorBrush(color);
        }

        private void GotFocusMethod(object sender, RoutedEventArgs e)
        {
            Color color = Color.FromRgb((byte)200, (byte)0, (byte)0);
            Background = new SolidColorBrush(color);
        }

        protected override void OnGotFocus(RoutedEventArgs e)
        {
            // TODO : Implement your code here.
            base.OnGotFocus(e);
            Color color = Color.FromRgb((byte)100, (byte)100, (byte)200);
            Background = new SolidColorBrush(color);

        }

        private void Sliders_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Rows = (int)sliderRows.Value;
            Columns = (int)sliderColumns.Value;
            WrapStart = sliderWrapStart.Value;
            WrapEnd = sliderWrapEnd.Value;
            RowWrapStart = sliderRowWrapStart.Value;
            RowWrapEnd = sliderRowWrapEnd.Value;
            Middle = sliderMiddle.Value;
            Roll = sliderRoll.Value;
            SphereRadius = sliderRadius.Value;
            NeedsUpdate = true;
        }

        private void ToolWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var hwnd = new System.Windows.Interop.WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
        }

        private static Button CreateButton(string inContent)
        {

            Button button = new Button();
            button.Margin = new Thickness(10, 5, 10, 5);
            button.MaxWidth = 100;
            button.Content = inContent;
            return button;
        }

        private void Button_Close_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }
        private void PresetSphere(object sender, RoutedEventArgs e)
        {
            sliderColumns.Value = 20;
            sliderRows.Value = 40;
            sliderWrapStart.Value = 0.0;
            sliderWrapEnd.Value = 1.0;
            RowWrapStart = 0.0;
            RowWrapEnd = 0.5;
            Middle = 0;
            NeedsUpdate = true;
        }
        private void PresetTorus(object sender, RoutedEventArgs e)
        {
            sliderColumns.Value = 20;
            sliderRows.Value = 40;
            sliderWrapStart.Value = 0.0;
            sliderWrapEnd.Value = 1.0;
            RowWrapStart = 0.0;
            RowWrapEnd = 1.0;
            Middle = 2;
            NeedsUpdate = true;
        }
    }
}
