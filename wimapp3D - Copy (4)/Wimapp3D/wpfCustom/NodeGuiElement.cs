using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace GameEngine
{
    class NodeGuiElement : Grid
    {
        private Grid RootGrid { get; set; } = new Grid();
        private TranslateTransform myTranslate = new TranslateTransform();
        public NodeGuiElement(RenderableGeo inObject)
        {
            Border border = new Border();
            border.BorderThickness = new Thickness(2, 2, 2, 2);
            border.BorderBrush = Brushes.Red;
            border.CornerRadius = new CornerRadius(15, 15, 15, 15);
            SetColumn(border, 0);
            SetRow(border, 0);
            SetColumnSpan(border, 3);
            SetRowSpan(border, 3);
            Children.Add(border);
            myTranslate.X = 15;
            myTranslate.Y = 15;
            RenderTransform = myTranslate;
            MouseLeftButtonDown += NodeGuiElement_MouseLeftButtonDown;
            Color color = Color.FromRgb((byte)80, (byte)80, (byte)70);
            Background = new SolidColorBrush(color);
            RowDefinitions.Add(new RowDefinition() { Height = new GridLength(20) });
            RowDefinitions.Add(new RowDefinition() { Height = new GridLength(50) });
            ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(70) });
            Button newBtn = new Button();
            newBtn.HorizontalAlignment = HorizontalAlignment.Center;
            newBtn.Width = 60;
            newBtn.Height = 30;
            newBtn.Content = inObject.Name;
            newBtn.Click += inObject.OpenProportiesButton;
            Grid.SetRow(newBtn, 1);
            Children.Add(newBtn);
            //Wimapp3D.MainWindow.AppWindow.MainWindowCanvas.Children.Add(newBtn);
        }

        private void NodeGuiElement_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
        }
        bool inDrag = false;
        Point anchorPoint;

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            anchorPoint = PointToScreen(e.GetPosition(this));
            inDrag = true;
            CaptureMouse();
            e.Handled = true;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (inDrag)
            {
                Point currentPoint = PointToScreen(e.GetPosition(this));
                myTranslate.X = myTranslate.X + currentPoint.X - anchorPoint.X;
                myTranslate.Y = myTranslate.Y + currentPoint.Y - anchorPoint.Y;
                RenderTransform = myTranslate;
                anchorPoint = currentPoint;
            }
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            if (inDrag)
            {
                ReleaseMouseCapture();
                inDrag = false;
                e.Handled = true;
            }
        }
    }
}
