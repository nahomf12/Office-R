using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace OFFICE_R
{
    public partial class Announcements : Form
    {
        public Announcements()
        {
            InitializeComponent();
            FormClosing += new FormClosingEventHandler(Login_FormClosing);
        }

        private void Login_FormClosing(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Comment().Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Home().Show();
        }

        private void Announcements_Load(object sender, EventArgs e)
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
            SetListAnnouncement();
            if (!Program.securityStore.currentUser.isAdmin)
            {
                button5.Hide();
                button6.Hide();
                button8.Hide();
                button9.Hide();
            }
            profilePicture.BackgroundImageLayout = ImageLayout.Zoom;
        }

        private void SetListAnnouncement()
        {
            listBox1.DataSource = null;
            listBox1.Items.Clear();
            Dictionary<int, String> tasks = new Dictionary<int, string>();
            foreach (Models.Announcement task in Models.Announcement.FindAll())
            {
                tasks.Add(task.id, task.description);
            }
            if (tasks.Any())
            {
                listBox1.DataSource = new BindingSource(tasks, null);
                listBox1.DisplayMember = "Value";
                listBox1.ValueMember = "Key";
            }
            else
            {
                listBox1.Items.Add("NO ANNOUNCEMENT FOR NOW");
            }
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString();
            timer1.Start();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            SetListAnnouncement();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            new AddAnnouncement().Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedValue != null)
            {
                MessageBox.Show("Removed Announcement");
                Models.Announcement.Remove((int)listBox1.SelectedValue);
                SetListAnnouncement();
            }
            else
            {
                MessageBox.Show("Please select an announcement!");
            }
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
