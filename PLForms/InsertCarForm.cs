using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BE;

namespace PLForms
{
    public partial class InsertCarForm : Form
    {
        BL.IBL bl;

        public InsertCarForm(BL.IBL bl)
        {
            this.bl = bl;
            InitializeComponent();

            this.idNumber.Text = "Your number ID is n°" + (Car.autoIncrement + 1);
            comboBox1.DataSource = Enum.GetValues(typeof(Transmission));
            comboBox2.DataSource = Enum.GetValues(typeof(Category));
                
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool result =   
                bl.InsertCar(
                new Car(
                    textBox5.Text, 
                    dateTimePicker1.Value, 
                    new CarType(
                        Mark: textBox1.Text, 
                        Model: textBox2.Text, 
                        Volumecc:textBox3.Text, 
                        Color: textBox4.Text),
                    (Transmission)comboBox1.SelectedValue,
                    Convert.ToInt32(comboBox4.SelectedItem),
                    Convert.ToInt32(comboBox3.SelectedItem), 
                    (int)numericUpDown1.Value, 
                    textBox7.Text,
                    (Category)comboBox2.SelectedValue
                    )
                );
            if (result)
            {
                this.Close();
                MessageBox.Show("The car n°" + Car.autoIncrement + "has been added correctly.",
                                "Success",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }
    }
}
