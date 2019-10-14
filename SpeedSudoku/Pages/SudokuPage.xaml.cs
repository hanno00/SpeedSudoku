using SudokuLogic;
using System;
using System.Collections.Generic;
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
    public partial class SudokuPage : Page
    {
        public SudokuPage()
        {
            InitializeComponent();
            NumberGrid numberGrid4 = new NumberGrid(true);
            numberGrid4.completeGrid[0] = new Row(true, new int[] { 1, 2, 3, 4 });
            numberGrid4.completeGrid[1] = new Row(true, new int[] { 5, 6, 7, 8 });
            numberGrid4.completeGrid[2] = new Row(true, new int[] { 9, 10, 11, 12 });
            numberGrid4.completeGrid[3] = new Row(true, new int[] { 13, 14, 15, 16 });

            NumberGrid numberGrid6 = new NumberGrid(false);
            numberGrid6.completeGrid[0] = new Row(false, new int[] { 1, 2, 3, 4, 5, 6 });
            numberGrid6.completeGrid[1] = new Row(false, new int[] { 7, 8, 9, 9, 9, 9 });
            numberGrid6.completeGrid[2] = new Row(false, new int[] { 1, 2, 3, 4, 5, 6 });
            numberGrid6.completeGrid[3] = new Row(false, new int[] { 7, 8, 9, 9, 9, 9 });
            numberGrid6.completeGrid[4] = new Row(false, new int[] { 1, 2, 3, 4, 5, 6 });
            numberGrid6.completeGrid[5] = new Row(false, new int[] { 7, 8, 9, 9, 9, 9 });

            var numberGrid = numberGrid6;
            Grid grid = baseGrid;


            for (int i = 0; i < numberGrid.completeGrid.Length; i++)
            {
                ColumnDefinition column = new ColumnDefinition();
                column.Width = new GridLength(20, GridUnitType.Star);
                grid.ColumnDefinitions.Add(column);
                RowDefinition row = new RowDefinition();
                row.Height = new GridLength(20, GridUnitType.Star);
                grid.RowDefinitions.Add(row);
                for (int j = 0; j < numberGrid.completeGrid.Length; j++)
                {
                    TextBlock block = new TextBlock();
                    block.Text = "" + numberGrid.completeGrid[j].gridNum[i];
                    block.VerticalAlignment = VerticalAlignment.Center;
                    block.HorizontalAlignment = HorizontalAlignment.Center;
                    block.FontSize = 24;
                    Grid.SetColumn(block, i);
                    Grid.SetRow(block, j);

                    Border border = new Border();
                    border.BorderThickness = new Thickness(1);
                    border.BorderBrush = new SolidColorBrush(Colors.Black);
                    Grid.SetColumn(border, i);
                    Grid.SetRow(border, j);

                    grid.Children.Add(block);
                    grid.Children.Add(border);
                }
            }
        }
    }
}
