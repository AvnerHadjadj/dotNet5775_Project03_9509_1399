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
    public partial class InsertCarFaultForm : Form
    {
        BL.IBL bl;

        public InsertCarFaultForm(BL.IBL bl)
        {
            this.bl = bl;
            InitializeComponent();

            this.idNumber.Text = "Your number ID is n°" + (bl.SelectAllCar_Faults().Count + 1);
            comboBox1.DataSource = bl.SelectAllCars().Select(c => c.Name.ToString()).ToList();
            comboBox2.DataSource = bl.SelectAllFaults().Select(f => f.Description).ToList();
        }

        // THESE FONCTIONS RETURNS THE ID OF THE SELECTED ITEM THE COMBOBOX ASSICATED
        private int carId()
        {          
            string name = (string)(comboBox1.SelectedValue);
            return bl.SelectAllCars().Where(c => c.Name.ToString().Equals(name)).FirstOrDefault().IdNumber;
        }
        private int faultId()
        {
            return bl.SelectAllFaults().Where(c => c.Description.Equals(comboBox2.SelectedValue)).FirstOrDefault().IdNumber;
        }

        
        private void button1_Click(object sender, EventArgs e)
        {
            if (bl.InsertCar_Fault(new Car_Fault(carId(), faultId(), dateTimePicker1.Value)))
            {
                this.Close();
                MessageBox.Show("The car has been added correctly.",
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
