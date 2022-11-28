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
        private static string PUSH_PATH = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\AppServer\Upload";
        private static string PULL_PATH = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\Downloads";
        private static int BUFFER_SIZE = 1024 * 1024 * 100; //1KB x 1,024 = 1MB; 1MB x 100 = 100MB
        private List<string> pushFilePaths = new List<string>();

        public Client()
        {
            InitializeComponent();
            Directory.CreateDirectory(PUSH_PATH);
            Directory.CreateDirectory(PULL_PATH);
            pushFilePaths.Add(PUSH_PATH);
        }

        private void Client_Load(object sender, EventArgs e)
        {
            RefreshPushPaths();
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
            BinaryWriter bWrite = new BinaryWriter(File.Open(PULL_PATH + @"\" + fileName, FileMode.Create));
            //BinaryWriter bWrite = new BinaryWriter(File.Open(@"C:\Users\panda\Desktop\Downloads\" + fileName, FileMode.Create));
            bWrite.Write(clientData, 4 + fileNameLen, receivedBytesLen - 4 - fileNameLen);
            bWrite.Close();
            ReceiveMessage();
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            if (uploadFileBrowse.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                pushFilePaths.Add(uploadFileBrowse.);
                RefreshPushPaths();
            }
            //if (folderBrowse.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //{
            //    pushFilePaths.Add(folderBrowse.SelectedPath);
            //    RefreshPushPaths();
            //}
        }

        private void btnPush_Click(object sender, EventArgs e)
        {
            PushFiles(PUSH_PATH);
        }

        private void btnPull_Click(object sender, EventArgs e)
        {
            byte[] clientData = Encoding.ASCII.GetBytes("pull");
            clientSocket.Send(clientData);
        }

        private void PushFiles(string path)
        {
            foreach (string f in Directory.GetFiles(path))
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

        private void RefreshPushPaths()
        {
            lbClient.Items.Clear();
            lbClient.Items.Add("Save to: " + PULL_PATH);
            lbClient.Items.Add("Push from: " + PUSH_PATH);
            lbClient.Items.Add(" ");
            foreach (string s in pushFilePaths)
                lbClient.Items.Add(s);
        }
    }
}
