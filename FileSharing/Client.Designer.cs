namespace FileSharing
{
    partial class Client
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Client));
            this.lbl0 = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.lbServer = new System.Windows.Forms.ListBox();
            this.lbl1 = new System.Windows.Forms.Label();
            this.lbl2 = new System.Windows.Forms.Label();
            this.lbClient = new System.Windows.Forms.ListBox();
            this.btn = new System.Windows.Forms.Button();
            this.btnUpload = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.pbStatus = new System.Windows.Forms.PictureBox();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnPush = new System.Windows.Forms.Button();
            this.btnPull = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.folderBrowse = new System.Windows.Forms.FolderBrowserDialog();
            this.uploadFileBrowse = new System.Windows.Forms.OpenFileDialog();
            this.btnSync = new System.Windows.Forms.Button();
            this.btnServerRemove = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.pbStatus)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl0
            // 
            this.lbl0.AutoSize = true;
            this.lbl0.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl0.Location = new System.Drawing.Point(12, 9);
            this.lbl0.Name = "lbl0";
            this.lbl0.Size = new System.Drawing.Size(91, 24);
            this.lbl0.TabIndex = 0;
            this.lbl0.Text = "Server IP:";
            // 
            // txtAddress
            // 
            this.txtAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddress.Location = new System.Drawing.Point(109, 9);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(179, 26);
            this.txtAddress.TabIndex = 1;
            this.txtAddress.Text = "172.20.8.252:25565";
            this.toolTip.SetToolTip(this.txtAddress, "Host and port seperated by colon :");
            // 
            // lbServer
            // 
            this.lbServer.FormattingEnabled = true;
            this.lbServer.Items.AddRange(new object[] {
            "PLACEHOLDER",
            "(This will be the interface for server files)"});
            this.lbServer.Location = new System.Drawing.Point(12, 78);
            this.lbServer.Name = "lbServer";
            this.lbServer.Size = new System.Drawing.Size(402, 173);
            this.lbServer.TabIndex = 4;
            // 
            // lbl1
            // 
            this.lbl1.AutoSize = true;
            this.lbl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl1.Location = new System.Drawing.Point(12, 55);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(92, 20);
            this.lbl1.TabIndex = 0;
            this.lbl1.Text = "Server Files";
            this.toolTip.SetToolTip(this.lbl1, "Files stored remotely");
            // 
            // lbl2
            // 
            this.lbl2.AutoSize = true;
            this.lbl2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl2.Location = new System.Drawing.Point(12, 275);
            this.lbl2.Name = "lbl2";
            this.lbl2.Size = new System.Drawing.Size(84, 20);
            this.lbl2.TabIndex = 0;
            this.lbl2.Text = "Local Files";
            this.toolTip.SetToolTip(this.lbl2, "Files stored on your machine");
            // 
            // lbClient
            // 
            this.lbClient.FormattingEnabled = true;
            this.lbClient.Location = new System.Drawing.Point(12, 298);
            this.lbClient.Name = "lbClient";
            this.lbClient.Size = new System.Drawing.Size(402, 173);
            this.lbClient.TabIndex = 4;
            this.lbClient.DoubleClick += new System.EventHandler(this.lbClient_DoubleClick);
            // 
            // btn
            // 
            this.btn.Enabled = false;
            this.btn.Location = new System.Drawing.Point(339, 257);
            this.btn.Name = "btn";
            this.btn.Size = new System.Drawing.Size(75, 23);
            this.btn.TabIndex = 5;
            this.btn.Text = "Browse";
            this.toolTip.SetToolTip(this.btn, "Choose new location to save server files");
            this.btn.UseVisualStyleBackColor = true;
            // 
            // btnUpload
            // 
            this.btnUpload.Location = new System.Drawing.Point(339, 477);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(75, 23);
            this.btnUpload.TabIndex = 6;
            this.btnUpload.Text = "Add Files";
            this.toolTip.SetToolTip(this.btnUpload, "Adds files to the upload queue");
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // pbStatus
            // 
            this.pbStatus.BackgroundImage = global::FileSharing.Properties.Resources.CloudError;
            this.pbStatus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbStatus.Location = new System.Drawing.Point(420, 9);
            this.pbStatus.Name = "pbStatus";
            this.pbStatus.Size = new System.Drawing.Size(27, 26);
            this.pbStatus.TabIndex = 3;
            this.pbStatus.TabStop = false;
            this.toolTip.SetToolTip(this.pbStatus, "Connection status");
            // 
            // btnRemove
            // 
            this.btnRemove.BackgroundImage = global::FileSharing.Properties.Resources.Cancel;
            this.btnRemove.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnRemove.Enabled = false;
            this.btnRemove.Location = new System.Drawing.Point(420, 330);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(27, 26);
            this.btnRemove.TabIndex = 2;
            this.toolTip.SetToolTip(this.btnRemove, "Removes selected item from queue");
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackgroundImage = global::FileSharing.Properties.Resources.Refresh;
            this.btnRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnRefresh.Enabled = false;
            this.btnRefresh.Location = new System.Drawing.Point(420, 298);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(27, 26);
            this.btnRefresh.TabIndex = 2;
            this.toolTip.SetToolTip(this.btnRefresh, "Refreshes the items in queue");
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnPush
            // 
            this.btnPush.BackgroundImage = global::FileSharing.Properties.Resources.Push;
            this.btnPush.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnPush.Enabled = false;
            this.btnPush.Location = new System.Drawing.Point(78, 477);
            this.btnPush.Name = "btnPush";
            this.btnPush.Size = new System.Drawing.Size(27, 26);
            this.btnPush.TabIndex = 2;
            this.toolTip.SetToolTip(this.btnPush, "Pushes changes to the server (temporarily uploads all \"Uploads\" to server");
            this.btnPush.UseVisualStyleBackColor = true;
            this.btnPush.Click += new System.EventHandler(this.btnPush_Click);
            // 
            // btnPull
            // 
            this.btnPull.BackgroundImage = global::FileSharing.Properties.Resources.Pull;
            this.btnPull.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnPull.Enabled = false;
            this.btnPull.Location = new System.Drawing.Point(45, 477);
            this.btnPull.Name = "btnPull";
            this.btnPull.Size = new System.Drawing.Size(27, 26);
            this.btnPull.TabIndex = 2;
            this.toolTip.SetToolTip(this.btnPull, "Pulls changes from server (temporarily downloads all server files)");
            this.btnPull.UseVisualStyleBackColor = true;
            this.btnPull.Click += new System.EventHandler(this.btnPull_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.BackgroundImage = global::FileSharing.Properties.Resources.ConnectArrow;
            this.btnConnect.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnConnect.Location = new System.Drawing.Point(294, 9);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(27, 26);
            this.btnConnect.TabIndex = 2;
            this.toolTip.SetToolTip(this.btnConnect, "Connect");
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // uploadFileBrowse
            // 
            this.uploadFileBrowse.FileName = "file";
            this.uploadFileBrowse.Multiselect = true;
            // 
            // btnSync
            // 
            this.btnSync.BackgroundImage = global::FileSharing.Properties.Resources.SyncDatabase;
            this.btnSync.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSync.Enabled = false;
            this.btnSync.Location = new System.Drawing.Point(12, 477);
            this.btnSync.Name = "btnSync";
            this.btnSync.Size = new System.Drawing.Size(27, 26);
            this.btnSync.TabIndex = 2;
            this.btnSync.UseVisualStyleBackColor = true;
            this.btnSync.Click += new System.EventHandler(this.btnSync_Click);
            // 
            // btnServerRemove
            // 
            this.btnServerRemove.BackgroundImage = global::FileSharing.Properties.Resources.Cancel;
            this.btnServerRemove.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnServerRemove.Enabled = false;
            this.btnServerRemove.Location = new System.Drawing.Point(420, 78);
            this.btnServerRemove.Name = "btnServerRemove";
            this.btnServerRemove.Size = new System.Drawing.Size(27, 26);
            this.btnServerRemove.TabIndex = 2;
            this.btnServerRemove.UseVisualStyleBackColor = true;
            this.btnServerRemove.Click += new System.EventHandler(this.btnServerRemove_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(179, 477);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(100, 23);
            this.progressBar.TabIndex = 7;
            this.progressBar.Visible = false;
            // 
            // Client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(459, 516);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.btn);
            this.Controls.Add(this.lbClient);
            this.Controls.Add(this.lbServer);
            this.Controls.Add(this.pbStatus);
            this.Controls.Add(this.btnServerRemove);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnPush);
            this.Controls.Add(this.btnPull);
            this.Controls.Add(this.btnSync);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.lbl2);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.lbl1);
            this.Controls.Add(this.lbl0);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Client";
            this.Text = "File Sharing";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Client_FormClosing);
            this.Load += new System.EventHandler(this.Client_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbStatus)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl0;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.PictureBox pbStatus;
        private System.Windows.Forms.ListBox lbServer;
        private System.Windows.Forms.Label lbl1;
        private System.Windows.Forms.Label lbl2;
        private System.Windows.Forms.ListBox lbClient;
        private System.Windows.Forms.Button btn;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Button btnSync;
        private System.Windows.Forms.Button btnPull;
        private System.Windows.Forms.Button btnPush;
        private System.Windows.Forms.FolderBrowserDialog folderBrowse;
        private System.Windows.Forms.OpenFileDialog uploadFileBrowse;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnServerRemove;
        private System.Windows.Forms.ProgressBar progressBar;
    }
}

