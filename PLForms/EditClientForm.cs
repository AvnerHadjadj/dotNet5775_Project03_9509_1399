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
    public partial class EditClientForm : Form
    {
        IBL bl;
        Client clientToEdit;

        public EditClientForm(IBL bl, int clientId)
        {
            this.bl = bl;
            this.clientToEdit = bl.SelectClient(clientId);

            InitializeComponent();

            textBox1.Text = clientToEdit.IdTeoudaZeout.ToString();
            textBox2.Text = clientToEdit.Name.ToString();
            dateTimePicker1.Value = clientToEdit.BirthDate;
            textBox3.Text = clientToEdit.Address;
            dateTimePicker2.Value = clientToEdit.LicenseDrivingDate;
            textBox4.Text = clientToEdit.CreditCartNumber;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                clientToEdit.IdTeoudaZeout = int.Parse(textBox1.Text);
                clientToEdit.Name = textBox2.Text;
                clientToEdit.BirthDate = dateTimePicker1.Value;
                clientToEdit.Address = textBox3.Text;
                clientToEdit.LicenseDrivingDate = dateTimePicker2.Value;
                clientToEdit.CreditCartNumber = textBox4.Text;

                bool result = bl.UpdateClient(clientToEdit);

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
