using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;


namespace GameEngine
{
    class PolyObjectLoaderControls : Window
    {
        public Grid RootGrid { get; private set; }
        public string FilePath { get; set; } = "undefined";
        public bool NeedsUpdate { get; set; } = false;
        TextBlock filePathTextBlock = new TextBlock();
        public PolyObjectLoaderControls()
        {
            WindowStyle = WindowStyle.ThreeDBorderWindow;
            StackPanel SliderStackPanel = new StackPanel();
            Grid.SetColumn(SliderStackPanel, 0);
            Grid.SetRow(SliderStackPanel, 0);
            RootGrid = new Grid()
            { HorizontalAlignment = HorizontalAlignment.Stretch, VerticalAlignment = VerticalAlignment.Stretch };

            // Create a sqare grid with 20 pixel borders 
            RootGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(100) });
            RootGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(30) });

            RootGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(700) });
            filePathTextBlock.Text = FilePath;
            filePathTextBlock.MaxHeight = 30;
            SliderStackPanel.Children.Add(filePathTextBlock);

            Button Button_LoadFile = new Button() { Content = "Load File" };
            Button_LoadFile.Click += LoadFile;
            SliderStackPanel.Children.Add(Button_LoadFile);

            RootGrid.Children.Add(SliderStackPanel);
            // Add the RootGrid to the content of the window
            Content = RootGrid;
            // fit the window size to the size of the RootGrid
            SizeToContent = SizeToContent.WidthAndHeight;
        }

        private void LoadFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                FilePath = openFileDialog.FileName;
                filePathTextBlock.Text = FilePath;
                NeedsUpdate = true;
            }

                //txtEditor.Text = File.ReadAllText(openFileDialog.FileName);
        }
    }
}
