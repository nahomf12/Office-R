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
    public partial class Attendances : Form
    {
        public Attendances()
        {
            InitializeComponent();
            FormClosing += new FormClosingEventHandler(Login_FormClosing);
        }

        private void Login_FormClosing(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Home().Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Comment().Show();
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

        private void Attendances_Load(object sender, EventArgs e)
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
            SetListAttendance();
            if (!Program.securityStore.currentUser.isAdmin)
            {
                button5.Hide();
                button6.Hide();
            }
            profilePicture.BackgroundImageLayout = ImageLayout.Zoom;
        }
        private void SetListAttendance()
        {
            listBox1.DataSource = null;
            listBox1.Items.Clear();
            Dictionary<int, String> tasks = new Dictionary<int, string>();
            foreach (Models.Attendance task in Models.Attendance.Find("dateTime >= '"+DateTime.Today.Date+ "'::date AND dateTime < ('" + DateTime.Today.Date + "'::date + '1 day'::interval)"))
            {
                tasks.Add(task.id, task.employee.fullName + " At " + task.dateTime);
            }
            if (tasks.Any())
            {
                listBox1.DataSource = new BindingSource(tasks, null);
                listBox1.DisplayMember = "Value";
                listBox1.ValueMember = "Key";
            }
            else
            {
                listBox1.Items.Add("NO ATTENDEE");
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            SetListAttendance();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            SignupSheet sheet = new SignupSheet();
            sheet.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString();
            timer1.Start();
        }
    }
}
