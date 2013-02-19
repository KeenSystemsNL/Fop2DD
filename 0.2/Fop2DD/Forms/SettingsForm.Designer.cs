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
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.cmdBrowse = new System.Windows.Forms.Button();
            this.dialcmd_ArgsTextBox = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.dialcmd_WorkDirTextBox = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.dialcmd_FileTextBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.fop2WebInterfaceTextBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.hotkeyBox = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
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
            this.portTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.hostTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.advancedConnection = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pingIntervalTextBox = new System.Windows.Forms.TextBox();
            this.applyButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.commandsBox.SuspendLayout();
            this.hotkeyBox.SuspendLayout();
            this.authenticationBox.SuspendLayout();
            this.connectionBox.SuspendLayout();
            this.advancedConnection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // commandsBox
            // 
            resources.ApplyResources(this.commandsBox, "commandsBox");
            this.commandsBox.Controls.Add(this.label16);
            this.commandsBox.Controls.Add(this.label15);
            this.commandsBox.Controls.Add(this.label14);
            this.commandsBox.Controls.Add(this.cmdBrowse);
            this.commandsBox.Controls.Add(this.dialcmd_ArgsTextBox);
            this.commandsBox.Controls.Add(this.label12);
            this.commandsBox.Controls.Add(this.dialcmd_WorkDirTextBox);
            this.commandsBox.Controls.Add(this.label11);
            this.commandsBox.Controls.Add(this.dialcmd_FileTextBox);
            this.commandsBox.Controls.Add(this.label10);
            this.commandsBox.Controls.Add(this.fop2WebInterfaceTextBox);
            this.commandsBox.Controls.Add(this.label9);
            this.errorProvider.SetError(this.commandsBox, resources.GetString("commandsBox.Error"));
            this.errorProvider.SetIconAlignment(this.commandsBox, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("commandsBox.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.commandsBox, ((int)(resources.GetObject("commandsBox.IconPadding"))));
            this.commandsBox.Name = "commandsBox";
            this.commandsBox.TabStop = false;
            // 
            // label16
            // 
            resources.ApplyResources(this.label16, "label16");
            this.errorProvider.SetError(this.label16, resources.GetString("label16.Error"));
            this.label16.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.errorProvider.SetIconAlignment(this.label16, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label16.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.label16, ((int)(resources.GetObject("label16.IconPadding"))));
            this.label16.Name = "label16";
            // 
            // label15
            // 
            resources.ApplyResources(this.label15, "label15");
            this.errorProvider.SetError(this.label15, resources.GetString("label15.Error"));
            this.label15.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.errorProvider.SetIconAlignment(this.label15, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label15.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.label15, ((int)(resources.GetObject("label15.IconPadding"))));
            this.label15.Name = "label15";
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.errorProvider.SetError(this.label14, resources.GetString("label14.Error"));
            this.label14.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.errorProvider.SetIconAlignment(this.label14, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label14.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.label14, ((int)(resources.GetObject("label14.IconPadding"))));
            this.label14.Name = "label14";
            // 
            // cmdBrowse
            // 
            resources.ApplyResources(this.cmdBrowse, "cmdBrowse");
            this.errorProvider.SetError(this.cmdBrowse, resources.GetString("cmdBrowse.Error"));
            this.errorProvider.SetIconAlignment(this.cmdBrowse, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("cmdBrowse.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.cmdBrowse, ((int)(resources.GetObject("cmdBrowse.IconPadding"))));
            this.cmdBrowse.Name = "cmdBrowse";
            this.cmdBrowse.UseVisualStyleBackColor = true;
            this.cmdBrowse.Click += new System.EventHandler(this.cmdBrowse_Click);
            // 
            // dialcmd_ArgsTextBox
            // 
            resources.ApplyResources(this.dialcmd_ArgsTextBox, "dialcmd_ArgsTextBox");
            this.errorProvider.SetError(this.dialcmd_ArgsTextBox, resources.GetString("dialcmd_ArgsTextBox.Error"));
            this.errorProvider.SetIconAlignment(this.dialcmd_ArgsTextBox, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("dialcmd_ArgsTextBox.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.dialcmd_ArgsTextBox, ((int)(resources.GetObject("dialcmd_ArgsTextBox.IconPadding"))));
            this.dialcmd_ArgsTextBox.Name = "dialcmd_ArgsTextBox";
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.errorProvider.SetError(this.label12, resources.GetString("label12.Error"));
            this.errorProvider.SetIconAlignment(this.label12, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label12.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.label12, ((int)(resources.GetObject("label12.IconPadding"))));
            this.label12.Name = "label12";
            // 
            // dialcmd_WorkDirTextBox
            // 
            resources.ApplyResources(this.dialcmd_WorkDirTextBox, "dialcmd_WorkDirTextBox");
            this.errorProvider.SetError(this.dialcmd_WorkDirTextBox, resources.GetString("dialcmd_WorkDirTextBox.Error"));
            this.errorProvider.SetIconAlignment(this.dialcmd_WorkDirTextBox, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("dialcmd_WorkDirTextBox.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.dialcmd_WorkDirTextBox, ((int)(resources.GetObject("dialcmd_WorkDirTextBox.IconPadding"))));
            this.dialcmd_WorkDirTextBox.Name = "dialcmd_WorkDirTextBox";
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.errorProvider.SetError(this.label11, resources.GetString("label11.Error"));
            this.errorProvider.SetIconAlignment(this.label11, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label11.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.label11, ((int)(resources.GetObject("label11.IconPadding"))));
            this.label11.Name = "label11";
            // 
            // dialcmd_FileTextBox
            // 
            resources.ApplyResources(this.dialcmd_FileTextBox, "dialcmd_FileTextBox");
            this.errorProvider.SetError(this.dialcmd_FileTextBox, resources.GetString("dialcmd_FileTextBox.Error"));
            this.errorProvider.SetIconAlignment(this.dialcmd_FileTextBox, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("dialcmd_FileTextBox.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.dialcmd_FileTextBox, ((int)(resources.GetObject("dialcmd_FileTextBox.IconPadding"))));
            this.dialcmd_FileTextBox.Name = "dialcmd_FileTextBox";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.errorProvider.SetError(this.label10, resources.GetString("label10.Error"));
            this.errorProvider.SetIconAlignment(this.label10, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label10.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.label10, ((int)(resources.GetObject("label10.IconPadding"))));
            this.label10.Name = "label10";
            // 
            // fop2WebInterfaceTextBox
            // 
            resources.ApplyResources(this.fop2WebInterfaceTextBox, "fop2WebInterfaceTextBox");
            this.errorProvider.SetError(this.fop2WebInterfaceTextBox, resources.GetString("fop2WebInterfaceTextBox.Error"));
            this.errorProvider.SetIconAlignment(this.fop2WebInterfaceTextBox, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("fop2WebInterfaceTextBox.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.fop2WebInterfaceTextBox, ((int)(resources.GetObject("fop2WebInterfaceTextBox.IconPadding"))));
            this.fop2WebInterfaceTextBox.Name = "fop2WebInterfaceTextBox";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.errorProvider.SetError(this.label9, resources.GetString("label9.Error"));
            this.errorProvider.SetIconAlignment(this.label9, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label9.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.label9, ((int)(resources.GetObject("label9.IconPadding"))));
            this.label9.Name = "label9";
            // 
            // hotkeyBox
            // 
            resources.ApplyResources(this.hotkeyBox, "hotkeyBox");
            this.hotkeyBox.Controls.Add(this.label13);
            this.hotkeyBox.Controls.Add(this.label8);
            this.hotkeyBox.Controls.Add(this.hotkeyWinCheckBox);
            this.hotkeyBox.Controls.Add(this.hotkeyShiftCheckBox);
            this.hotkeyBox.Controls.Add(this.hotkeyAltCheckBox);
            this.hotkeyBox.Controls.Add(this.hotkeyCtrlCheckBox);
            this.hotkeyBox.Controls.Add(this.hotkeyComboBox);
            this.errorProvider.SetError(this.hotkeyBox, resources.GetString("hotkeyBox.Error"));
            this.errorProvider.SetIconAlignment(this.hotkeyBox, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("hotkeyBox.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.hotkeyBox, ((int)(resources.GetObject("hotkeyBox.IconPadding"))));
            this.hotkeyBox.Name = "hotkeyBox";
            this.hotkeyBox.TabStop = false;
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.errorProvider.SetError(this.label13, resources.GetString("label13.Error"));
            this.label13.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.errorProvider.SetIconAlignment(this.label13, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label13.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.label13, ((int)(resources.GetObject("label13.IconPadding"))));
            this.label13.Name = "label13";
            this.label13.UseMnemonic = false;
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.errorProvider.SetError(this.label8, resources.GetString("label8.Error"));
            this.errorProvider.SetIconAlignment(this.label8, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label8.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.label8, ((int)(resources.GetObject("label8.IconPadding"))));
            this.label8.Name = "label8";
            // 
            // hotkeyWinCheckBox
            // 
            resources.ApplyResources(this.hotkeyWinCheckBox, "hotkeyWinCheckBox");
            this.errorProvider.SetError(this.hotkeyWinCheckBox, resources.GetString("hotkeyWinCheckBox.Error"));
            this.errorProvider.SetIconAlignment(this.hotkeyWinCheckBox, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("hotkeyWinCheckBox.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.hotkeyWinCheckBox, ((int)(resources.GetObject("hotkeyWinCheckBox.IconPadding"))));
            this.hotkeyWinCheckBox.Name = "hotkeyWinCheckBox";
            this.hotkeyWinCheckBox.UseVisualStyleBackColor = true;
            // 
            // hotkeyShiftCheckBox
            // 
            resources.ApplyResources(this.hotkeyShiftCheckBox, "hotkeyShiftCheckBox");
            this.errorProvider.SetError(this.hotkeyShiftCheckBox, resources.GetString("hotkeyShiftCheckBox.Error"));
            this.errorProvider.SetIconAlignment(this.hotkeyShiftCheckBox, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("hotkeyShiftCheckBox.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.hotkeyShiftCheckBox, ((int)(resources.GetObject("hotkeyShiftCheckBox.IconPadding"))));
            this.hotkeyShiftCheckBox.Name = "hotkeyShiftCheckBox";
            this.hotkeyShiftCheckBox.UseVisualStyleBackColor = true;
            // 
            // hotkeyAltCheckBox
            // 
            resources.ApplyResources(this.hotkeyAltCheckBox, "hotkeyAltCheckBox");
            this.errorProvider.SetError(this.hotkeyAltCheckBox, resources.GetString("hotkeyAltCheckBox.Error"));
            this.errorProvider.SetIconAlignment(this.hotkeyAltCheckBox, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("hotkeyAltCheckBox.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.hotkeyAltCheckBox, ((int)(resources.GetObject("hotkeyAltCheckBox.IconPadding"))));
            this.hotkeyAltCheckBox.Name = "hotkeyAltCheckBox";
            this.hotkeyAltCheckBox.UseVisualStyleBackColor = true;
            // 
            // hotkeyCtrlCheckBox
            // 
            resources.ApplyResources(this.hotkeyCtrlCheckBox, "hotkeyCtrlCheckBox");
            this.errorProvider.SetError(this.hotkeyCtrlCheckBox, resources.GetString("hotkeyCtrlCheckBox.Error"));
            this.errorProvider.SetIconAlignment(this.hotkeyCtrlCheckBox, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("hotkeyCtrlCheckBox.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.hotkeyCtrlCheckBox, ((int)(resources.GetObject("hotkeyCtrlCheckBox.IconPadding"))));
            this.hotkeyCtrlCheckBox.Name = "hotkeyCtrlCheckBox";
            this.hotkeyCtrlCheckBox.UseVisualStyleBackColor = true;
            // 
            // hotkeyComboBox
            // 
            resources.ApplyResources(this.hotkeyComboBox, "hotkeyComboBox");
            this.hotkeyComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.errorProvider.SetError(this.hotkeyComboBox, resources.GetString("hotkeyComboBox.Error"));
            this.hotkeyComboBox.FormattingEnabled = true;
            this.errorProvider.SetIconAlignment(this.hotkeyComboBox, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("hotkeyComboBox.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.hotkeyComboBox, ((int)(resources.GetObject("hotkeyComboBox.IconPadding"))));
            this.hotkeyComboBox.Name = "hotkeyComboBox";
            // 
            // authenticationBox
            // 
            resources.ApplyResources(this.authenticationBox, "authenticationBox");
            this.authenticationBox.Controls.Add(this.contextTextBox);
            this.authenticationBox.Controls.Add(this.label7);
            this.authenticationBox.Controls.Add(this.passwordTextBox);
            this.authenticationBox.Controls.Add(this.label6);
            this.authenticationBox.Controls.Add(this.usernameTextBox);
            this.authenticationBox.Controls.Add(this.label5);
            this.errorProvider.SetError(this.authenticationBox, resources.GetString("authenticationBox.Error"));
            this.errorProvider.SetIconAlignment(this.authenticationBox, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("authenticationBox.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.authenticationBox, ((int)(resources.GetObject("authenticationBox.IconPadding"))));
            this.authenticationBox.Name = "authenticationBox";
            this.authenticationBox.TabStop = false;
            // 
            // contextTextBox
            // 
            resources.ApplyResources(this.contextTextBox, "contextTextBox");
            this.errorProvider.SetError(this.contextTextBox, resources.GetString("contextTextBox.Error"));
            this.errorProvider.SetIconAlignment(this.contextTextBox, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("contextTextBox.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.contextTextBox, ((int)(resources.GetObject("contextTextBox.IconPadding"))));
            this.contextTextBox.Name = "contextTextBox";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.errorProvider.SetError(this.label7, resources.GetString("label7.Error"));
            this.errorProvider.SetIconAlignment(this.label7, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label7.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.label7, ((int)(resources.GetObject("label7.IconPadding"))));
            this.label7.Name = "label7";
            // 
            // passwordTextBox
            // 
            resources.ApplyResources(this.passwordTextBox, "passwordTextBox");
            this.errorProvider.SetError(this.passwordTextBox, resources.GetString("passwordTextBox.Error"));
            this.errorProvider.SetIconAlignment(this.passwordTextBox, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("passwordTextBox.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.passwordTextBox, ((int)(resources.GetObject("passwordTextBox.IconPadding"))));
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.UseSystemPasswordChar = true;
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.errorProvider.SetError(this.label6, resources.GetString("label6.Error"));
            this.errorProvider.SetIconAlignment(this.label6, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label6.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.label6, ((int)(resources.GetObject("label6.IconPadding"))));
            this.label6.Name = "label6";
            // 
            // usernameTextBox
            // 
            resources.ApplyResources(this.usernameTextBox, "usernameTextBox");
            this.errorProvider.SetError(this.usernameTextBox, resources.GetString("usernameTextBox.Error"));
            this.errorProvider.SetIconAlignment(this.usernameTextBox, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("usernameTextBox.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.usernameTextBox, ((int)(resources.GetObject("usernameTextBox.IconPadding"))));
            this.usernameTextBox.Name = "usernameTextBox";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.errorProvider.SetError(this.label5, resources.GetString("label5.Error"));
            this.errorProvider.SetIconAlignment(this.label5, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label5.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.label5, ((int)(resources.GetObject("label5.IconPadding"))));
            this.label5.Name = "label5";
            // 
            // connectionBox
            // 
            resources.ApplyResources(this.connectionBox, "connectionBox");
            this.connectionBox.Controls.Add(this.portTextBox);
            this.connectionBox.Controls.Add(this.label2);
            this.connectionBox.Controls.Add(this.hostTextBox);
            this.connectionBox.Controls.Add(this.label1);
            this.errorProvider.SetError(this.connectionBox, resources.GetString("connectionBox.Error"));
            this.errorProvider.SetIconAlignment(this.connectionBox, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("connectionBox.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.connectionBox, ((int)(resources.GetObject("connectionBox.IconPadding"))));
            this.connectionBox.Name = "connectionBox";
            this.connectionBox.TabStop = false;
            // 
            // portTextBox
            // 
            resources.ApplyResources(this.portTextBox, "portTextBox");
            this.errorProvider.SetError(this.portTextBox, resources.GetString("portTextBox.Error"));
            this.errorProvider.SetIconAlignment(this.portTextBox, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("portTextBox.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.portTextBox, ((int)(resources.GetObject("portTextBox.IconPadding"))));
            this.portTextBox.Name = "portTextBox";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.errorProvider.SetError(this.label2, resources.GetString("label2.Error"));
            this.errorProvider.SetIconAlignment(this.label2, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label2.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.label2, ((int)(resources.GetObject("label2.IconPadding"))));
            this.label2.Name = "label2";
            // 
            // hostTextBox
            // 
            resources.ApplyResources(this.hostTextBox, "hostTextBox");
            this.errorProvider.SetError(this.hostTextBox, resources.GetString("hostTextBox.Error"));
            this.errorProvider.SetIconAlignment(this.hostTextBox, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("hostTextBox.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.hostTextBox, ((int)(resources.GetObject("hostTextBox.IconPadding"))));
            this.hostTextBox.Name = "hostTextBox";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.errorProvider.SetError(this.label1, resources.GetString("label1.Error"));
            this.errorProvider.SetIconAlignment(this.label1, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label1.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.label1, ((int)(resources.GetObject("label1.IconPadding"))));
            this.label1.Name = "label1";
            // 
            // advancedConnection
            // 
            resources.ApplyResources(this.advancedConnection, "advancedConnection");
            this.advancedConnection.Controls.Add(this.label4);
            this.advancedConnection.Controls.Add(this.label3);
            this.advancedConnection.Controls.Add(this.pingIntervalTextBox);
            this.errorProvider.SetError(this.advancedConnection, resources.GetString("advancedConnection.Error"));
            this.errorProvider.SetIconAlignment(this.advancedConnection, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("advancedConnection.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.advancedConnection, ((int)(resources.GetObject("advancedConnection.IconPadding"))));
            this.advancedConnection.Name = "advancedConnection";
            this.advancedConnection.TabStop = false;
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.errorProvider.SetError(this.label4, resources.GetString("label4.Error"));
            this.errorProvider.SetIconAlignment(this.label4, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label4.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.label4, ((int)(resources.GetObject("label4.IconPadding"))));
            this.label4.Name = "label4";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.errorProvider.SetError(this.label3, resources.GetString("label3.Error"));
            this.errorProvider.SetIconAlignment(this.label3, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label3.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.label3, ((int)(resources.GetObject("label3.IconPadding"))));
            this.label3.Name = "label3";
            // 
            // pingIntervalTextBox
            // 
            resources.ApplyResources(this.pingIntervalTextBox, "pingIntervalTextBox");
            this.errorProvider.SetError(this.pingIntervalTextBox, resources.GetString("pingIntervalTextBox.Error"));
            this.errorProvider.SetIconAlignment(this.pingIntervalTextBox, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("pingIntervalTextBox.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.pingIntervalTextBox, ((int)(resources.GetObject("pingIntervalTextBox.IconPadding"))));
            this.pingIntervalTextBox.Name = "pingIntervalTextBox";
            // 
            // applyButton
            // 
            resources.ApplyResources(this.applyButton, "applyButton");
            this.errorProvider.SetError(this.applyButton, resources.GetString("applyButton.Error"));
            this.errorProvider.SetIconAlignment(this.applyButton, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("applyButton.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.applyButton, ((int)(resources.GetObject("applyButton.IconPadding"))));
            this.applyButton.Name = "applyButton";
            this.applyButton.UseVisualStyleBackColor = true;
            this.applyButton.Click += new System.EventHandler(this.applyButton_Click);
            // 
            // cancelButton
            // 
            resources.ApplyResources(this.cancelButton, "cancelButton");
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.errorProvider.SetError(this.cancelButton, resources.GetString("cancelButton.Error"));
            this.errorProvider.SetIconAlignment(this.cancelButton, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("cancelButton.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.cancelButton, ((int)(resources.GetObject("cancelButton.IconPadding"))));
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // okButton
            // 
            resources.ApplyResources(this.okButton, "okButton");
            this.errorProvider.SetError(this.okButton, resources.GetString("okButton.Error"));
            this.errorProvider.SetIconAlignment(this.okButton, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("okButton.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.okButton, ((int)(resources.GetObject("okButton.IconPadding"))));
            this.okButton.Name = "okButton";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            resources.ApplyResources(this.errorProvider, "errorProvider");
            // 
            // tabControl1
            // 
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.errorProvider.SetError(this.tabControl1, resources.GetString("tabControl1.Error"));
            this.errorProvider.SetIconAlignment(this.tabControl1, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("tabControl1.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.tabControl1, ((int)(resources.GetObject("tabControl1.IconPadding"))));
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // tabPage1
            // 
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Controls.Add(this.connectionBox);
            this.tabPage1.Controls.Add(this.advancedConnection);
            this.tabPage1.Controls.Add(this.authenticationBox);
            this.errorProvider.SetError(this.tabPage1, resources.GetString("tabPage1.Error"));
            this.errorProvider.SetIconAlignment(this.tabPage1, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("tabPage1.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.tabPage1, ((int)(resources.GetObject("tabPage1.IconPadding"))));
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.Controls.Add(this.commandsBox);
            this.errorProvider.SetError(this.tabPage2, resources.GetString("tabPage2.Error"));
            this.errorProvider.SetIconAlignment(this.tabPage2, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("tabPage2.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.tabPage2, ((int)(resources.GetObject("tabPage2.IconPadding"))));
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            resources.ApplyResources(this.tabPage3, "tabPage3");
            this.tabPage3.Controls.Add(this.hotkeyBox);
            this.errorProvider.SetError(this.tabPage3, resources.GetString("tabPage3.Error"));
            this.errorProvider.SetIconAlignment(this.tabPage3, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("tabPage3.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.tabPage3, ((int)(resources.GetObject("tabPage3.IconPadding"))));
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // SettingsForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.applyButton);
            this.MaximizeBox = false;
            this.Name = "SettingsForm";
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
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
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
        private System.Windows.Forms.TextBox dialcmd_ArgsTextBox;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox dialcmd_WorkDirTextBox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox dialcmd_FileTextBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button cmdBrowse;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
    }
}