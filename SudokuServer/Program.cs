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

            TcpListener server = new TcpListener(IPAddress.Parse("127.0.0.1"), 5555);
            clients = new List<TcpClient>();
            server.Start();
            Console.WriteLine($">> Server Online at {server.LocalEndpoint}.");
            Console.WriteLine(">> Waiting for 2 connections to start a game.");
            // accept clients

            while (clients.Count < 1)
            {
                clients.Add(server.AcceptTcpClient());
                Console.WriteLine("Client connected.");
            }

            Console.WriteLine(">> Both clients are here, sending sudoku...");

            // send the sudoku

            SudokuReader sr = new SudokuReader("C:\\testJson.json");
            NumberGrid gridToSend = sr.getRandomSudoku(4);
            string sudoku = gridToSend.ToString();

            foreach (var client in clients)
            {
                sendToClient(client, sudoku);
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
            Console.WriteLine($"String:{s}");
            byte[] prependBytes = BitConverter.GetBytes(s.Length);
            byte[] oMessage = System.Text.Encoding.UTF8.GetBytes(s);

            client.GetStream().Write(prependBytes, 0, prependBytes.Length);
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
