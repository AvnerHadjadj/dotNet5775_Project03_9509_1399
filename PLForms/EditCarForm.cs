using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BL;
using BE;

namespace PLForms
{
    public partial class EditCarForm : Form
    {
        IBL bl;
        Car carToEdit;

        public EditCarForm(IBL bl, int carId)
        {
            this.bl = bl;
            this.carToEdit = bl.SelectCar(carId);

            InitializeComponent();
            comboBox4.DataSource = Enum.GetValues(typeof(Transmission));
            comboBox5.DataSource = Enum.GetValues(typeof(Category));

            textBox1.Text = carToEdit.RegistrationNumber;
            dateTimePicker1.Value = carToEdit.ManufactureDate;            
            numericUpDown1.Value = carToEdit.Kilometers;
            textBox3.Text = carToEdit.BranchAddress;            
            textBox4.Text = carToEdit.Name.mark;
            textBox5.Text = carToEdit.Name.model;
            textBox6.Text = carToEdit.Name.volumecc;
            textBox7.Text = carToEdit.Name.color;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            try
            {
                carToEdit.RegistrationNumber = textBox1.Text;
                carToEdit.ManufactureDate = dateTimePicker1.Value;
                carToEdit.PassengersNumber =  Convert.ToInt32(comboBox2.SelectedValue);
                carToEdit.DoorsNumber = Convert.ToInt32(comboBox3.SelectedValue);
                carToEdit.Kilometers = (int)(numericUpDown1.Value);
                carToEdit.BranchAddress = textBox3.Text;
                carToEdit.TransmissionType = (Transmission)(comboBox4.SelectedValue);
                carToEdit.Cat = (Category)(comboBox5.SelectedValue);
                carToEdit.Name = new CarType(textBox4.Text, textBox5.Text, textBox6.Text, textBox7.Text);

                bool result = bl.UpdateCar(carToEdit);

                if (result)
                {
                    this.Close();
                    MessageBox.Show("The modifications has been saved.",
                                    "Success",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Sorry. The modifications was not saved. Please try again.",
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sorry. The modifications was not saved. Please try again.\n\nMore details:\n" + ex.Message);
            }
        }
    }
}
