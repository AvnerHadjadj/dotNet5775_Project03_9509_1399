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
    public partial class InsertFaultForm : Form
    {
        BL.IBL bl;

        public InsertFaultForm(BL.IBL bl)
        {
            this.bl = bl;
            InitializeComponent();

            this.idNumber.Text = "Your number ID is n°" + (Fault.autoIncrement + 1);
        }

        private void idNumber_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (bl.InsertFault(new Fault(textBox1.Text, checkBox1.Checked, (int)numericUpDown1.Value, textBox3.Text)))
            {
                this.Close();
                MessageBox.Show("The fault has been added correctly.",
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
