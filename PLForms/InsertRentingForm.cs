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
using BL;

namespace PLForms
{
    public partial class InsertRentingForm : Form
    {
        IBL bl;
        public InsertRentingForm(IBL bl)
        {
            this.bl = bl;
            InitializeComponent();


            this.IdRent.Text = "Renting n°" + (bl.SelectAllRentings().Count + 1);
            comboBox1.DataSource = bl.SelectAllCars().Select(c => c.Name).ToList();
            comboBox2.DataSource = bl.SelectAllClients().Select(c => c.Name).ToList();

            comboBox3.Enabled = false;
            dateTimePicker1.Value = DateTime.Now;            
        }

        // THESE FONCTIONS RETURNS THE ID OF THE SELECTED ITEM THE COMBOBOX ASSICATED
        private int firstDriver()
        {
            return bl.SelectAllClients().Where(c => c.Name.Equals(comboBox2.SelectedValue)).FirstOrDefault().IdNumber;
        }
        private int secondDriver()
        {
            return (checkBox1.Checked == true) ? bl.SelectAllClients().Where(c => c.Name.Equals(comboBox3.SelectedValue)).FirstOrDefault().IdNumber : 0;
        }
        private int carId()
        {
            return bl.SelectAllCars().Where(c => c.Name.Equals(((BE.CarType)(comboBox1.SelectedValue)))).FirstOrDefault().IdNumber;
        }

        // THESE FUNCTIONS HANDLES THE MESSAGEBOX ERRORS
        private bool firstDriverEqualsSecondDriver()
        {
            if (firstDriver() == secondDriver())
            {
                MessageBox.Show("You can't select twice the same driver !",
                                "Warning",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return true;
            }
            else return false;               
        }
        private bool dateIsPast()
        {
            if (dateTimePicker1.Value < new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 0, 0, 0))
            {
                MessageBox.Show("Date invalid because it is in the past",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return true;
            }
            else return false;
        }

        /// <summary>
        /// Updates the summary of the renting 
        /// </summary>
        public void SummaryUpdate()
        {
            // summmary update
            string summary = "";
            summary += "=== Summary of the new renting ===\n\n";
            summary += "Car selected: " + comboBox1.SelectedValue + "\n";
            summary += "Rental starts on: " + dateTimePicker1.Value.ToShortDateString() + "\n";
            summary += "First Driver: " + comboBox2.SelectedValue + "\n";
            summary += (checkBox1.Checked == true) ? "Second Driver: " + comboBox3.SelectedValue + "\n" : "";
            summary += "Daily cost: " + (int)(bl.SelectCar(carId())).Cat + " NIS" + "\n";
            summary += (checkBox1.Checked == true) ? "Tax for 2nd driver: 50NIS" + "\n" : "";
            summary += ((comboBox2.SelectedIndex != -1) && bl.IsYoungDriver(firstDriver())) ? "Tax young driver 1: 100NIS" + "\n" : "";
            summary += ((checkBox1.Checked == true) && bl.IsYoungDriver(secondDriver())) ? "Tax young driver 2: 100NIS" + "\n" : "";
            summary += ((comboBox2.SelectedIndex != -1) && bl.IsNewDriver(firstDriver())) ? "Tax new driver 1: 100NIS" + "\n" : "";
            summary += ((checkBox1.Checked == true) && bl.IsNewDriver(secondDriver())) ? "Tax new driver 2: 100NIS" + "\n" : "";
             
            SummaryRenting.Text = summary;            
        }

       
               

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {                       
            carDescription.Text = bl.SelectCar(carId()).ToString();

            SummaryUpdate();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value < new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 0, 0, 0))
            {
                MessageBox.Show("Date invalid because it is the past",
                                "Warning",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
            }
            else
            {
                SummaryUpdate();
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!firstDriverEqualsSecondDriver())
                SummaryUpdate();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!firstDriverEqualsSecondDriver())
                SummaryUpdate();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {            
            comboBox3.Enabled = checkBox1.Checked;

            // filling the combobox of second driver
            comboBox3.DataSource = bl.SelectAllClients().Select(c => c.Name).ToList(); //.Select(c => c.Name);
        }

        private void FinishRenting_Click(object sender, EventArgs e)
        {   
            // checking the fields
            if(!dateIsPast() && !firstDriverEqualsSecondDriver())                
            {
                SummaryUpdate();

                // creating the renting
                BE.Renting newRent = new Renting(dateTimePicker1.Value, carId(), bl.SelectCar(carId()).Kilometers, firstDriver(), secondDriver());

                try
                {
                    bool result = bl.InsertRenting(newRent);

                    if (result)
                    {
                        this.Close();
                        MessageBox.Show("The new renting has been inserted. \n\nYour Renting has the number" + bl.SelectAllRentings().Count + ".",
                                        "Success", 
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
                    }
                    else
                        MessageBox.Show("Sorry. The new renting wasn't inserted. Please try again.",
                                        "Error", 
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
