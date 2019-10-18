using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using SudokuLogic;


namespace SudokuServer
{
    class Program
    {
        static void Main(string[] args)
        {
            // setup server connection
            List<TcpClient> clients;

            TcpListener server = new TcpListener(IPAddress.Loopback, 5555);
            clients = new List<TcpClient>();
            server.Start();
            Console.WriteLine($">> Server Online at {server.LocalEndpoint}.");
            Console.WriteLine(">> Waiting for 2 connections to start a game.");
            Console.ReadKey();
            // accept clients

            while (clients.Count < 2)
            {
                clients.Add(server.AcceptTcpClient());
            }

            Console.WriteLine(">> Both clients are here, sending sudoku...");

            // send the sudoku

            SudokuReader sr = new SudokuReader("C:\\Users\\Yoram\\Desktop\\Programming\\C#\\C# eindopdracht\\SpeedSudoku\\SudokuLogic\\testJson.json");
            NumberGrid gridToSend = sr.getRandomSudoku(4);
            byte[] gridBytes = System.Text.Encoding.Unicode.GetBytes(gridToSend.ToString().ToCharArray());

            foreach (var client in clients)
            {
                string sudokuString = "";
                sendToClient(client, sudokuString);
            }

            // wait for clients to send it back

            foreach(var client in clients)
            {
                receiveMessageFromClient(client);
            }

            // check if it's complete and right
            // Misschien in client doen?

            // send back answer

            foreach (var client in clients)
            {
                string winner = "Hypothetische winnaar";
                sendToClient(client, winner);
            }

            // close sockets
            server.Stop();
            clients.Clear();
        }

        private static void sendToClient(TcpClient client, string s)
        {
            byte[] oMessage = System.Text.Encoding.Unicode.GetBytes(s);
            client.GetStream().Write(oMessage, 0, oMessage.Length);
        }

        private static string receiveMessageFromClient(TcpClient client)
        {
            byte[] iMessage = new byte[1024];
            client.GetStream().Read(iMessage, 0, iMessage.Length);
            return System.Text.Encoding.Unicode.GetString(iMessage);

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
