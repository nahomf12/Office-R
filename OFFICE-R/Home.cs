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
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
            FormClosing += new FormClosingEventHandler(Login_FormClosing);
        }

        private void Home_Load(object sender, EventArgs e)
        {
            timer1.Start();
            button1.Text = Program.securityStore.currentUser.username;
            button7.Text = Program.securityStore.currentUser.fullName;
            Console.WriteLine(@"C:\Users\nahom\Desktop\OFFICE-R\OFFICE-R\Resources\static\img\propic\" + Program.securityStore.currentUser.username + ".jpg");
            if (File.Exists(@"C:\Users\nahom\Desktop\OFFICE-R\OFFICE-R\Resources\static\img\propic\" + Program.securityStore.currentUser.username + ".jpg")){
                profilePicture.BackgroundImage = Image.FromFile(@"C:\Users\nahom\Desktop\OFFICE-R\OFFICE-R\Resources\static\img\propic\" + Program.securityStore.currentUser.username+".jpg");
            }
            else
            {
                profilePicture.BackgroundImage = Image.FromFile(@"C:\Users\nahom\Desktop\OFFICE-R\user.png");
            }
            SetListTask();
            if (!Program.securityStore.currentUser.isAdmin)
            {
                button9.Hide();
                button10.Hide();
                button5.Hide();
                button6.Hide();
            }
            profilePicture.BackgroundImageLayout = ImageLayout.Zoom;
        }

        private void Login_FormClosing(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void SetListTask()
        {
            listBox1.DataSource = null;
            listBox1.Items.Clear();
            Dictionary<int, String> tasks = new Dictionary<int, string>();
            if (Program.securityStore.currentUser.isAdmin)
            {
                foreach (Task task in Task.Find("isComplete=TRUE AND isChecked=FALSE;"))
                {
                    tasks.Add(task.id, task.title + " For " + task.employee.fullName);
                }
                if (tasks.Any())
                {
                    listBox1.DataSource = new BindingSource(tasks, null);
                    listBox1.DisplayMember = "Value";
                    listBox1.ValueMember = "Key";
                }
                else
                {
                    listBox1.Items.Add("NO TASKS FOR NOW");
                }

            }
            else
            {
                foreach (Task task in Task.Find("employeeId=" + Program.securityStore.currentUser.ID + " AND isComplete=FALSE;"))
                {
                    tasks.Add(task.id, task.title + " By " + task.assigner.fullName);
                }
                if (tasks.Any())
                {
                    listBox1.DataSource = new BindingSource(tasks, null);
                    listBox1.DisplayMember = "Value";
                    listBox1.ValueMember = "Key";
                }
                else
                {
                    listBox1.Items.Add("NO TASKS FOR NOW");
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToLongDateString()+ " " + DateTime.Now.ToLongTimeString();
            timer1.Start();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedValue != null) {
                MessageBox.Show("Completed task");
                Program.database.ExecuteUpdate("UPDATE task SET iscomplete=TRUE WHERE id="+listBox1.SelectedValue+";");
                SetListTask();
            }
            else
            {
                MessageBox.Show("Please select a task!");
            }
        }

        private void dataTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedValue != null)
            {
                MessageBox.Show("Checked task");
                Program.database.ExecuteUpdate("UPDATE task SET ischecked=TRUE WHERE id=" + listBox1.SelectedValue + ";");
                SetListTask();
            }
            else
            {
                MessageBox.Show("Please select a task!");
            }

        }

        private void button9_Click(object sender, EventArgs e)
        {
            new AddTask().Show();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            SetListTask();
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

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Attendances().Show();
        }
    }
}
