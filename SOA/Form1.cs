using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SOA.ServiceReference1;
namespace SOA
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Car c = new Car();
            c.Id = Convert.ToInt32(textBox1.Text);
            c.Car_Type = textBox2.Text;
            c.Car_Name = textBox3.Text;
            c.Driver = textBox4.Text;
            c.Car_Number = textBox5.Text;

            ServiceClient service = new ServiceClient();

            if(service.InsertCar(c) == 1)
            {
                MessageBox.Show("Car Details Inserted Successfully");
            }
            else
            {
                MessageBox.Show("CarId already taken");


            }
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();



        }

        private void button2_Click(object sender, EventArgs e)
        {
            Car c = new Car()
            {
                Id = Convert.ToInt32(textBox1.Text),
                Car_Type = textBox2.Text,
                Car_Name = textBox3.Text,
                Driver = textBox4.Text,
                Car_Number = textBox5.Text

            };

            ServiceClient service = new ServiceClient();
            if(service.UpdateCar(c) == 1)
            {
                MessageBox.Show("Car details updated successfully");
            }

            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();


        }

        private void button3_Click(object sender, EventArgs e)
        {
            Car c = new Car()
            {
                Id = Convert.ToInt32(textBox1.Text)
               

            };

            ServiceClient service = new ServiceClient();
            if (service.DeleteCar(c) == 1)
            {
                MessageBox.Show("Car details deleted successfully");
            }

            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            List<Car> carD = new List<Car>();
            Car g = new Car();
            Car c = new Car()
            {
                Id = Convert.ToInt32(textBox1.Text)

            };
            ServiceClient service = new ServiceClient();
            carD.Add(service.GetCar(c));
            dataGridView1.DataSource = carD;
            textBox3.Text = dataGridView1.Rows[0].Cells[0].Value.ToString();
            textBox5.Text = dataGridView1.Rows[0].Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.Rows[0].Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.Rows[0].Cells[3].Value.ToString();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            List<Car> carD = new List<Car>();
            ServiceClient service = new ServiceClient();

            dataGridView1.DataSource = service.GetAllCars();
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
        }
    }
}
