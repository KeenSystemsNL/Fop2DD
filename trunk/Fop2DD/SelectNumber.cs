using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Fop2DD
{
    public partial class SelectNumber : Form
    {
        public SelectNumber(string[] numbers)
        {
            InitializeComponent();
            numberBox.DataSource = numbers;
        }

        public string SelectedNumber { get; private set; }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.SelectedNumber = numberBox.Text;
            this.DialogResult = DialogResult.OK;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
