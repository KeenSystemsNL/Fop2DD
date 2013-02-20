using Fop2DD.Core.Common;
using Fop2DD.Core.Hotkeys;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;
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
            if (ValidateForm())
            {
                var s = Properties.Settings.Default;
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
                return true;
            }
            return false;
        }

        private bool ValidateForm()
        {
            errorProvider.Clear();

            //Tab 0
            if (string.IsNullOrWhiteSpace(hostTextBox.Text))
                errorProvider.SetError(hostTextBox, Properties.Resources.settings_err_invalidhost);

            int port;
            if (!int.TryParse(portTextBox.Text, out port))
                errorProvider.SetError(portTextBox, Properties.Resources.settings_err_portnumber);
            else if (!Validators.IsValidPort(port))
                errorProvider.SetError(portTextBox, string.Format(Properties.Resources.settings_err_portrange, Validators.PORT_MIN, Validators.PORT_MAX));

            int pinginterval;
            if (!int.TryParse(pingIntervalTextBox.Text, out pinginterval))
                errorProvider.SetError(pingIntervalTextBox, Properties.Resources.settings_err_pingnumber);
            else if (!Validators.IsValidPingInterval(pinginterval))
                errorProvider.SetError(pingIntervalTextBox, string.Format(Properties.Resources.settings_err_pingrange, Validators.PING_MIN, Validators.PING_MAX));

            if (string.IsNullOrWhiteSpace(contextTextBox.Text))
                errorProvider.SetError(contextTextBox, Properties.Resources.settings_err_nocontext);

            if (string.IsNullOrWhiteSpace(usernameTextBox.Text))
                errorProvider.SetError(usernameTextBox, Properties.Resources.settings_err_nouser);

            //Tab 1
            if (!string.IsNullOrWhiteSpace(fop2WebInterfaceTextBox.Text))
                if (!Validators.IsValidHttpUrl(fop2WebInterfaceTextBox.Text))
                    errorProvider.SetError(fop2WebInterfaceTextBox, Properties.Resources.settings_err_invalidfop2url);

            //Tab2
            if ((Key)hotkeyComboBox.SelectedItem != Key.None)
                if (!hotkeyCtrlCheckBox.Checked && !hotkeyShiftCheckBox.Checked && !hotkeyWinCheckBox.Checked && !hotkeyAltCheckBox.Checked)
                    errorProvider.SetError(hotkeyComboBox, Properties.Resources.settings_err_invalidhotkeycombo);

            //Select first tab with error(s) (if any)
            int[] errorspertab = new int[tabControl.TabPages.Count];
            for (int i = 0; i < tabControl.TabPages.Count; i++)
                errorspertab[i] = GetControlErrorCount(tabControl.TabPages[i]);

            if (errorspertab.Sum() > 0)
                tabControl.SelectedIndex = errorspertab.ToList().FindIndex(e => e > 0);

            //Return result
            return errorspertab.Sum() == 0;
        }

        private int GetControlErrorCount(Control control)
        {
            int errors = 0;

            if (!string.IsNullOrEmpty(errorProvider.GetError(control)))
                errors++;

            foreach (Control c in control.Controls)
                errors += GetControlErrorCount(c);
            return errors;
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
