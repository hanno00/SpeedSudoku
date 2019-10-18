using SpeedSudoku;
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
    public partial class SudokuPage6 : Page
    {
        public NumberGrid currentGrid { get; set; }
        public string currentValue { get; set; }
        public Stopwatch stopwatch { get; set; }
        public SudokuPage6()
        {
            InitializeComponent();

            currentGrid = new NumberGrid(new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 });

            NumberGrid numberGrid = new NumberGrid(new int[] { 3, 0, 1, 0, 2, 4, 2, 6, 4, 0, 1, 3, 4, 1, 5, 0, 6, 2, 0, 3, 2, 1, 0, 5, 5, 2, 6, 4, 3, 1, 1, 4, 3, 2, 5, 6 });

            stopwatch = new Stopwatch();
            stopwatch.Start();

            Grid grid = baseGrid;
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
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
                    block.FontSize = 80;

                    Grid.SetColumn(block, i);
                    Grid.SetRow(block, j);

                    Border border = new Border
                    {
                        BorderThickness = new Thickness(1),
                        BorderBrush = new SolidColorBrush(Colors.Black)
                    };
                    Grid.SetColumn(border, i);
                    Grid.SetRow(border, j);

                    grid.Children.Add(block);
                    grid.Children.Add(border);
                }
            }

            for (int btnCount = 1; btnCount <= 6; btnCount++)
            {
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
            Grid.SetColumn(buttonEraser, 6);
            buttonGrid.Children.Add(buttonEraser);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Grid grid = baseGrid;
            UIElementCollection list = grid.Children;

            foreach (object item in list)
            {
                if (item is TextBlock)
                {
                    TextBlock textBlock = (TextBlock)item;
                    int.TryParse(textBlock.Text, out int value);
                    currentGrid.completeGrid[Grid.GetRow(textBlock)].rowNum[Grid.GetColumn(textBlock)] = value;
                }
            }

            if (Logic.checkCompleteGrid(currentGrid))
            {
                stopwatch.Stop();
                TimeSpan ts = stopwatch.Elapsed;

                // Format and display the TimeSpan value.
                string elapsedTime = String.Format("{0:00}:{1:00}.{2:00}",
                    ts.Minutes, ts.Seconds,
                    ts.Milliseconds / 10);
                MessageBox.Show("Je hebt de Sudoku opgelost!\nJe tijd is: " + elapsedTime);
            }
            else
            {
                MessageBox.Show("Je hebt de Sudoku nog niet goed opgelost.");
            }

        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var textblock = (TextBlock)sender;
            textblock.Text = currentValue;
            textblock.Foreground = new SolidColorBrush(Colors.Black);
            if (currentValue == "X")
            {
                textblock.Foreground = new SolidColorBrush(Colors.White);
            }
        }

        private void Number_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            currentValue = "" + button.Content;
            valueTextBlock.Text = "Current value: " + currentValue;
        }
    }
}
