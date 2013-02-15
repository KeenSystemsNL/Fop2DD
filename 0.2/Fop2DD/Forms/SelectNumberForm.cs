using System.Windows.Forms;

namespace Fop2DD
{
    public partial class SelectNumberForm : Form
    {
        public string SelectedNumber { get; private set; }

        public SelectNumberForm(string[] numbers)
        {
            InitializeComponent();
            numberBox.DataSource = numbers;
        }
    }
}
