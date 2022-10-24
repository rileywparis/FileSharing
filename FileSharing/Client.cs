using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileSharing
{
    public partial class Client : Form
    {
        private static Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private static string PATH = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\AppServer\Upload";
        private static string PATH0 = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\Downloads";
        private static int BUFFER_SIZE = 1024 * 1024 * 100; //1KB x 1,024 = 1MB; 1MB x 100 = 100MB

        public Client()
        {
            InitializeComponent();
            Directory.CreateDirectory(PATH);
            Directory.CreateDirectory(PATH0);
        }

        private void Client_Load(object sender, EventArgs e)
        {
            lbClient.Items.Insert(0, "Save to: " + PATH0);
            lbClient.Items.Insert(1, "Push from: " + PATH);
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (!clientSocket.Connected)
            {
                string[] address = txtAddress.Text.Split(':');
                while (!clientSocket.Connected)
                    try
                    {
                        clientSocket.Connect(address[0], int.Parse(address[1]));
                    }
                    catch (SocketException) { }
                pbStatus.BackgroundImage = FileSharing.Properties.Resources.CloudOK;
                btnPull.Enabled = true;
                btnPush.Enabled = true;

                Thread t = new Thread(() => ReceiveMessage());
                t.Start();
            }
        }

        private static void ReceiveMessage()
        {
            byte[] clientData = new byte[BUFFER_SIZE];
            int receivedBytesLen = clientSocket.Receive(clientData);
            int fileNameLen = BitConverter.ToInt32(clientData, 0);
            string fileName = new DirectoryInfo(Encoding.ASCII.GetString(clientData, 4, fileNameLen)).Name;
            BinaryWriter bWrite = new BinaryWriter(File.Open(PATH0 + @"\" + fileName, FileMode.Create));
            //BinaryWriter bWrite = new BinaryWriter(File.Open(@"C:\Users\panda\Desktop\Downloads\" + fileName, FileMode.Create));
            bWrite.Write(clientData, 4 + fileNameLen, receivedBytesLen - 4 - fileNameLen);
            bWrite.Close();
            ReceiveMessage();
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {

        }

        private void btnPush_Click(object sender, EventArgs e)
        {
            foreach (string f in Directory.GetFiles(PATH))
            {
                byte[] fileNameByte = Encoding.ASCII.GetBytes(f);
                byte[] fileNameLen = BitConverter.GetBytes(fileNameByte.Length);
                byte[] fileData = File.ReadAllBytes(f);
                byte[] clientData = new byte[4 + fileNameByte.Length + fileData.Length];

                fileNameLen.CopyTo(clientData, 0);
                fileNameByte.CopyTo(clientData, 4);
                fileData.CopyTo(clientData, 4 + fileNameByte.Length);
                clientSocket.Send(clientData);
            }
        }

        private void btnPull_Click(object sender, EventArgs e)
        {
            byte[] clientData = Encoding.ASCII.GetBytes("pull");
            clientSocket.Send(clientData);
        }
    }
}
