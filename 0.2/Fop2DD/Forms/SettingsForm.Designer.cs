namespace Fop2DD
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.commandsBox = new System.Windows.Forms.GroupBox();
            this.fop2WebInterfaceTextBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.hotkeyBox = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.hotkeyWinCheckBox = new System.Windows.Forms.CheckBox();
            this.hotkeyShiftCheckBox = new System.Windows.Forms.CheckBox();
            this.hotkeyAltCheckBox = new System.Windows.Forms.CheckBox();
            this.hotkeyCtrlCheckBox = new System.Windows.Forms.CheckBox();
            this.hotkeyComboBox = new System.Windows.Forms.ComboBox();
            this.authenticationBox = new System.Windows.Forms.GroupBox();
            this.contextTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.usernameTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
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
            this.applyButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.commandsBox.SuspendLayout();
            this.hotkeyBox.SuspendLayout();
            this.authenticationBox.SuspendLayout();
            this.connectionBox.SuspendLayout();
            this.advancedConnection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // commandsBox
            // 
            this.commandsBox.Controls.Add(this.fop2WebInterfaceTextBox);
            this.commandsBox.Controls.Add(this.label9);
            this.commandsBox.Location = new System.Drawing.Point(12, 206);
            this.commandsBox.Name = "commandsBox";
            this.commandsBox.Size = new System.Drawing.Size(530, 142);
            this.commandsBox.TabIndex = 3;
            this.commandsBox.TabStop = false;
            this.commandsBox.Text = "Commands";
            // 
            // fop2WebInterfaceTextBox
            // 
            this.fop2WebInterfaceTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fop2WebInterfaceTextBox.Location = new System.Drawing.Point(150, 29);
            this.fop2WebInterfaceTextBox.Name = "fop2WebInterfaceTextBox";
            this.fop2WebInterfaceTextBox.Size = new System.Drawing.Size(374, 20);
            this.fop2WebInterfaceTextBox.TabIndex = 1;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(7, 32);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(104, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "FOP2 Web interface";
            // 
            // hotkeyBox
            // 
            this.hotkeyBox.Controls.Add(this.label8);
            this.hotkeyBox.Controls.Add(this.hotkeyWinCheckBox);
            this.hotkeyBox.Controls.Add(this.hotkeyShiftCheckBox);
            this.hotkeyBox.Controls.Add(this.hotkeyAltCheckBox);
            this.hotkeyBox.Controls.Add(this.hotkeyCtrlCheckBox);
            this.hotkeyBox.Controls.Add(this.hotkeyComboBox);
            this.hotkeyBox.Location = new System.Drawing.Point(280, 129);
            this.hotkeyBox.Name = "hotkeyBox";
            this.hotkeyBox.Size = new System.Drawing.Size(262, 71);
            this.hotkeyBox.TabIndex = 2;
            this.hotkeyBox.TabStop = false;
            this.hotkeyBox.Text = "Dial hotkey";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(130, 32);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(13, 13);
            this.label8.TabIndex = 4;
            this.label8.Text = "+";
            // 
            // hotkeyWinCheckBox
            // 
            this.hotkeyWinCheckBox.AutoSize = true;
            this.hotkeyWinCheckBox.Location = new System.Drawing.Point(67, 43);
            this.hotkeyWinCheckBox.Name = "hotkeyWinCheckBox";
            this.hotkeyWinCheckBox.Size = new System.Drawing.Size(48, 17);
            this.hotkeyWinCheckBox.TabIndex = 3;
            this.hotkeyWinCheckBox.Text = "WIN";
            this.hotkeyWinCheckBox.UseVisualStyleBackColor = true;
            // 
            // hotkeyShiftCheckBox
            // 
            this.hotkeyShiftCheckBox.AutoSize = true;
            this.hotkeyShiftCheckBox.Location = new System.Drawing.Point(67, 20);
            this.hotkeyShiftCheckBox.Name = "hotkeyShiftCheckBox";
            this.hotkeyShiftCheckBox.Size = new System.Drawing.Size(57, 17);
            this.hotkeyShiftCheckBox.TabIndex = 1;
            this.hotkeyShiftCheckBox.Text = "SHIFT";
            this.hotkeyShiftCheckBox.UseVisualStyleBackColor = true;
            // 
            // hotkeyAltCheckBox
            // 
            this.hotkeyAltCheckBox.AutoSize = true;
            this.hotkeyAltCheckBox.Location = new System.Drawing.Point(6, 43);
            this.hotkeyAltCheckBox.Name = "hotkeyAltCheckBox";
            this.hotkeyAltCheckBox.Size = new System.Drawing.Size(46, 17);
            this.hotkeyAltCheckBox.TabIndex = 2;
            this.hotkeyAltCheckBox.Text = "ALT";
            this.hotkeyAltCheckBox.UseVisualStyleBackColor = true;
            // 
            // hotkeyCtrlCheckBox
            // 
            this.hotkeyCtrlCheckBox.AutoSize = true;
            this.hotkeyCtrlCheckBox.Location = new System.Drawing.Point(7, 20);
            this.hotkeyCtrlCheckBox.Name = "hotkeyCtrlCheckBox";
            this.hotkeyCtrlCheckBox.Size = new System.Drawing.Size(54, 17);
            this.hotkeyCtrlCheckBox.TabIndex = 0;
            this.hotkeyCtrlCheckBox.Text = "CTRL";
            this.hotkeyCtrlCheckBox.UseVisualStyleBackColor = true;
            // 
            // hotkeyComboBox
            // 
            this.hotkeyComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.hotkeyComboBox.FormattingEnabled = true;
            this.hotkeyComboBox.Location = new System.Drawing.Point(158, 29);
            this.hotkeyComboBox.Name = "hotkeyComboBox";
            this.hotkeyComboBox.Size = new System.Drawing.Size(98, 21);
            this.hotkeyComboBox.TabIndex = 5;
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
            this.authenticationBox.Size = new System.Drawing.Size(262, 110);
            this.authenticationBox.TabIndex = 1;
            this.authenticationBox.TabStop = false;
            this.authenticationBox.Text = "Authentication";
            // 
            // contextTextBox
            // 
            this.contextTextBox.Location = new System.Drawing.Point(90, 25);
            this.contextTextBox.Name = "contextTextBox";
            this.contextTextBox.Size = new System.Drawing.Size(166, 20);
            this.contextTextBox.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 28);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(43, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Context";
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Location = new System.Drawing.Point(90, 77);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.Size = new System.Drawing.Size(166, 20);
            this.passwordTextBox.TabIndex = 5;
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
            this.connectionBox.Size = new System.Drawing.Size(262, 188);
            this.connectionBox.TabIndex = 0;
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
            this.showAdvancedLabel.TabIndex = 4;
            this.showAdvancedLabel.TabStop = true;
            this.showAdvancedLabel.Text = "▸ Show advanced options";
            this.showAdvancedLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // advancedConnection
            // 
            this.advancedConnection.Controls.Add(this.label4);
            this.advancedConnection.Controls.Add(this.label3);
            this.advancedConnection.Controls.Add(this.pingIntervalTextBox);
            this.advancedConnection.Location = new System.Drawing.Point(6, 117);
            this.advancedConnection.Name = "advancedConnection";
            this.advancedConnection.Size = new System.Drawing.Size(246, 61);
            this.advancedConnection.TabIndex = 5;
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
            this.label4.TabIndex = 2;
            this.label4.Text = "seconds";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Ping interval";
            // 
            // pingIntervalTextBox
            // 
            this.pingIntervalTextBox.Location = new System.Drawing.Point(80, 29);
            this.pingIntervalTextBox.MaxLength = 5;
            this.pingIntervalTextBox.Name = "pingIntervalTextBox";
            this.pingIntervalTextBox.Size = new System.Drawing.Size(50, 20);
            this.pingIntervalTextBox.TabIndex = 1;
            // 
            // portTextBox
            // 
            this.portTextBox.Location = new System.Drawing.Point(86, 52);
            this.portTextBox.MaxLength = 5;
            this.portTextBox.Name = "portTextBox";
            this.portTextBox.Size = new System.Drawing.Size(50, 20);
            this.portTextBox.TabIndex = 3;
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
            // applyButton
            // 
            this.applyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.applyButton.Location = new System.Drawing.Point(467, 360);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(75, 23);
            this.applyButton.TabIndex = 4;
            this.applyButton.Text = "Apply";
            this.applyButton.UseVisualStyleBackColor = true;
            this.applyButton.Click += new System.EventHandler(this.applyButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.Location = new System.Drawing.Point(386, 360);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 5;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.Location = new System.Drawing.Point(305, 360);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 6;
            this.okButton.Text = "Ok";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(553, 395);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.commandsBox);
            this.Controls.Add(this.hotkeyBox);
            this.Controls.Add(this.authenticationBox);
            this.Controls.Add(this.connectionBox);
            this.Controls.Add(this.applyButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SettingsForm";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.commandsBox.ResumeLayout(false);
            this.commandsBox.PerformLayout();
            this.hotkeyBox.ResumeLayout(false);
            this.hotkeyBox.PerformLayout();
            this.authenticationBox.ResumeLayout(false);
            this.authenticationBox.PerformLayout();
            this.connectionBox.ResumeLayout(false);
            this.connectionBox.PerformLayout();
            this.advancedConnection.ResumeLayout(false);
            this.advancedConnection.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox commandsBox;
        private System.Windows.Forms.TextBox fop2WebInterfaceTextBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox hotkeyBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox hotkeyWinCheckBox;
        private System.Windows.Forms.CheckBox hotkeyShiftCheckBox;
        private System.Windows.Forms.CheckBox hotkeyAltCheckBox;
        private System.Windows.Forms.CheckBox hotkeyCtrlCheckBox;
        private System.Windows.Forms.ComboBox hotkeyComboBox;
        private System.Windows.Forms.GroupBox authenticationBox;
        private System.Windows.Forms.TextBox contextTextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox usernameTextBox;
        private System.Windows.Forms.Label label5;
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
        private System.Windows.Forms.Button applyButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.ErrorProvider errorProvider;
    }
}