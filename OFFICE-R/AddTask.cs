using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using OFFICE_R.Models;
using System.Windows.Forms;

namespace OFFICE_R
{
    public partial class AddTask : Form
    {
        public AddTask()
        {
            InitializeComponent();
        }

        private void AddTask_Load(object sender, EventArgs e)
        {
            SetComboEmployee();

        }

        private void SetComboEmployee()
        {
            comboBox1.DataSource = null;
            comboBox1.Items.Clear();
            Dictionary<int, String> tasks = new Dictionary<int, string>();
            foreach (Employee employee in Employee.Find("id != "+Program.securityStore.currentUser.id+" AND isReleased=FALSE;"))
            {
                tasks.Add(employee.id, employee.fullName + " - " + employee.username);
            }
            if (tasks.Any())
            {
                comboBox1.DataSource = new BindingSource(tasks, null);
                comboBox1.DisplayMember = "Value";
                comboBox1.ValueMember = "Key";
            }
            else
            {
                comboBox1.Items.Add("NO EMPLOYEES");
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if(textBox1.Text.Any() && comboBox1.SelectedValue!=null && textBox2.Text.Any())
            {
                Task task = new Task();
                task.title = textBox1.Text;
                task.employeeId = (int) comboBox1.SelectedValue;
                task.assignerId = Program.securityStore.currentUser.ID;
                task.description = textBox2.Text;
                Task.Create(task);
                MessageBox.Show("Assigned Task");
                this.Hide();
            }
            else
            {
                MessageBox.Show("Please Fill All Required Fields");
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
