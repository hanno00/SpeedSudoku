using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SudokuClient
{
    class Program
    {
        static void Main(string[] args)
        {
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
            Console.Read();


        }
    }
}
