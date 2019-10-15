using SudokuLogic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFTraining.Pages
{
    /// <summary>
    /// Interaction logic for SudokuPage.xaml
    /// </summary>
    public partial class SudokuPage4 : Page
    {
        public NumberGrid currentGrid { get; set; }
        public string currentValue { get; set; }
        public Stopwatch stopwatch { get; set; }
        public SudokuPage4()
        {
            InitializeComponent();

            currentGrid = new NumberGrid(new int[] {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 });

            NumberGrid numberGrid = new NumberGrid(new int[] {1,0,3,4,2,3,1,4,1,0,4,0,2,4,0,3 });

            Grid grid = baseGrid;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    TextBlock block = new TextBlock();
                    if (numberGrid.completeGrid[j].rowNum[i] != 0)
                    {
                        block.Text = "" + numberGrid.completeGrid[j].rowNum[i];
                        block.FontWeight = FontWeights.Bold;
                    }
                    else
                    {
                        block.Text = "X";
                        block.Foreground = new SolidColorBrush(Colors.White);
                        block.MouseDown += new MouseButtonEventHandler(TextBlock_MouseDown);
                    }
                    block.VerticalAlignment = VerticalAlignment.Center;
                    block.HorizontalAlignment = HorizontalAlignment.Center;
                    block.FontSize = 100;
                    
                    Grid.SetColumn(block, i);
                    Grid.SetRow(block, j);

                    Border border = new Border();
                    border.BorderThickness = new Thickness(0.5);
                    border.BorderBrush = new SolidColorBrush(Colors.Black);
                    Grid.SetColumn(border, i);
                    Grid.SetRow(border, j);

                    grid.Children.Add(block);
                    grid.Children.Add(border);
                }
            }

            for (int btnCount = 1; btnCount <= 4; btnCount++) {
                Button button = new Button();
                button.Content = "" + btnCount;
                button.Click += new RoutedEventHandler(Number_Click);
                button.Height = 50;
                button.Width = 50;
                Grid.SetColumn(button, btnCount - 1);
                buttonGrid.Children.Add(button);
            }
            Button buttonEraser = new Button();
            buttonEraser.Content = "X";
            buttonEraser.Click += new RoutedEventHandler(Number_Click);
            buttonEraser.Height = 50;
            buttonEraser.Width = 50;
            Grid.SetColumn(buttonEraser, 4);
            buttonGrid.Children.Add(buttonEraser);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Grid grid = baseGrid;
            UIElementCollection list = grid.Children;

            Console.WriteLine(currentGrid.ToString());
            Console.WriteLine("Bop");

            foreach (object item in list)
            {
                if (item is TextBlock)
                {
                    TextBlock textBlock = (TextBlock)item;
                    int value = 0;
                    int.TryParse(textBlock.Text, out value);
                    currentGrid.completeGrid[Grid.GetRow(textBlock)].rowNum[Grid.GetColumn(textBlock)] = value;
                }
            }

            Console.WriteLine(currentGrid.ToString());
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine(sender);
            var textblock = (TextBlock)sender;
            textblock.Text = currentValue;
            textblock.Foreground = new SolidColorBrush(Colors.Black);
            if (currentValue == "X")
            {
                Console.WriteLine("boo");
                textblock.Foreground = new SolidColorBrush(Colors.White);
            }
            
            Console.WriteLine(e);
        }

        private void Number_Click(object sender, RoutedEventArgs e) {
            Button button = (Button)sender;
            currentValue = "" + button.Content;
            valueTextBlock.Text = "Current value: " + currentValue;
        }

        private void Eraser_Click(object sender, RoutedEventArgs e)
        {
            currentValue = "X";
            valueTextBlock.Text = "Current value: " + currentValue;
        }


        private void StartTimer(object sender, RoutedEventArgs e)
        {
            stopwatch = new Stopwatch();
            stopwatch.Start();
        }

        private void StopTimer(object sender, RoutedEventArgs e)
        {
            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;

            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            MessageBox.Show("RunTime " + elapsedTime);

        }
    }
}
