using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
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
        private bool busy = false;

        public Client()
        {
            InitializeComponent();
            Directory.CreateDirectory(PUSH_PATH);
            Directory.CreateDirectory(PULL_PATH);
            foreach (string f in Directory.GetFiles(PUSH_PATH))
                pushFilePaths.Add(f);
        }

        private void Client_Load(object sender, EventArgs e)
        {
            RefreshPushPaths();
            lbServer.Items.Clear();
            lbServer.Items.Add("Not connected");
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (!clientSocket.Connected)
            {
                string[] address = txtAddress.Text.Split(':');
                try
                {
                    clientSocket.Connect(address[0], int.Parse(address[1]));
                }
                catch (SocketException) { }
            }
            if (clientSocket.Connected)
            {
                pbStatus.BackgroundImage = FileSharing.Properties.Resources.CloudOK;
                btnPull.Enabled = true;
                btnPush.Enabled = true;
                btnSync.Enabled = true;
                btnRemove.Enabled = true;
                btnRefresh.Enabled = true;
                btnServerRemove.Enabled = true;
                RefreshPullList();

                Thread t = new Thread(() => ReceiveMessage());
                t.Start();
            }
        }

        private void ReceiveMessage()
        {
            byte[] clientData = new byte[BUFFER_SIZE];
            int receivedBytesLen = clientSocket.Receive(clientData);

            if (Encoding.ASCII.GetString(clientData, 0, sizeof(char) * 3).Contains("@@@"))
                busy = false;
            else if (Encoding.ASCII.GetString(clientData, 0, sizeof(char) * 3).Contains("$$$"))
            {
                lbServer.Items.Clear();
                string[] fileNames = Encoding.ASCII.GetString(clientData).Substring(3).Split(';');
                foreach (string f in fileNames)
                    lbServer.Items.Add(f);
            }
            else
            {
                int fileNameLen = BitConverter.ToInt32(clientData, 0);
                string fileName = new DirectoryInfo(Encoding.ASCII.GetString(clientData, 4, fileNameLen)).Name;
                BinaryWriter bWrite = new BinaryWriter(File.Open(PULL_PATH + @"\" + fileName, FileMode.Create));
                bWrite.Write(clientData, 4 + fileNameLen, receivedBytesLen - 4 - fileNameLen);
                bWrite.Close();
            }
            ReceiveMessage();
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            if (uploadFileBrowse.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                foreach (string f in uploadFileBrowse.FileNames)
                    pushFilePaths.Add(f);
                RefreshPushPaths();
            }
        }

        private void btnPush_Click(object sender, EventArgs e)
        {
            PushFiles();
        }

        private void btnPull_Click(object sender, EventArgs e)
        {
            RefreshPullList();
            PullAll();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            foreach (string f in Directory.GetFiles(PUSH_PATH))
                if (!pushFilePaths.Contains(f))
                    pushFilePaths.Add(f);
            RefreshPushPaths();
            RefreshPullList();
        }

        private void btnSync_Click(object sender, EventArgs e)
        {
            PushFiles();
            PullAll();
        }

        private void lbClient_DoubleClick(object sender, EventArgs e)
        {
            if (lbClient.SelectedIndex == 0)
                Process.Start(PULL_PATH);
            else if (lbClient.SelectedIndex == 1)
                Process.Start(PUSH_PATH);
            else if (File.Exists(lbClient.SelectedItem.ToString()))
                Process.Start(lbClient.SelectedItem.ToString());
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lbClient.SelectedIndex > 3)
                pushFilePaths.Remove(lbClient.SelectedItem.ToString());
            else
                MessageBox.Show("File not selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            RefreshPushPaths();
        }

        private void btnServerRemove_Click(object sender, EventArgs e)
        {

            if (lbServer.SelectedIndex != -1 && File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\AppServer\" + @lbServer.SelectedItem.ToString()))
            {
                DialogResult d = MessageBox.Show("Are you sure you want to permanently delete file?", "Warning!", MessageBoxButtons.OKCancel);

                if (d == DialogResult.OK)
                    File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\AppServer\" + @lbServer.SelectedItem.ToString());
            }
            else
                MessageBox.Show("File not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            RefreshPullList();
        }

        private void Client_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (clientSocket.Connected)
                clientSocket.Close();
        }

        private void PushFiles()
        {
            progressBar.Visible = true;
            int i = 0;
            foreach (string f in pushFilePaths)
            {
                System.Threading.SpinWait.SpinUntil(() => !busy);
                Thread.Sleep(1500);
                busy = true;
                progressBar.Value = (i / pushFilePaths.Count) * 100;

                byte[] fileNameByte = Encoding.ASCII.GetBytes(f);
                byte[] fileNameLen = BitConverter.GetBytes(fileNameByte.Length);
                byte[] fileData = File.ReadAllBytes(f);
                byte[] clientData = new byte[4 + fileNameByte.Length + fileData.Length];

                fileNameLen.CopyTo(clientData, 0);
                fileNameByte.CopyTo(clientData, 4);
                fileData.CopyTo(clientData, 4 + fileNameByte.Length);
                clientSocket.Send(clientData);
                ++i;
            }
            progressBar.Visible = false;
            pushFilePaths.Clear();
            RefreshPullList();
            RefreshPushPaths();
        }

        private void RefreshPushPaths()
        {
            lbClient.Items.Clear();
            lbClient.Items.Add("Save to: " + PULL_PATH);
            lbClient.Items.Add("Upload from: " + PUSH_PATH);
            lbClient.Items.Add(" ");
            lbClient.Items.Add("Files to upload: ");
            foreach (string s in pushFilePaths)
                lbClient.Items.Add(s);
        }

        private void RefreshPullList()
        {
            lbServer.Items.Clear();
            byte[] clientData = Encoding.ASCII.GetBytes("getfiles");
            clientSocket.Send(clientData);
        }

        private void PullAll()
        {
            byte[] clientData = Encoding.ASCII.GetBytes("pull");
            clientSocket.Send(clientData);
        }
    }
}
