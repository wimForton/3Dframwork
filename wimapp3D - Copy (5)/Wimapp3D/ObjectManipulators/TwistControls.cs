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
    class TwistControls : Window
    {
        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        public Grid RootGrid { get; private set; }
        public string TextboxName { get; set; } = "undefined";

        public bool NeedsUpdate { get; set; } = false;
        public MySlider sliderTwist = new MySlider(1, 0, 20, 0.2);
        Twist MyInputObject;

        public TwistControls(Twist inObject)
        {
            MyInputObject = inObject;
            GotFocus += GotFocusMethod;
            GotKeyboardFocus += GotKeyFocusMethod;
            LostFocus += LostFocuMethod;
            LostKeyboardFocus += LostKeyFocusMethod;
            sliderTwist.Value = inObject.TwistAmount;
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

            sliderTwist.ValueChanged += new RoutedPropertyChangedEventHandler<double>(Sliders_ValueChanged);
            Grid sliderTwistGrid = MySlider.CreateSliderGrid(sliderTwist, "rows");
            ControlsStackPanel.Children.Add(sliderTwistGrid);

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

            MyInputObject.TwistAmount = sliderTwist.Value;

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

    }
}
