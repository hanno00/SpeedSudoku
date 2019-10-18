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
using System.Windows.Threading;

namespace SpeedSudoku.Pages
{
    /// <summary>
    /// Interaction logic for ResultPage.xaml
    /// </summary>
    public partial class ResultPage : Page
    {
        public ResultPage()
        {
            InitializeComponent();
            winStatus.Text = "Waiting for results!";
            var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(2) };
            timer.Start();
            timer.Tick += (sender, args) =>
            {
                timer.Stop();
                Random random = new Random();
                if (random.Next(0, 2) == 1)
                {
                    winStatus.Text = "You won!";
                    winStatus.FontSize = 50;
                }
                else {
                    winStatus.Text = "You lost :(";
                    winStatus.FontSize = 50;
                }

            };
        }
    }
}
