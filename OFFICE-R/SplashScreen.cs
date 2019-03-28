using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OFFICE_R.Models;

namespace OFFICE_R
{
    public partial class SplashScreen : Form
    {
        public SplashScreen()
        {
            InitializeComponent();
        }

        public int timeLeft { get; set; }

        private void SplashScreen_Load(object sender, EventArgs e)
        {
            timeLeft = 20;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (timeLeft > 0)
            {
                timeLeft = timeLeft - 1;
                float progress = ((20f - timeLeft) / 20f);
                label1.Text = "Loading "+Convert.ToString(progress*100)+"%";
            }
            else
            {
                timer1.Stop();
                new Login().Show();
                this.Hide();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
