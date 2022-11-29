using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Server
{
    internal class Server
    {
        private static Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private static List<Socket> clientSockets = new List<Socket>();
        private static string PATH = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\AppServer";
        private const int BUFFER_SIZE = 1024 * 1024 * 100;   //1KB x 1,024 = 1MB; 1MB x 100 = 100MB
        private static byte[] buffer = new byte[BUFFER_SIZE];

        static void Main(string[] args)
        {
            Console.Title = "Server";
            Directory.CreateDirectory(PATH);
            Console.WriteLine("Enter IPv4 address: ");
            string address = Console.ReadLine();
            Console.WriteLine("Setting up server...");
            //serverSocket.Bind(new IPEndPoint(IPAddress.Parse("172.20.8.252"), 25565));  //Modify this; IPv4 Address and port of your choice -------------------------------------------
            serverSocket.Bind(new IPEndPoint(IPAddress.Parse(address), 25565));
            serverSocket.Listen(0);
            serverSocket.BeginAccept(AcceptCallback, null);
            Console.WriteLine("Setup complete");
            Console.ReadLine();
        }

        private static void AcceptCallback(IAsyncResult AR)
        {
            Socket socket;
            try
            {
                socket = serverSocket.EndAccept(AR);
            }
            catch (ObjectDisposedException)
            {
                return;
            }

            clientSockets.Add(socket);
            socket.BeginReceive(buffer, 0, BUFFER_SIZE, SocketFlags.None, ReceiveCallback, socket);
            Console.WriteLine("Client connected");
            serverSocket.BeginAccept(AcceptCallback, null);
        }

        private static void ReceiveCallback(IAsyncResult AR)
        {
            Socket current = (Socket)AR.AsyncState;
            int received;

            try
            {
                received = current.EndReceive(AR);
            }
            catch (SocketException)
            {
                Console.WriteLine("Client disconnected");
                current.Close();
                clientSockets.Remove(current);
                return;
            }

            byte[] recBuf = new byte[received];
            Array.Copy(buffer, recBuf, received);

            if (Encoding.ASCII.GetString(recBuf).Equals("pull"))
                Send(current);
            else
            {
                int fileNameLen = BitConverter.ToInt32(recBuf, 0);
                string fileName = new DirectoryInfo(Encoding.ASCII.GetString(recBuf, 4, fileNameLen)).Name;
                Console.WriteLine(current.LocalEndPoint + " sending " + fileName);
                BinaryWriter bWrite = new BinaryWriter(File.Open(PATH + @"\" + fileName, FileMode.Create));
                bWrite.Write(recBuf, 4 + fileNameLen, received - 4 - fileNameLen);
                bWrite.Close();
            }

            current.BeginReceive(buffer, 0, BUFFER_SIZE, SocketFlags.None, ReceiveCallback, current);
        }

        private static void Send(Socket clientSocket)
        {
            foreach (string f in Directory.GetFiles(PATH))
            {
                string fileName = new DirectoryInfo(f).Name;
                if (f != "Upload")
                {
                    Console.WriteLine(clientSocket.LocalEndPoint + " receiving " + fileName);
                    byte[] fileNameByte = Encoding.ASCII.GetBytes(f);
                    byte[] fileNameLen = BitConverter.GetBytes(fileNameByte.Length);
                    byte[] fileData = File.ReadAllBytes(f);
                    byte[] serverData = new byte[4 + fileNameByte.Length + fileData.Length];

                    fileNameLen.CopyTo(serverData, 0);
                    fileNameByte.CopyTo(serverData, 4);
                    fileData.CopyTo(serverData, 4 + fileNameByte.Length);
                    clientSocket.Send(serverData);

                    Thread.Sleep(1500); //Temporary pause to prevent bug
                }
            }
        }
    }
}
