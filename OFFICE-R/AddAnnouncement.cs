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
    public partial class AddAnnouncement : Form
    {
        public AddAnnouncement()
        {
            InitializeComponent();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (textBox2.Text.Any())
            {
                Models.Announcement task = new Models.Announcement();
                task.description = textBox2.Text;
                Models.Announcement.Create(task);
                MessageBox.Show("Announced");
                this.Hide();
            }
            else
            {
                MessageBox.Show("Please Fill All Required Fields");
            }
        }
    }
}
