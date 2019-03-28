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
    public partial class SignupSheet : Form
    {
        public SignupSheet()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Any() && textBox2.Text.Any())
            {
                String username = textBox1.Text;
                String password = Models.Employee.GetHashString(textBox2.Text);
                List<Models.Employee> employees = Models.Employee.Find("username='" + username + "' AND password='" + password + "'");
                if (employees.Any())
                {
                    if (!Models.Attendance.Find("employeeId="+ employees[0].ID+ " AND dateTime >= '" + DateTime.Today.Date + "'::date AND dateTime < ('" + DateTime.Today.Date + "'::date + '1 day'::interval) ").Any())
                    {
                        Models.Attendance task = new Models.Attendance();
                        task.employeeId = employees[0].ID;
                        task.dateTime = DateTime.Today;
                        Models.Attendance.Create(task);
                        MessageBox.Show("Welcome " + employees[0].fullName);
                        textBox1.Clear();
                        textBox2.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Already Signed in!!");
                        textBox1.Clear();
                        textBox2.Clear();
                    }
                 }
                else
                {
                    MessageBox.Show("Employee Not Found");
                }
            }
            else
            {
                MessageBox.Show("Please Fill All Required Fields");
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label3.Text = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString();
            timer1.Start();
        }

        private void SignupSheet_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }
    }
}
