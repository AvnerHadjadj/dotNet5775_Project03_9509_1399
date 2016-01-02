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
    public partial class EditFaultForm : Form
    {
        IBL bl;
        Fault faultToEdit;

        public EditFaultForm(IBL bl, int faultId)
        {
            this.bl = bl;
            this.faultToEdit = bl.SelectFault(faultId);

            InitializeComponent();

            textBox1.Text = faultToEdit.Description;
            textBox2.Text = faultToEdit.RepairCost.ToString();
            textBox3.Text = faultToEdit.PreferredGarage;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                faultToEdit.Description = textBox1.Text;
                faultToEdit.RepairCost = Convert.ToDouble(textBox2.Text);
                faultToEdit.PreferredGarage = textBox3.Text;

                bool result = bl.UpdateFault(faultToEdit);

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
