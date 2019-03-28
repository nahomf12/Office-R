using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OFFICE_R.Models;
using DarrenLee.Media;

namespace OFFICE_R
{
    public partial class AddEmployee : Form
    {
        Camera camera = new Camera();
        public AddEmployee()
        {
            InitializeComponent();
            this.FormClosing += AddEmployee_FormClosing;
        }

        private void AddEmployee_FormClosing(object sender, FormClosingEventArgs e)
        {
            camera.Stop();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Any() && textBox2.Text.Any() && textBox3.Text.Any() && textBox3.Text.Any() && textBox5.Text.Any() && textBox6.Text.Any() && textBox7.Text.Any())
            {
                try
                {
                    Employee employee = new Employee();
                    employee.fullName = textBox1.Text;
                    employee.email = textBox2.Text;
                    employee.username = textBox3.Text;
                    employee.password = textBox4.Text;
                    employee.phone = textBox5.Text;
                    employee.address = textBox6.Text;
                    employee.salary = Convert.ToInt32(textBox7.Text);
                    Employee.Create(employee);

                    MessageBox.Show("Employee Added");
                    this.Hide();
                }catch(Exception ex)
                {
                    MessageBox.Show("Please Fill All Required Fields" + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please Fill All Required Fields");
            }
        }

        private void AddEmployee_Load(object sender, EventArgs e)
        {
            camera.OnFrameArrived += Camera_OnFrameArrived;
            camera.ChangeCamera(0);
            camera.Start(2);
            button2.Hide();
        }
        

        private void Camera_OnFrameArrived(object source, FrameArrivedEventArgs e)
        {
            Image image = e.GetFrame();
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox1.Image = image;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Any() && textBox2.Text.Any() && textBox3.Text.Any() && textBox3.Text.Any() && textBox5.Text.Any() && textBox6.Text.Any() && textBox7.Text.Any())
            {
                button2.Show();
                camera.Capture(@"C:\Users\Sisay\source\repos\OFFICE-R\OFFICE-R\Resources\static\img\propic\" + textBox1.Text);
                camera.Stop();
            }
            else
            {
                MessageBox.Show("Please Fill All Required Fields");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            camera.Start(2);
        }
    }
}
