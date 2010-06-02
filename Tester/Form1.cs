using System;
using System.Windows.Forms;

namespace Tester
{
    public partial class Form1 : Form
    {
        string desktop;

        public Form1()
        {
            InitializeComponent();
            desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop, Environment.SpecialFolderOption.None);
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
