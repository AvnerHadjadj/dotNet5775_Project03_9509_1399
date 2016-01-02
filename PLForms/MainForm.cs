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
    public partial class MainForm : Form
    {
        IBL bl;
        public MainForm()
        {
            InitializeComponent();
            bl = new BLClientAdapter(); // BLFactory.getBL();
            // RESIZING THE WINDOW

            this.Location = new Point(0, 0);
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;

            // displaying the renting list in the datagridview as the main list to manage
            var bindingList = new BindingList<Renting>(bl.SelectAllRentings());
            var source = new BindingSource(bindingList, null);
            dataGridViewList.DataSource = source;

            // filling the combobox of clients in the menu
            clientListComboBox.DataSource = bl.SelectAllClients().Select(c => c.Name).ToList();
        }

        // ----------------------------------
        // GENERAL FUNCTION
        // ----------------------------------

        /// <summary>
        /// Returns the object in the selected row
        /// </summary>
        /// <returns></returns>
        private object GetSelectedRow()
        {
            try
            {
                return dataGridViewList.CurrentRow.DataBoundItem;
            }
            catch
            {
                MessageBox.Show("error");
                return null;
            }
        }

        private bool TypeOfRowSelectedIs(Type t)
        {
            Type selectedRowType = GetSelectedRow().GetType();
            if ((selectedRowType == t))
                return true;
            else
            {
                MessageBox.Show("You have to select a " + t.Name.ToString() + " in the data grid view.\nYou selected a " + selectedRowType.Name.ToString() + ".\nYou should click on 'Refresh the list' button.");
                return false;
            }
        }

        // -----------------------------------------------------------------------------
        // FUNCTIONS FOR THE BUTTONS IN THE RIGHT TABS 
        // -----------------------------------------------------------------------------

        

        private void InsertRentingBtn_Click(object sender, EventArgs e)
        {
            //try
            //{
            Form f = new InsertRentingForm(bl);
            f.Show();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        private void InsertClientBtn_Click(object sender, EventArgs e)
        {
            try
            {
                Form f = new InsertClientForm(bl);
                f.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void InsertCarBtn_Click(object sender, EventArgs e)
        {
            Form f = new InsertCarForm(bl);
            f.Show();
        }

        private void InsertFltBtn_Click(object sender, EventArgs e)
        {
            Form f = new InsertFaultForm(bl);
            f.Show();
        }

        private void InsertCarFltBtn_Click(object sender, EventArgs e)
        {
            Form f = new InsertCarFaultForm(bl);
            f.Show();
        }

        private void EndingRentingBtn_Click(object sender, EventArgs e)
        {           
            if (dataGridViewList.SelectedRows.Count != 0)
            {
                if(TypeOfRowSelectedIs(typeof(Renting))) 
                {
                    int rentIdSelected = ((Renting)(GetSelectedRow())).IdNumber;
                                
                    if (bl.SelectRenting(rentIdSelected).RentalPriceDaily == 0)
                    {
                        Form endRentingF = new EndRentingForm(bl, rentIdSelected);
                        endRentingF.Show();   
                    }
                    else
                    {
                        MessageBox.Show("You can't end a renting that is already finished.",
                                    "Warning",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                    }
                }               
            }
            else
            {
                MessageBox.Show("You have to select a renting in the list before.",
                                "Warning",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var bindingList = new BindingList<Renting>(bl.SelectAllRentings());
            var source = new BindingSource(bindingList, null);
            dataGridViewList.DataSource = source;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var bindingList = new BindingList<Client>(bl.SelectAllClients());
            var source = new BindingSource(bindingList, null);
            dataGridViewList.DataSource = source;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            var bindingList = new BindingList<Car>(bl.SelectAllCars());
            var source = new BindingSource(bindingList, null);
            dataGridViewList.DataSource = source;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            var bindingList = new BindingList<Fault>(bl.SelectAllFaults());
            var source = new BindingSource(bindingList, null);
            dataGridViewList.DataSource = source;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            var bindingList = new BindingList<Car_Fault>(bl.SelectAllCar_Faults());
            var source = new BindingSource(bindingList, null);
            dataGridViewList.DataSource = source;
        }

        private void DeleteRentingBtn_Click(object sender, EventArgs e)
        {
            if (dataGridViewList.SelectedRows.Count != 0)
            {
                if (TypeOfRowSelectedIs(typeof(Client)))
                {
                    int rentIdSelected = ((Renting)(GetSelectedRow())).IdNumber;
                    if (bl.DeleteRenting(rentIdSelected))
                    {
                        MessageBox.Show("Renting deleted!",
                                        "Warning",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning);

                    }
                    else
                    {
                        MessageBox.Show("Error!",
                                        "Error",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("You have to select a renting in the list before.",
                                "Warning",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
            }
        }

        private void DeleteClientBtn_Click(object sender, EventArgs e)
        {
            if (dataGridViewList.SelectedRows.Count != 0)
            {
                if (TypeOfRowSelectedIs(typeof(Client)))
                {
                    int clientIdSelected = ((Client)(GetSelectedRow())).IdNumber; ;
                    if (bl.DeleteClient(clientIdSelected))
                    {
                        MessageBox.Show("Client deleted!",
                                        "Warning",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning);

                    }
                    else
                    {
                        MessageBox.Show("Error!",
                                        "Error",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("You have to select a client in the list before.",
                                "Warning",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
            }
        }

        private void DeleteCarBtn_Click(object sender, EventArgs e)
        {
            if (dataGridViewList.SelectedRows.Count != 0)
            {
                if (TypeOfRowSelectedIs(typeof(Car)))
                {
                    int carIdSelected = ((Car)(GetSelectedRow())).IdNumber;
                    if (bl.DeleteCar(carIdSelected))
                    {
                        MessageBox.Show("Car deleted!",
                                        "Warning",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning);

                    }
                    else
                    {
                        MessageBox.Show("Error!",
                                        "Error",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("You have to select a car in the list before.",
                                "Warning",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridViewList.SelectedRows.Count != 0)
            {
                if (TypeOfRowSelectedIs(typeof(Fault)))
                {
                    int faultIdSelected = ((Fault)(GetSelectedRow())).IdNumber;
                    if (bl.DeleteFault(faultIdSelected))
                    {
                        MessageBox.Show("Fault deleted!",
                                        "Warning",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning);

                    }
                    else
                    {
                        MessageBox.Show("Error!",
                                        "Error",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("You have to select a fault in the list before.",
                                "Warning",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (dataGridViewList.SelectedRows.Count != 0)
            {
                if (TypeOfRowSelectedIs(typeof(Car_Fault)))
                {
                    int carfaultIdSelected = ((Car_Fault)(GetSelectedRow())).IdNumber;
                    if (bl.DeleteCar_Fault(carfaultIdSelected))
                    {
                        MessageBox.Show("Car fault deleted!",
                                        "Warning",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning);

                    }
                    else
                    {
                        MessageBox.Show("Error!",
                                        "Error",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("You have to select a car fault in the list before.",
                                "Warning",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
            }
        }

        private void UpdateRentingBtn_Click(object sender, EventArgs e)
        {
            if (dataGridViewList.SelectedRows.Count != 0)
            {
                if (TypeOfRowSelectedIs(typeof(Renting)))
                {
                    int rentIdSelected = ((Renting)(GetSelectedRow())).IdNumber;

                    if (bl.SelectRenting(rentIdSelected).RentalPriceDaily == 0)
                    {
                        Form editRentingF = new EditRentingForm(bl, rentIdSelected);
                        editRentingF.Show();
                    }
                    else
                    {
                        MessageBox.Show("You can't edit a renting that is already finished.",
                                    "Warning",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                    }
                }
            }
            else
            {
                MessageBox.Show("You have to select a renting in the list before.",
                                "Warning",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
            }
        }

        private void UpdateClientBtn_Click(object sender, EventArgs e)
        {
            if (dataGridViewList.SelectedRows.Count != 0)
            {
                if (TypeOfRowSelectedIs(typeof(Client)))
                {
                    int clientIdSelected = ((Client)(GetSelectedRow())).IdNumber;
                    Form editClientF = new EditClientForm(bl, clientIdSelected);
                    editClientF.Show();
                }
            }
            else
            {
                MessageBox.Show("You have to select a client in the list before.",
                                "Warning",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
            }
        }

        private void UpdateCarBtn_Click(object sender, EventArgs e)
        {
            if (dataGridViewList.SelectedRows.Count != 0)
            {
                if (TypeOfRowSelectedIs(typeof(Car)))
                {
                    int carIdSelected = ((Car)(GetSelectedRow())).IdNumber;
                    Form editCarF = new EditCarForm(bl, carIdSelected);
                    editCarF.Show();
                }
            }
            else
            {
                MessageBox.Show("You have to select a car in the list before.",
                                "Warning",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridViewList.SelectedRows.Count != 0)
            {
                if (TypeOfRowSelectedIs(typeof(Fault)))
                {
                    int faultIdSelected = ((Fault)(GetSelectedRow())).IdNumber;
                    Form editFaultF = new EditFaultForm(bl, faultIdSelected);
                    editFaultF.Show();
                }
            }
            else
            {
                MessageBox.Show("You have to select a fault in the list before.",
                                "Warning",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (dataGridViewList.SelectedRows.Count != 0)
            {
                if (TypeOfRowSelectedIs(typeof(Car_Fault)))
                {
                    int carfaultIdSelected = ((Car_Fault)(GetSelectedRow())).IdNumber;
                    Form editCarfaultF = new EditCarFaultForm(bl, carfaultIdSelected);
                    editCarfaultF.Show();
                }
            }
            else
            {
                MessageBox.Show("You have to select a renting in the list before.",
                                "Warning",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
            }
        }

        private void FinalPriceBtn_Click(object sender, EventArgs e)
        {
            
            if (dataGridViewList.SelectedRows.Count != 0)
            {
                if (TypeOfRowSelectedIs(typeof(Renting)))
                {
                    int rentIdSelected = ((Renting)(GetSelectedRow())).IdNumber;

                    if (bl.SelectRenting(rentIdSelected).RentalPriceDaily != 0)
                    {
                        MessageBox.Show("The clients payed " +
                                        bl.FinalRentingPrice(rentIdSelected)
                                        + " for this renting.",
                                        "Final price:",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("The renting you selected isn't over yet.\n" +
                                        "You have to finish this renting before.",
                                        "Warning",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning);
                    }
                }
            }
            else
            {
                MessageBox.Show("You have to select a renting in the list before.",
                                "Warning",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
            }
        }

        private void FindRentingBtn_Click(object sender, EventArgs e)
        {

            int clientSelected = bl.SelectAllClients().Where(c=> c.Name == ((string)(clientListComboBox.SelectedItem))).FirstOrDefault().IdNumber;
            dataGridViewList.DataSource = bl.GetRentingForClient(clientSelected);
        }

        private void FilterLessWeekBtn_Click(object sender, EventArgs e)
        {
            dataGridViewList.DataSource = bl.FindRenting(r => (r.RentalEndDate - r.RentalStartDate) < TimeSpan.FromDays(7)); 
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            dataGridViewList.DataSource = bl.FindRenting(r => (r.RentalEndDate - r.RentalStartDate) > TimeSpan.FromDays(7)); 
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            dataGridViewList.DataSource = bl.FindRenting(r => bl.SelectCar(r.CarId).Cat == Category.A); 
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            dataGridViewList.DataSource = bl.FindRenting(r => bl.SelectCar(r.CarId).Cat == Category.B); 
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            dataGridViewList.DataSource = bl.FindRenting(r => bl.SelectCar(r.CarId).Cat == Category.C);
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            dataGridViewList.DataSource = bl.FindRenting(r => bl.SelectCar(r.CarId).Cat == Category.D); 
        }

        private void ExpensesForClientBtn_Click(object sender, EventArgs e)
        {
            if (dataGridViewList.SelectedRows.Count == 1)
            {
                if (TypeOfRowSelectedIs(typeof(Client)))
                {
                    int clientId = ((Client)(GetSelectedRow())).IdNumber;
                    double expenses = bl.ExpensesForClient(clientId, new DateTime(2014, 1, 1), new DateTime(2016, 1, 1));

                    MessageBox.Show(bl.SelectClient(clientId).Name + "'s expenses for all time: " + expenses + "nis.");
                }
            }
            else
            {
                MessageBox.Show("You have to select a renting in the list before.",
                               "Warning",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Warning);
            }
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            if (dataGridViewList.SelectedRows.Count == 1)
            {
                if (TypeOfRowSelectedIs(typeof(Client)))
                {
                    int clientId = ((Client)(GetSelectedRow())).IdNumber;
                    double expenses = bl.ExpensesForClient(clientId, DateTime.Now.AddDays(-7.0), DateTime.Now);

                    MessageBox.Show(bl.SelectClient(clientId).Name + "'s expenses for 7 last days: " + expenses + "nis.");
                }
            }
            else
            {
                MessageBox.Show("You have to select a renting in the list before.",
                               "Warning",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Warning);
            }
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            if (dataGridViewList.SelectedRows.Count == 1)
            {
                if (TypeOfRowSelectedIs(typeof(Client)))
                {
                    int clientId = ((Client)(GetSelectedRow())).IdNumber;
                    double expenses = bl.ExpensesForClient(clientId, DateTime.Now.AddDays(-30.0), DateTime.Now);

                    MessageBox.Show(bl.SelectClient(clientId).Name + "'s expenses for 30 last days: " + expenses + "nis.");

                }
            }
            else
            {
                MessageBox.Show("You have to select a renting in the list before.",
                               "Warning",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Warning);
            }
        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            if (dataGridViewList.SelectedRows.Count == 1)
            {
                if (TypeOfRowSelectedIs(typeof(Car)))
                {
                    int carId = ((Car)(GetSelectedRow())).IdNumber;
                    double earning = bl.EarningsForCar(carId);

                    MessageBox.Show("Earning from the " + bl.SelectCar(carId).Name + ": " + earning + "nis.");
                }
            }
            else
            {
                MessageBox.Show("You have to select a renting in the list before.",
                               "Warning",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Display faults by frequency
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            string str = "";
            foreach(string n in bl.FaultsNamesOrderByFrequency())
                str += n + '\n';

            MessageBox.Show("The faults that occur the more often are in the top of the list.\n" +
                            "You should handle them with a special treatment.\n\n" + str,
                            "Faults ordered by frequency",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                            
        }


    }
}
