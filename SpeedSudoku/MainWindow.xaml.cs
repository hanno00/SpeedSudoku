using SpeedSudoku.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
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
using WPFTraining.Pages;

namespace SpeedSudoku
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Content = new InlogPage(this);

            Thread gameThread = new Thread(() => GameThread(this));
            gameThread.Start();
        }

        static void GameThread(MainWindow main) {
            TcpClient client = new TcpClient("127.0.0.1", 5555);
            NetworkStream stream = client.GetStream();

            byte[] buffer = new byte[4];
            int incomingBytes = stream.Read(buffer, 0, buffer.Length);

            int packetLength = BitConverter.ToInt32(buffer, 0);

            byte[] totalBuffer = new byte[packetLength];
            int msgPosition = 0;
            while (msgPosition < packetLength)
            {
                incomingBytes = stream.Read(totalBuffer, msgPosition, packetLength - msgPosition);
                msgPosition += incomingBytes;
            }
            string json = Encoding.UTF8.GetString(totalBuffer, 0, packetLength);
            Console.WriteLine("Json\n" + json);

            Application.Current.Dispatcher.Invoke(new Action(() => { main.switchToLobbyPage(); }));
            while (true) { }



        }

        public void switchToSudoku()
        {
            switchContent(new ResultPage());
        }

        public void switchToLobbyPage()
        {
            switchContent(new LobbyPage());
        }

        public void switchContent(Page page)
        {
            MainFrame.Content = page;
        }
    }
}
