using System;
using System.Windows.Forms;

namespace WindowsFormsAppFruitCalc
{
    public partial class FormAbout : Form
    {
        public FormAbout()
        {
            InitializeComponent();
        }

        private void FormAbout_Load(object sender, EventArgs e)
        {
            lblMac.Text = Common.GetMacAddress();
        }
    }
}
