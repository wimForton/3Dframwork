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
using System.Windows.Shapes;

namespace GameEngine
{
    class NodeGuiElement : Grid
    {
        public static bool connectionDragging = false;

        private Grid RootGrid { get; set; } = new Grid();
        public TranslateTransform myTranslate = new TranslateTransform();
        //public DrawConnection myConnection;
        private IRenderableGeo myRenderableObject;
        public NodeGuiElement(IRenderableGeo inObject)
        {
            myRenderableObject = inObject;
            Border border = new Border();
            border.BorderThickness = new Thickness(2, 2, 2, 2);
            border.BorderBrush = Brushes.LightGray;
            //border.CornerRadius = new CornerRadius(15, 15, 15, 15);
            SetColumn(border, 0);
            SetRow(border, 0);
            SetColumnSpan(border, 3);
            SetRowSpan(border, 3);
            Children.Add(border);
            myTranslate.X = 15;
            myTranslate.Y = 15;
            RenderTransform = myTranslate;
            myRenderableObject.GuiNodePosition = new Vector(myTranslate.X, myTranslate.Y, 0);
            MouseLeftButtonDown += NodeGuiElement_MouseLeftButtonDown;
            Color color = Color.FromRgb((byte)70, (byte)70, (byte)110);
            Background = new SolidColorBrush(color);

            RowDefinitions.Add(new RowDefinition() { Height = new GridLength(30) });
            RowDefinitions.Add(new RowDefinition() { Height = new GridLength(35) });
            RowDefinitions.Add(new RowDefinition() { Height = new GridLength(30) });
            ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(70) });
            Button newBtn = new Button();
            newBtn.HorizontalAlignment = HorizontalAlignment.Center;
            newBtn.Width = 60;
            newBtn.Height = 20;
            newBtn.Content = inObject.Name;
            newBtn.Click += ButtonOpenProperties;//inObject.OpenProportiesButton;
            Grid.SetRow(newBtn, 1);
            Children.Add(newBtn);

            if (inObject.isRootGeoNode)
            {
                color = Color.FromRgb((byte)110, (byte)110, (byte)50);
                Background = new SolidColorBrush(color);
            }
            else
            {
                Button inBtn = new Button();
                inBtn.HorizontalAlignment = HorizontalAlignment.Center;
                inBtn.Width = 25;
                inBtn.Height = 20;
                inBtn.Content = "in";
                inBtn.Click += ButtonInputProperties;
                Grid.SetRow(inBtn, 0);
                Children.Add(inBtn);
            }


            Button outBtn = new Button();
            outBtn.HorizontalAlignment = HorizontalAlignment.Center;
            outBtn.Width = 25;
            outBtn.Height = 20;
            outBtn.Content = "out";
            outBtn.Click += ButtonOutputProperties;
            Grid.SetRow(outBtn, 2);
            Children.Add(outBtn);
            Wimapp3D.MainWindow.AppWindow.UpdateCanvas();
        }

        private void ButtonOutputProperties(object sender, RoutedEventArgs e)
        {
            if (IRenderableGeo.ChildLookingForGeoParent != null && myRenderableObject.ChildGeoNodes.IndexOf(IRenderableGeo.ChildLookingForGeoParent) < 0) 
            {
                //first remove connections, remove from child list of current parent
                if (IRenderableGeo.ChildLookingForGeoParent.InputObject != null) {

                    int index = IRenderableGeo.ChildLookingForGeoParent.InputObject.ChildGeoNodes.IndexOf(IRenderableGeo.ChildLookingForGeoParent);
                    if(index >= 0) {
                        IRenderableGeo.ChildLookingForGeoParent.InputObject.ChildGeoNodes.RemoveAt(index);  
                    }
                }
                //then create, replace
                IRenderableGeo.ChildLookingForGeoParent.InputObject = myRenderableObject;
                myRenderableObject.ChildGeoNodes.Add(IRenderableGeo.ChildLookingForGeoParent);
                Wimapp3D.MainWindow.AppWindow.UpdateCanvas();
                IRenderableGeo.ChildLookingForGeoParent.NeedsUpdate = true;
                IRenderableGeo.ChildLookingForGeoParent = null;
            }            
        }

        private void ButtonInputProperties(object sender, RoutedEventArgs e)
        {
            IRenderableGeo.ChildLookingForGeoParent = myRenderableObject;
        }

        private void ButtonOpenProperties(object sender, RoutedEventArgs e)
        {
            myRenderableObject.OpenProportiesWindow();
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
                myRenderableObject.GuiNodePosition = new Vector(myTranslate.X, myTranslate.Y, 0);
                Wimapp3D.MainWindow.AppWindow.UpdateCanvas();
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
