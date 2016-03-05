using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Photoman.UserInterface
{
    public partial class AboutBox : Form
    {
        public AboutBox()
        {
            InitializeComponent();
        }

        private void AboutBox_Load(object sender, EventArgs e)
        {
            lblVersion.Text = "Photoman version " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            lblCredits.Text = Constants.strCredits;
            lblLicenceInfo.Text = Constants.strLicense;
        }
    }
}
