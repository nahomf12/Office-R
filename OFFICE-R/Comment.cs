using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using OFFICE_R.Models;
using System.Windows.Forms;
using System.IO;

namespace OFFICE_R
{
    public partial class Comment : Form
    {
        public Comment()
        {
            InitializeComponent();
            FormClosing += new FormClosingEventHandler(Login_FormClosing);
        }

        private void Login_FormClosing(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void Comment_Load(object sender, EventArgs e)
        {
            timer1.Start();
            button1.Text = Program.securityStore.currentUser.username;
            button7.Text = Program.securityStore.currentUser.fullName;
            if (File.Exists(@"C:\Users\nahom\Desktop\OFFICE-R\OFFICE-R\Resources\static\img\propic\" + Program.securityStore.currentUser.username + ".jpg"))
            {
                profilePicture.BackgroundImage = Image.FromFile(@"C:\Users\nahom\Desktop\OFFICE-R\OFFICE-R\Resources\static\img\propic\" + Program.securityStore.currentUser.username + ".jpg");
            }
            else
            {
                profilePicture.BackgroundImage = Image.FromFile(@"C:\Users\nahom\Desktop\OFFICE-R\user.png");
            }
            SetListComment();
            if (!Program.securityStore.currentUser.isAdmin)
            {
                button5.Hide();
                button6.Hide();
            }
            profilePicture.BackgroundImageLayout = ImageLayout.Zoom;
        }

        private void SetListComment()
        {
            listBox1.DataSource = null;
            listBox1.Items.Clear();
            Dictionary<int, String> tasks = new Dictionary<int, string>();
            foreach (Models.Comment task in Models.Comment.Find("employeeId=" + Program.securityStore.currentUser.ID))
            {
                tasks.Add(task.id, task.description + " By " + task.assigner.fullName);
            }
            if (tasks.Any())
            {
                listBox1.DataSource = new BindingSource(tasks, null);
                listBox1.DisplayMember = "Value";
                listBox1.ValueMember = "Key";
            }
            else
            {
                listBox1.Items.Add("NO COMMENTS FOR NOW");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Home().Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString();
            timer1.Start();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            new Home().Show();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            SetListComment();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            new AddComment().Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Announcements().Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {

            this.Hide();
            new Employees().Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Attendances().Show();
        }
    }
}
