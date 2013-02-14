namespace Fop2DD
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.connectButton = new System.Windows.Forms.Button();
            this.NotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.connectionBox = new System.Windows.Forms.GroupBox();
            this.showAdvancedLabel = new System.Windows.Forms.LinkLabel();
            this.advancedConnection = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pingIntervalTextBox = new System.Windows.Forms.TextBox();
            this.portTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.hostTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.authenticationBox = new System.Windows.Forms.GroupBox();
            this.contextTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.usernameTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripStatusLabel = new Fop2DD.SpringLabel();
            this.contextMenu.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.connectionBox.SuspendLayout();
            this.advancedConnection.SuspendLayout();
            this.authenticationBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // connectButton
            // 
            this.connectButton.Location = new System.Drawing.Point(461, 189);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(75, 23);
            this.connectButton.TabIndex = 0;
            this.connectButton.Text = "Connect";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // NotifyIcon
            // 
            this.NotifyIcon.ContextMenuStrip = this.contextMenu;
            this.NotifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("NotifyIcon.Icon")));
            this.NotifyIcon.Visible = true;
            this.NotifyIcon.BalloonTipClicked += new System.EventHandler(this.NotifyIcon_BalloonTipClicked);
            this.NotifyIcon.DoubleClick += new System.EventHandler(this.NotifyIcon_DoubleClick);
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new System.Drawing.Size(126, 76);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.settingsToolStripMenuItem.Text = "Settings...";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(122, 6);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // statusStrip
            // 
            this.statusStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 227);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(557, 22);
            this.statusStrip.SizingGrip = false;
            this.statusStrip.TabIndex = 7;
            // 
            // connectionBox
            // 
            this.connectionBox.Controls.Add(this.showAdvancedLabel);
            this.connectionBox.Controls.Add(this.advancedConnection);
            this.connectionBox.Controls.Add(this.portTextBox);
            this.connectionBox.Controls.Add(this.label2);
            this.connectionBox.Controls.Add(this.hostTextBox);
            this.connectionBox.Controls.Add(this.label1);
            this.connectionBox.Location = new System.Drawing.Point(12, 12);
            this.connectionBox.Name = "connectionBox";
            this.connectionBox.Size = new System.Drawing.Size(262, 171);
            this.connectionBox.TabIndex = 8;
            this.connectionBox.TabStop = false;
            this.connectionBox.Text = "FOP2 Connection";
            // 
            // showAdvancedLabel
            // 
            this.showAdvancedLabel.ActiveLinkColor = System.Drawing.SystemColors.ControlText;
            this.showAdvancedLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.showAdvancedLabel.AutoSize = true;
            this.showAdvancedLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.showAdvancedLabel.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.showAdvancedLabel.LinkColor = System.Drawing.SystemColors.HotTrack;
            this.showAdvancedLabel.Location = new System.Drawing.Point(118, 88);
            this.showAdvancedLabel.Name = "showAdvancedLabel";
            this.showAdvancedLabel.Size = new System.Drawing.Size(134, 13);
            this.showAdvancedLabel.TabIndex = 10;
            this.showAdvancedLabel.TabStop = true;
            this.showAdvancedLabel.Text = "▸ Show advanced options";
            this.showAdvancedLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.showAdvancedLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.showAdvancedLabel_LinkClicked);
            // 
            // advancedConnection
            // 
            this.advancedConnection.Controls.Add(this.label4);
            this.advancedConnection.Controls.Add(this.label3);
            this.advancedConnection.Controls.Add(this.pingIntervalTextBox);
            this.advancedConnection.Location = new System.Drawing.Point(6, 104);
            this.advancedConnection.Name = "advancedConnection";
            this.advancedConnection.Size = new System.Drawing.Size(246, 61);
            this.advancedConnection.TabIndex = 9;
            this.advancedConnection.TabStop = false;
            this.advancedConnection.Text = "Advanced";
            this.advancedConnection.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(141, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "seconds";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Ping interval";
            // 
            // pingIntervalTextBox
            // 
            this.pingIntervalTextBox.Location = new System.Drawing.Point(80, 29);
            this.pingIntervalTextBox.MaxLength = 5;
            this.pingIntervalTextBox.Name = "pingIntervalTextBox";
            this.pingIntervalTextBox.Size = new System.Drawing.Size(50, 20);
            this.pingIntervalTextBox.TabIndex = 5;
            this.pingIntervalTextBox.Text = "20";
            // 
            // portTextBox
            // 
            this.portTextBox.Location = new System.Drawing.Point(86, 52);
            this.portTextBox.MaxLength = 5;
            this.portTextBox.Name = "portTextBox";
            this.portTextBox.Size = new System.Drawing.Size(50, 20);
            this.portTextBox.TabIndex = 3;
            this.portTextBox.Text = "4445";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Port";
            // 
            // hostTextBox
            // 
            this.hostTextBox.Location = new System.Drawing.Point(86, 26);
            this.hostTextBox.Name = "hostTextBox";
            this.hostTextBox.Size = new System.Drawing.Size(166, 20);
            this.hostTextBox.TabIndex = 1;
            this.hostTextBox.Text = "192.168.10.5";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Host";
            // 
            // authenticationBox
            // 
            this.authenticationBox.Controls.Add(this.contextTextBox);
            this.authenticationBox.Controls.Add(this.label7);
            this.authenticationBox.Controls.Add(this.passwordTextBox);
            this.authenticationBox.Controls.Add(this.label6);
            this.authenticationBox.Controls.Add(this.usernameTextBox);
            this.authenticationBox.Controls.Add(this.label5);
            this.authenticationBox.Location = new System.Drawing.Point(280, 13);
            this.authenticationBox.Name = "authenticationBox";
            this.authenticationBox.Size = new System.Drawing.Size(262, 170);
            this.authenticationBox.TabIndex = 9;
            this.authenticationBox.TabStop = false;
            this.authenticationBox.Text = "Authentication";
            // 
            // contextTextBox
            // 
            this.contextTextBox.Location = new System.Drawing.Point(90, 25);
            this.contextTextBox.Name = "contextTextBox";
            this.contextTextBox.Size = new System.Drawing.Size(166, 20);
            this.contextTextBox.TabIndex = 7;
            this.contextTextBox.Text = "TESTA";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 28);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(43, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Context";
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Location = new System.Drawing.Point(90, 77);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.Size = new System.Drawing.Size(166, 20);
            this.passwordTextBox.TabIndex = 5;
            this.passwordTextBox.Text = "1234";
            this.passwordTextBox.UseSystemPasswordChar = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 80);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Password";
            // 
            // usernameTextBox
            // 
            this.usernameTextBox.Location = new System.Drawing.Point(90, 51);
            this.usernameTextBox.Name = "usernameTextBox";
            this.usernameTextBox.Size = new System.Drawing.Size(166, 20);
            this.usernameTextBox.TabIndex = 3;
            this.usernameTextBox.Text = "101";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 54);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Username";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(542, 17);
            this.toolStripStatusLabel.Spring = true;
            this.toolStripStatusLabel.Text = "-";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(557, 249);
            this.Controls.Add(this.authenticationBox);
            this.Controls.Add(this.connectionBox);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.connectButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Fop2DD";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.contextMenu.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.connectionBox.ResumeLayout(false);
            this.connectionBox.PerformLayout();
            this.advancedConnection.ResumeLayout(false);
            this.advancedConnection.PerformLayout();
            this.authenticationBox.ResumeLayout(false);
            this.authenticationBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.NotifyIcon NotifyIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip;
        private SpringLabel toolStripStatusLabel;
        private System.Windows.Forms.GroupBox connectionBox;
        private System.Windows.Forms.LinkLabel showAdvancedLabel;
        private System.Windows.Forms.GroupBox advancedConnection;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox pingIntervalTextBox;
        private System.Windows.Forms.TextBox portTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox hostTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox authenticationBox;
        private System.Windows.Forms.TextBox contextTextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox usernameTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    }
}

