using APITesting.Activity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APITestAutomationFW
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            //label1.Visible = false;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Visible = true;
            TestCaseExecutor.getInstance().ExecuteTests();
            label1.Text = "Execution completed.";
        }
    }
}
