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
    public partial class EditCarFaultForm : Form
    {
        IBL bl;
        Car_Fault carfaultToEdit;

        public EditCarFaultForm(IBL bl, int carfaultId)
        {
            this.bl = bl;
            this.carfaultToEdit = bl.SelectCar_Fault(carfaultId);

            InitializeComponent();
            comboBox2.DataSource = bl.SelectAllCars().Select(c => c.IdNumber).ToList();
            comboBox3.DataSource = bl.SelectAllFaults().Select(c => c.IdNumber).ToList();
           
            dateTimePicker1.Value = carfaultToEdit.FaultDate;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                carfaultToEdit.CarId = Convert.ToInt32(comboBox2.SelectedValue);
                carfaultToEdit.FaultId = Convert.ToInt32(comboBox3.SelectedValue);
                carfaultToEdit.FaultDate = dateTimePicker1.Value;

                bool result = bl.UpdateCar_Fault(carfaultToEdit);

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
