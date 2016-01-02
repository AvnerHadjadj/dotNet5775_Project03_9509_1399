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
    public partial class EndRentingForm : Form
    {
        IBL bl;
        Renting rentToEnd;
        public EndRentingForm(IBL bl, int rentId)
        {
            this.bl = bl;
            this.rentToEnd = bl.SelectRenting(rentId);

            InitializeComponent();

            // initializing the variables fields
            titleLabel.Text = "Ending the renting #" + rentToEnd.IdNumber;
            comboBox1.DataSource = bl.SelectAllFaults().Select(f => f.Description).ToList();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            // checking that end date is after start date
            if (dateTimePicker1.Value < rentToEnd.RentalStartDate)
            {
                MessageBox.Show("Ending date can't be past to the start date: " + rentToEnd.RentalStartDate.ToShortDateString());
            }
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            bool isChecked = (checkBox1.Checked == true);
                    
            // changing the text of the checkBox
            checkBox1.Text = isChecked ? "Yes" : "No";

            // changing status of the combobox of faults choice to add
            comboBox1.Enabled = isChecked ? true : false;
            button2.Enabled = isChecked ? true : false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                if (comboBox1.SelectedIndex != -1)
                {
                    if (bl.InsertCar_Fault(new Car_Fault(rentToEnd.CarId, comboBox1.SelectedIndex + 1, rentToEnd.RentalStartDate.AddHours(1))))
                    {
                        MessageBox.Show("The car fault has been saved.\nChoose another fault if needed.",
                                        "Success",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Error: The car fault was not saved.",
                                        "Error",
                                         MessageBoxButtons.OK,
                                         MessageBoxIcon.Error);
                    }
                }
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button2.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form newFaultForm = new InsertFaultForm(this.bl);
            newFaultForm.Show();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (bl.RentingEnding(rentToEnd.IdNumber, dateTimePicker1.Value, (int)numericUpDown1.Value))
            {
                this.Close();
                MessageBox.Show("The renting #" + rentToEnd.IdNumber + "ended correctly for the price of " + bl.FinalRentingPrice(rentToEnd.IdNumber) + " NIS.",
                                "Success",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Sorry! Something went wrong !",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }
    }
}
