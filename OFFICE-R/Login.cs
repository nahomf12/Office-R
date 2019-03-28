using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OFFICE_R
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            FormClosing += new FormClosingEventHandler(Login_FormClosing);
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void Login_FormClosing(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Program.securityStore.Login(textBox1.Text, textBox2.Text)) {
                new Home().Show();
                this.Hide();
            }
            else
            {
                label4.Text = "Check your username and password!!";
            }
        }
    }
}
