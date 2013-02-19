﻿using Fop2DD.Core.Hotkeys;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using swi = System.Windows.Input;

namespace Fop2DD
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (this.ApplySettings())
                this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void applyButton_Click(object sender, EventArgs e)
        {
            this.ApplySettings();
        }

        private bool ApplySettings()
        {
            var s = Properties.Settings.Default;
            try
            {
                s.Host = hostTextBox.Text.Trim();
                s.Port = int.Parse(portTextBox.Text);

                s.PBXContext = contextTextBox.Text.Trim();
                s.Username = usernameTextBox.Text.Trim();
                s.Password = passwordTextBox.Text;

                s.PingInterval = int.Parse(pingIntervalTextBox.Text);
                s.FOP2Url = fop2WebInterfaceTextBox.Text.Trim();

                s.GrabMinLength = 6; //TODO: Create interface element
                s.DialCmd_MinLength = 6; //TODO: Create interface element

                swi.ModifierKeys modifiers = swi.ModifierKeys.None;
                modifiers |= hotkeyAltCheckBox.Checked ? swi.ModifierKeys.Alt : swi.ModifierKeys.None;
                modifiers |= hotkeyCtrlCheckBox.Checked ? swi.ModifierKeys.Control : swi.ModifierKeys.None;
                modifiers |= hotkeyWinCheckBox.Checked ? swi.ModifierKeys.Windows : swi.ModifierKeys.None;
                modifiers |= hotkeyShiftCheckBox.Checked ? swi.ModifierKeys.Shift : swi.ModifierKeys.None;

                s.GlobalDialHotkey = new DDHotkey((swi.Key)hotkeyComboBox.SelectedValue, modifiers).ToString();

                s.DialCmd_File = dialcmd_FileTextBox.Text;
                s.DialCmd_WorkDir = dialcmd_WorkDirTextBox.Text;
                s.DialCmd_Args = dialcmd_ArgsTextBox.Text;

                s.Save();
            }
            catch
            {
                //TODO: make this a bit more specific :P
                this.errorProvider.SetError(okButton, "Invalid setting, somewhere...");

                s.Reload();
                return false;
            }
            return true;
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            var s = Properties.Settings.Default;

            hostTextBox.Text = s.Host;
            portTextBox.Text = s.Port.ToString();

            contextTextBox.Text = s.PBXContext;
            usernameTextBox.Text = s.Username;
            passwordTextBox.Text = s.Password;

            pingIntervalTextBox.Text = s.PingInterval.ToString();
            fop2WebInterfaceTextBox.Text = s.FOP2Url;

            hotkeyComboBox.DataSource = Enum.GetValues(typeof(swi.Key)).Cast<swi.Key>();

            DDHotkey hk = DDHotkey.Parse(s.GlobalDialHotkey);
            hotkeyComboBox.SelectedItem = hk.Key;
            hotkeyAltCheckBox.Checked = (hk.Modifier & swi.ModifierKeys.Alt) != 0;
            hotkeyCtrlCheckBox.Checked = (hk.Modifier & swi.ModifierKeys.Control) != 0;
            hotkeyWinCheckBox.Checked = (hk.Modifier & swi.ModifierKeys.Windows) != 0;
            hotkeyShiftCheckBox.Checked = (hk.Modifier & swi.ModifierKeys.Shift) != 0;

            dialcmd_FileTextBox.Text = s.DialCmd_File;
            dialcmd_WorkDirTextBox.Text = s.DialCmd_WorkDir;
            dialcmd_ArgsTextBox.Text = s.DialCmd_Args;

            //TODO: Create interface element for "GrabMinLength"
            //TODO: Create interface element for "DialCmd_MinLength"
        }

        private void cmdBrowse_Click(object sender, EventArgs e)
        {
            using (var f = new OpenFileDialog())
            {
                f.CheckFileExists = true;
                f.CheckPathExists = true;
                f.Filter = Properties.Resources.dialcmd_filter;
                f.FilterIndex = 0;
                f.Multiselect = false;
                f.Title = Properties.Resources.dialcmd_dialogtitle;

                var file = dialcmd_FileTextBox.Text.Trim();
                if (!string.IsNullOrEmpty(file))
                {
                    if (Directory.Exists(Path.GetDirectoryName(file)))
                        f.InitialDirectory = Path.GetDirectoryName(Path.GetDirectoryName(file));
                    if (File.Exists(Path.GetFileName(file)))
                        f.FileName = Path.GetFileName(file);
                }
                
                if (f.ShowDialog() == DialogResult.OK)
                {
                    dialcmd_FileTextBox.Text = f.FileName;
                    dialcmd_WorkDirTextBox.Text = Path.GetDirectoryName(f.FileName);
                }
            }
        }
    }
}