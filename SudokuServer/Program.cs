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


            // create lobby
            TcpListener server = new TcpListener(IPAddress.Loopback, 5555);
            clients = new List<TcpClient>();
            server.Start();
            Console.WriteLine($">> Server Online at {server.LocalEndpoint}.");
            Console.WriteLine(">> Waiting for 2 connections to start a game.");
            Console.ReadKey();
            // accept clients


            // send the sudoku

            // wait for clients to send it back

            // check if it's complete and right

            // send back answer

            // notify if game is over

            // close sockets
        }
    }
}
