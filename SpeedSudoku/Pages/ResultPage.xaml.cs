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
        public MainWindow main;
        public Button button;
        public ResultPage(MainWindow mainwindow)
        {
            InitializeComponent();
            main = mainwindow;
            button = new Button();
            button.Height = 50;
            button.Width = 200;
            button.Content = "Go Again!";
            button.FontSize = 20;
            button.Click += new RoutedEventHandler(Button_Click);
            winStatus.Text = "Waiting for results!";

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                main.switchToLobbyPage();
            }));
        }
    }
}
