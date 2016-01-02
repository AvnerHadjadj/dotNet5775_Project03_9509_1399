using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PLForms
{
    public partial class InsertClientForm : Form
    {
        BL.IBL bl;

        public InsertClientForm(BL.IBL bl)
        {
            this.bl = bl;
            InitializeComponent();

            this.idNumber.Text = "Your number ID is n°" + (BE.Client.autoIncrement + 1);
        }

        private void idNumber_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length < 7 ||
                textBox2.Text.Length < 3 ||
                textBox3.Text.Length < 10 ||
                textBox4.Text.Length < 12 ||
                dateTimePicker1.Enabled == false ||
                dateTimePicker2.Enabled == false)
            {
                MessageBox.Show("Error !! \nYou do not enter all the information on the right way. \n\nTry Again.",
                                "Error",
                                 MessageBoxButtons.OK,
                                 MessageBoxIcon.Error);
            }

            else
            {
                BE.Client newClient = new BE.Client(int.Parse(textBox1.Text), textBox3.Text, textBox2.Text, dateTimePicker1.Value, dateTimePicker2.Value, textBox4.Text);

                try
                {
                    bool result = bl.InsertClient(newClient);

                    if (result)
                    {
                        this.Close();
                        MessageBox.Show("The new client has been inserted. \n\nHis number ID is the number" + BE.Client.autoIncrement + ".",
                                        "Success",
                                         MessageBoxButtons.OK,
                                         MessageBoxIcon.Information);
                    }
                    else
                        MessageBox.Show("Sorry. The new client wasn't inserted. Please try again.",
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
