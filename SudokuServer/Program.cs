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
            const int bytesize = 1024;
            string message = "";
            byte[] buffer = new byte[bytesize];

            // create lobby
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

            foreach (var client in clients)
            {
                string sudokuString = "";
                sendToClient(client, sudokuString);
            }

            // wait for clients to send it back

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
            byte[] bMessage = System.Text.Encoding.Unicode.GetBytes(s);
            client.GetStream().Write(bMessage, 0, bMessage.Length);
        }
    }
}
