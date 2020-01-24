using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SudokuLogic;


namespace SudokuServer
{
    class Program
    {
        public static ArrayList playersReady;
        public static Dictionary<string, TimeSpan> playersTime;
        public static int amountOfPlayers = 2;
        public static int sudokuType = 0;

        public static SudokuReader sudokuReader;
        static void Main(string[] args)
        {
            // setup server connection
            List<TcpClient> clients;
            playersReady = new ArrayList();
            playersTime = new Dictionary<string, TimeSpan>();
            TcpListener server = new TcpListener(IPAddress.Parse("127.0.0.1"), 5555);
            clients = new List<TcpClient>();
            server.Start();
            
            //Asynchronously setup Reader
            asyncSetup();

            Console.WriteLine($">> Server Online at {server.LocalEndpoint}.");
            Console.WriteLine(">> Waiting for 2 connections to start a game.");
            // accept clients

            while (clients.Count < amountOfPlayers)
            {
                clients.Add(server.AcceptTcpClient());
                Console.WriteLine("Client connected.");
            }

            Console.WriteLine(">> Both clients are here, waiting for ready");

            while (true)
            {
                foreach (var client in clients)
                {
                    receiveMessageFromClient(client);
                }

                while (true)
                {
                    if (playersReady.Count >= amountOfPlayers)
                    {
                        Console.WriteLine("All players are ready!");
                        break;
                    }
                    Thread.Sleep(1000);
                }

                object sudoku = getRandomSudokuObject();
                foreach (var client in clients)
                {
                    sendToClient(client, sudoku);
                }

                foreach (var client in clients)
                {
                    receiveMessageFromClient(client);
                }

                string winner = "";
                while (true)
                {
                    if (playersTime.Count >= amountOfPlayers)
                    {
                        List<string> keys = new List<string>(playersTime.Keys);
                        if (keys.Count == 1)
                        {
                            winner = keys[0];
                        }
                        else
                        {
                            int win = TimeSpan.Compare((TimeSpan)playersTime[(string)playersReady[0]], (TimeSpan)playersTime[(string)playersReady[1]]);
                            if (win == -1)
                            {
                                winner = (string)playersReady[0];
                            }
                            else if (win == 1)
                            {
                                winner = (string)playersReady[1];
                            }
                        }
                        break;
                    }
                    Thread.Sleep(200);
                }

                foreach (var client in clients)
                {
                    sendToClient(client, getWinner(winner));
                }

                playersReady.Clear();
                playersTime.Clear();
            }

            // close sockets
            server.Stop();
            clients.Clear();
        }

        static async void asyncSetup()
        {
            // This method runs asynchronously.
            await Task.Run(() => setupReader());
        }

        private static void setupReader() {

            sudokuReader = new SudokuReader("..\\..\\..\\res\\sudoku.json");
        }

        static public object getRandomSudokuObject()
        {
            Random rand = new Random();
            int value = 4;
            if (sudokuType == 1)
            {
                value = 6;
            }
            sudokuType++;
            sudokuType %= 2;
            NumberGrid gridToSend = sudokuReader.getRandomSudoku(value);
            string sudoku = gridToSend.ToString();
            Console.WriteLine(sudoku);
            return new
            {
                id = "server/sendSudoku",
                data = sudoku
            };
        }

        static public object getWinner(string name)
        {
            return new
            {
                id = "server/sendWinner",
                data = name
            };
        }

        private static void sendToClient(TcpClient client, object json)
        {
            string s = JsonConvert.SerializeObject(json);
            Console.WriteLine($"String:{s}");
            byte[] prependBytes = BitConverter.GetBytes(s.Length);
            byte[] oMessage = System.Text.Encoding.UTF8.GetBytes(s);

            client.GetStream().Write(prependBytes, 0, prependBytes.Length);
            client.GetStream().Write(oMessage, 0, oMessage.Length);
        }

        private static void receiveMessageFromClient(TcpClient client)
        {
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

            dynamic deserialized = JsonConvert.DeserializeObject(json);

            if (deserialized != null)
            {
                switch ((string)deserialized.id)
                {
                    case "player/ready":
                        playersReady.Add((string)deserialized.data);
                        break;
                    case "player/time":
                        playersTime.Add((string)deserialized.username, (TimeSpan)deserialized.data);
                        break;
                    default:

                        break;
                }
            }




            /* Mocht dit hierboven nullChar's hebben, kunnen we ook dit gebruiken. Dit haalt die shite weg


             string message = System.Text.Encoding.Unicode.GetString(bytes);

             string messageToPrint = null;
             foreach (var nullChar in message)
             {
                 if (nullChar != '\0')
                 {
                     messageToPrint += nullChar;
                 }
             }
             return messageToPrint;
             */
        }
    }
}

