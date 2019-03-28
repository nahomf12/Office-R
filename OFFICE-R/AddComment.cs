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
    public partial class AddComment : Form
    {
        public AddComment()
        {
            InitializeComponent();
            SetComboEmployee();
        }
        private void SetComboEmployee()
        {
            comboBox1.DataSource = null;
            comboBox1.Items.Clear();
            Dictionary<int, String> tasks = new Dictionary<int, string>();
            foreach (Employee employee in Employee.Find("id !=" + Program.securityStore.currentUser.id + " AND isReleased=FALSE;"))
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
            if (comboBox1.SelectedValue != null && textBox2.Text.Any())
            {
                Models.Comment task = new Models.Comment();
                task.employeeId = (int)comboBox1.SelectedValue;
                task.assignerId = Program.securityStore.currentUser.ID;
                task.description = textBox2.Text;
                Models.Comment.Create(task);
                MessageBox.Show("Commented");
                this.Hide();
            }
            else
            {
                MessageBox.Show("Please Fill All Required Fields");
            }
        }
    }
}
