using Newtonsoft.Json;
using SpeedSudoku.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
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
    /// 

    public partial class MainWindow : Window
    {
        public bool readyForLobby = false;
        public bool readyForGame = false;
        public bool readyForResult = false;
        public static string username = Guid.NewGuid().ToString();
        public static ResultPage resultPage;

        public static TcpClient client;
        public static NetworkStream stream;
        public MainWindow()
        {
            InitializeComponent();
            resultPage = new ResultPage(this);
            Console.WriteLine(username);

            MainFrame.Content = new InlogPage(this);

            client = new TcpClient("127.0.0.1", 5555);
            stream = client.GetStream();


            Thread listenThread = new Thread(() => ListenThread(this, stream));
            listenThread.Start();
        }

        static void ListenThread(MainWindow main, NetworkStream stream)
        {

            while (true)
            {
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

                dynamic deserialized = JsonConvert.DeserializeObject(json);
                Console.WriteLine(deserialized);
                if (deserialized != null)
                {
                    switch ((string)deserialized.id)
                    {
                        case "server/sendSudoku":

                            char[] data = ((string)deserialized.data).ToCharArray();
                            int[] dataInt = new int[data.Length];
                            for (int i = 0; i < data.Length; i++)
                            {
                                dataInt[i] = int.Parse(data[i].ToString());
                            }
                            if (dataInt.Length == 16)
                            {
                                Application.Current.Dispatcher.Invoke(new Action(() => { main.switchToSudoku4(dataInt); }));
                            }
                            else if (dataInt.Length == 36)
                            {
                                Application.Current.Dispatcher.Invoke(new Action(() => { main.switchToSudoku6(dataInt); }));
                            }

                            break;
                        case "server/sendWinner":
                            if (String.Equals((string)deserialized.data, username))
                            {
                                Application.Current.Dispatcher.Invoke(new Action(() => { 
                                    resultPage.winStatus.Text = "You won!";
                                    resultPage.stackPanel.Children.Add(resultPage.button);

                                }));
                            }
                            else
                            {
                                Application.Current.Dispatcher.Invoke(new Action(() => { 
                                    resultPage.winStatus.Text = "You Lost :(";
                                    resultPage.stackPanel.Children.Add(resultPage.button);
                                }));
                            }
                            break;
                        default:

                            break;
                    }
                }
            }
        }

        public void switchToSudoku4(int[] values)
        {
            switchContent(new SudokuPage4(values, this));
        }

        public void switchToSudoku6(int[] values)
        {
            switchContent(new SudokuPage6(values, this));
        }

        public void switchToLobbyPage()
        {
            switchContent(new LobbyPage(this));
        }
        public void switchToResultPage()
        {
            resultPage = new ResultPage(this);
            switchContent(resultPage);
        }

        public void switchContent(Page page)
        {
            MainFrame.Content = page;
        }

        public void sendReady()
        {
            sendAction(JsonConvert.SerializeObject(new
            {
                id = "player/ready",
                data = username
            }));
        }

        public void sendTime(TimeSpan timeSpan)
        {
            sendAction(JsonConvert.SerializeObject(new
            {
                id = "player/time",
                username = username,
                data = timeSpan
            }));
        }

        public static void sendAction(string json)
        {
            Console.WriteLine(json);
            byte[] prependBytes = BitConverter.GetBytes(json.Length);
            byte[] databytes = System.Text.Encoding.UTF8.GetBytes(json);

            stream.Write(prependBytes, 0, prependBytes.Length);
            stream.Write(databytes, 0, databytes.Length);
        }
    }
}
