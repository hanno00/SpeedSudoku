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
            //var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(2) };
            //timer.Start();
            //timer.Tick += (sender, args) =>
            //{
            //    timer.Stop();
            //};
        }

        private void ReadyButton_Click(object sender, RoutedEventArgs e)
        {
            TextBlock text = new TextBlock
            {
                Text = "You are now ready",
                FontSize = 30,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(10)
            };
            stackPanel.Children.Remove((UIElement)stackPanel.FindName("readyButton"));
            stackPanel.Children.Add(text);

        }
    }


}
