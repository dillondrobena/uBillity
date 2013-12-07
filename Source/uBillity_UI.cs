
/****************************************************************************************************************
 * Dillon Drobena
 * 
 * 
 * uBillity_UI class - First screen that is created during the start of the program
 *					 - Contains menu strip, addCustomerScreen, searchCustomerSCreen, and home screen
 *					 - Handles transitions between each tab and new screen
 *					 - Add customer screen will pass a list<string> to the uBillity.cs class to initialize a new customer
 *					 - Search customer will send a request to pull information about a specific customer
 *					 - Generate All Bills will pull all customers and create new instances of BillControl.cs and Bill.cs
 *                
 * Changes on development methodology: Differing from our specifications, we decided to go with a windows form 
 *									   application instead of the ASP.Net web page. Also, during design we weren't sure
 *									   how many UI classes we would end up using, we now have 5 (or 10 if you include the
 *									   the design forms)
 * 
 *****************************************************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace uBillity_Prototype
{
    public partial class uBillity_UI : Form
    {
        List<string> addCustomerArray = new List<string>(); // List to store all text boxes information from addCustomerScreen
        List<string> searchCustomerArray = new List<string>(); //List to store all text boxes information from searchCustomerScreen
        public uBillity_UI()
        {
            InitializeComponent();
        }
        // Called when user pressed the Add Customer Button
        private void addCustomerToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //Set other screens to false
            addCustomerScreen.Visible = true;
            searchCustomerScreen.Visible = false;
            //Set home toolbar item to visible, search customer toolbar item to visible, add customer toolbar item to invisible
            homeToolStripMenuItem.Visible = true;
            addCustomerToolStripMenuItem.Visible = false;
            searchCustomerToolStripMenuItem.Visible = true;
            //Adjust window size
            this.ClientSize = new System.Drawing.Size(740, 538);
            date.Text = "As of " + DateTime.Today.ToShortDateString().ToString();
            clearSearchResults(); //Clear results from searchCustomerScreen if any are present
        }
        // Called when user presses Home button
        private void homeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clearSearchResults(); //Clear results from searchCustomerScreen if any are present
            if (verifyScreenValidity())
            {
                //Set other screens to false
                addCustomerScreen.Visible = false;
                //Set home toolbar item to invisible, add customer toolbar item to visible, search customer toolbar item to visible
                addCustomerToolStripMenuItem.Visible = true;
                searchCustomerToolStripMenuItem.Visible = true;
                homeToolStripMenuItem.Visible = false;
                //Adjust window size
                this.ClientSize = new System.Drawing.Size(520, 399);
            }
        }
        // Called when user pressed Add Customer from within Add Customer Screen

        /* Contains 4 stages of parsing information, so it gets slightly hectic and confusing
         * 1st parsing stage goes through the list and makes sure that if autobillingbox is set to auto, and that ANY credit card information
         * is left blank, it throws an error message and breaks the loop
         * 2nd parsing stage goes through the list and makes sure that all textboxes and comboboxes that do not belong to credit card information are not null
         * otherwise it too throws an error message and breaks the loop
         * 3rd parsing stage adds all textbox strings and combo box strings to the addCustomerArray list
         * 4th parsing stage adds all credit card information to the loop, ONLY if the autobillbox is set to Auto, otherwise it will ignore the request
         * This is to make sure the list is ONLY either 19 or 24 elements long EXACTLY
         * Final persing stage makes sure that the list is ONLY 19 or 24 elements long, meaning that the addCustomerArray list is a successfull and appropriate customer
         * and it will than send that list off to be added to the database, and clear all fields so the user can add another new customer
         */
        private void addCustomerButton_Click(object sender, EventArgs e)
        {
            addCustomerArray.Clear(); //Clear the list for each customer
            foreach (Control currentControl in addCustomerScreen.Controls) // Iterate through all controls
            {
                // 1st PARSE
                // Test if they put AUTO for billing option, and that they have credit card information filled out
                if (currentControl is TextBox && currentControl.Name.Contains("cc") && currentControl.Text.ToString() == "" && autoBillBox.Text.ToString() == "Auto")
                {
                    MessageBox.Show("You indicated Auto-Billing, please fill in all credit card boxes", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }
                // 2nd PARSE
                // Kinda hardcoded...but makes sure all text boxes and combo boxes have values in them, ignores credit card information since it's optional
                if ((currentControl is TextBox || currentControl is ComboBox) && currentControl.Text.ToString() == "" && (!(currentControl.Name.Contains("cc"))))
                {
                    MessageBox.Show("Make sure all appropriate boxes are filled.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }
                // 3rd PARSE
                else if (currentControl is TextBox && (!(currentControl.Name.Contains("cc"))))
                {
                    TextBox temp = currentControl as TextBox;
                    addCustomerArray.Insert(0, temp.Text.ToString()); //Push to front of the list since order is reversed
                }
                else if (currentControl is ComboBox) //Same for combo boxes
                {
                    ComboBox temp = currentControl as ComboBox;
                    addCustomerArray.Insert(0, temp.Text.ToString());
                }
                // 4th PARSE
                // Add credit card information only if it passed the initial tests and autobillbox is set to Auto
                else if (currentControl is TextBox && currentControl.Name.Contains("cc") && autoBillBox.Text.ToString() == "Auto")
                {
                    TextBox temp = currentControl as TextBox;
                    addCustomerArray.Insert(0, temp.Text.ToString());
                }
            }
            addCustomerArray.TrimExcess(); //Trim extra spaces
            //FINAL PARSE
            //Verify length is either 19 or 24, and clear the results
            if (addCustomerArray.Count == 19 && autoBillBox.Text.ToString() == "Manual")
            {
                clearCustomerResults();
                uBillity.addCustomer(addCustomerArray);
            }
            else if (addCustomerArray.Count == 24 && autoBillBox.Text.ToString() == "Auto")
            {
                clearCustomerResults();
                uBillity.addCustomer(addCustomerArray);
            }
        }
        // Called when user pressed Search Customer button
        private void searchCustomerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (verifyScreenValidity())
            {
                //Set other screens to false
                searchCustomerScreen.Visible = true;
                addCustomerScreen.Visible = true;
                //Set home toolbar item to visible, add customer toolbar item to visible, search customer toolbar item to invisible
                searchCustomerToolStripMenuItem.Visible = false;
                addCustomerToolStripMenuItem.Visible = true;
                homeToolStripMenuItem.Visible = true;
                //Adjust window size
                this.ClientSize = new System.Drawing.Size(520, 270);
                //Initializes the dynamic combo box
                initSearchComboBox();
            }
        }
        private void initSearchComboBox()
        {
            //Reset combo box
            customerList.Items.Clear();
            //Create dictionary to store all customers
            Dictionary<int, Customer> dict = uBillity.getCustomers();
            foreach (KeyValuePair<int, Customer> entry in dict)
            {
                customerList.Items.Add(entry.Value.getFirstName() + " " + entry.Value.getLastName());
            }
        }
        // Called during each screen transition to make sure user was not in the middle of entering new customer data, verify's their choice if they were.
        private bool verifyScreenValidity()
        {
            foreach (Control ctr in addCustomerScreen.Controls)
            {
                if ((ctr is TextBox || ctr is ComboBox) && ctr.Text.ToString() != "")
                {
                    if (MessageBox.Show("You began entering customer information, are you sure you want to quit?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        clearCustomerResults();
                        return true;
                    }
                    else return false;
                }
            }
            return true;
        }
        // Clears all data from within Add Customer screen
        private void clearCustomerResults()
        {
            // Clear add customer screen
            foreach (Control ctr in addCustomerScreen.Controls)
            {
                if (ctr is TextBox)
                {
                    ctr.Text = "";
                }
                else if (ctr is ComboBox)
                {
                    ctr.Text = "";
                }
            }
        }
        private void clearSearchResults()
        {
            // Clear search customer screen
            foreach (Control ctr in searchCustomerScreen.Controls)
            {
                if (ctr is TextBox)
                {
                    ctr.Text = "";
                }
                else if (ctr is ComboBox)
                {
                    ctr.Text = "";
                }
            }
        }
        // Called when user presses Search from within the Search Customer screen
        private void searchCustomerButton_Click(object sender, EventArgs e)
        {
            int count = 0; //Simple incrementer to verify 3 boxes contain data
            searchCustomerArray.Clear(); //Clear all data within the list
            if (customerList.Text == "")
            {
                foreach (Control currentControl in searchCustomerScreen.Controls)
                {
                    // Verify all boxes are filled
                    if (currentControl is TextBox && currentControl.Text.ToString() == "" && customerList.Text == "")
                    {
                        MessageBox.Show("Make sure all appropriate boxes are filled.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    }
                    else if (currentControl is TextBox)
                    {
                        TextBox temp = currentControl as TextBox;
                        searchCustomerArray.Insert(0, temp.Text.ToString()); //Append to beginning of list, rather than use a stack
                        count++;
                    }
                }
                //If all three boxes contain data
                if (count == 3)
                {
                    Customer customer = null; //Create new NULL customer
                    customer = uBillity.findCustBySSN(searchSS.Text.ToString());
                    if (customer == null)
                    {
                        MessageBox.Show("Based on your information, a matching customer could not be found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    //One or more boxes might not contain data
                    else
                    {
                        CustomerInfo customerInfo = new CustomerInfo(customer); // Creates a new customer form
                        customerInfo.ShowDialog(); // Displays the customer form in a way that it must be exited before any other operations can commence
                        //Clear search results for next search
                        clearSearchResults();
                    }
                }
            }
            //Tests if they user selected a customer from the drop-down menu
            else if (customerList.Text.ToString() != "")
            {
                //Split the result by a delimiter
                string[] result = customerList.Text.ToString().Split();
                //Find the customer
                Customer customer = uBillity.findCustByName(result[0], result[1]);
                //Show that customer's info
                CustomerInfo customerInfo = new CustomerInfo(customer);
                customerInfo.ShowDialog();
                //My sneaky trick to make sure not to search by any other criteria
                count--;
                //Clear search results for next search
                clearSearchResults();
            }
            //Refresh combo box after possible deletion
            initSearchComboBox();
        }
        // Called when user pressed the Generate All Bills button
        private void generateAllBillsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Generating a bill will credit a customer account until they pay.\nCurrent meter readings will also be archived, are you sure you want to continue? ", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (uBillity.getCustomers().Count != 0)
                {
                    BillControl bill = new BillControl(uBillity.getCustomers());
                    bill.ShowDialog();
                }
                else
                {
                    MessageBox.Show("There are no customers to generate bills for.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }
    }
}
