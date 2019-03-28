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
    public partial class Employees : Form
    {
        public Employees()
        {
            InitializeComponent();
            FormClosing += new FormClosingEventHandler(Login_FormClosing);
        }

        private void Login_FormClosing(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Employees_Load(object sender, EventArgs e)
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
            SetListEmployee();
            profilePicture.BackgroundImageLayout = ImageLayout.Zoom;
        }

        private void SetListEmployee()
        {
            listBox1.DataSource = null;
            listBox1.Items.Clear();
            Dictionary<int, String> tasks = new Dictionary<int, string>();
            foreach (Models.Employee task in Models.Employee.Find("isReleased=FALSE AND isAdmin=FALSE"))
            {
                tasks.Add(task.id, task.fullName + " - " + task.username + " - " + task.salary + " Birr");
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

        private void button11_Click(object sender, EventArgs e)
        {
            SetListEmployee();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            new AddEmployee().Show();
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

        private void button9_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedValue != null)
            {
                MessageBox.Show("Fired Employee");
                Program.database.ExecuteUpdate("UPDATE EMPLOYEE SET isReleased=TRUE WHERE id=" + listBox1.SelectedValue +";");
                SetListEmployee();
            }
            else
            {
                MessageBox.Show("Please select an Employee!");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Attendances().Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString();
            timer1.Start();
        }
    }
}
