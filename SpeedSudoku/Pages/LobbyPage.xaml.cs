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
    /// Interaction logic for LobbyPage.xaml
    /// </summary>
    public partial class LobbyPage : Page
    {
        public LobbyPage()
        {
            InitializeComponent();
            opponentStatus.Text = "Waiting for opponent";
            var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(2) };
            timer.Start();
            timer.Tick += (sender, args) =>
            {
                timer.Stop();
                opponentStatus.Text = "Opponent Found";
                TextBlock textBlock = new TextBlock();
                textBlock.Text = "Ready up! (1/2)";
                textBlock.FontSize = 50;
                textBlock.HorizontalAlignment = HorizontalAlignment.Center;
                textBlock.VerticalAlignment = VerticalAlignment.Center;
                textBlock.Margin = new Thickness(20);

                Button button = new Button();
                button.Content = "Ready";
                button.HorizontalAlignment = HorizontalAlignment.Center;
                button.VerticalAlignment = VerticalAlignment.Center;
                button.Height = 100;
                button.Width = 200;
                button.FontSize = 45;

                stackPanel.Children.Add(textBlock);
                stackPanel.Children.Add(button);
            };
        }
    }
}
