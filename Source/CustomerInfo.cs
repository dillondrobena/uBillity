/****************************************************************************************************************
 * Dillon Drobena
 * 
 * 
 * CustomerInfo class - Class to provide all customer information
 *					  - Displays every field of customer information that had been provided
 *					  - Option to edit most fields and save back to the database
 *					  - Can delete customer or generate bills
 *					  - Bill generation creates a new instance of the BillControl.cs class with one instance of the Bill.cs class
 *                
 * Changes on development methodology: Not originally discussed in detail when designing the UI. Implied class to display
 *									   customer information. In the design specifications we described having capabilities
 *									   to edit, delete, and generate bills for each customer. This class solves those issues.
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
    public partial class CustomerInfo : Form
    {
        List<string> saveCustomerInfo = new List<string>();
        Customer customer;
        public CustomerInfo(Customer cust)
        {
            InitializeComponent();
            customer = cust;
            this.Text = customer.getFirstName() + " " + customer.getLastName();
            //Hard code initialize text blocks
            firstNameBox.Text = customer.getFirstName();
            lastNameBox.Text = customer.getLastName();
            ssNumBox.Text = customer.getSocialSecurity();
            contTypeBox.Text = customer.getContractType();
            contLenBox.Text = customer.getContractLength();
            bStreetBox.Text = customer.getBillStreet();
            bCityBox.Text = customer.getBillCity();
            bStateBox.Text = customer.getBillState();
            bZipBox.Text = customer.getBillZip();
            sStreetBox.Text = customer.getServiceStreet();
            sCityBox.Text = customer.getServiceCity();
            sStateBox.Text = customer.getServiceState();
            sZipBox.Text = customer.getServiceZip();
            phoneNumBox.Text = customer.getPhoneNumber();
            emailBox.Text = customer.getEmail();
            paperBox.Text = customer.getBillType();
            billRate.Text = customer.getBillRate().ToString();
            curMeter.Text = customer.getCurrentMeterReading().ToString();
            moneyOwedBox.Text = customer.getMoneyOwed().ToString();
            accountNumberBox.Text = customer.getAccountNumber();
            //Set credit card info if available
            if (customer.getAutoBilling())
            {
                autoBillBox.Text = "Auto";
                ccName.Text = customer.getCardName();
                ccNum.Text = customer.getCardNumber();
                ccExp.Text = customer.getCardExpiration();
                ccSec.Text = customer.getCardSecurity();
                ccZip.Text = customer.getCardZip();
            }
            else autoBillBox.Text = "Manual";
            //Block changes to first name, last name, and social security number
            firstNameBox.Enabled = false;
            lastNameBox.Enabled = false;
            ssNumBox.Enabled = false;
            accountNumberBox.Enabled = false;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            //Boolean needed incase an error message box showed
            bool canProceed = true;
            saveCustomerInfo.Clear(); //Clear the list for each customer
            foreach (Control currentControl in customerScreen.Controls) // Iterate through all controls
            {
                //Checks if they changed information to autobilling, and didn't include credit card info
                if (currentControl is TextBox && currentControl.Name.ToString().Contains("cc") && currentControl.Text.ToString() == "" && autoBillBox.Text.ToString() == "Auto")
                {
                    MessageBox.Show("You changed billing to automatic, please include credit card information.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    canProceed = false;
                    break;
                }
                else if (currentControl is TextBox && currentControl.Text.ToString() != "" && currentControl.Name != "accountNumberBox" && currentControl.Name != "moneyOwedBox") // Make sure text isn't empty
                {
                    TextBox temp = currentControl as TextBox;
                    saveCustomerInfo.Insert(0, temp.Text.ToString()); //Push to front of the list since order is reversed
                    canProceed = true;
                }
                else if (currentControl is ComboBox && currentControl.Text.ToString() != "") //Same for combo boxes
                {
                    ComboBox temp = currentControl as ComboBox;
                    saveCustomerInfo.Insert(0, temp.Text.ToString());
                    canProceed = true;
                }
            }
            //Save specific information that was added later, it would take too much time to reorganize the data, so we're appending it to the end
            saveCustomerInfo.Add(customer.getPastMeterReading().ToString());
            saveCustomerInfo.Add(moneyOwedBox.Text);
            saveCustomerInfo.Add(accountNumberBox.Text);
            saveCustomerInfo.Add(customer.getPreviousBalance().ToString());

            saveCustomerInfo.TrimExcess(); //Trim extra spaces
            //Save customer
            if (canProceed)
            {
                uBillity.updateCustomer(saveCustomerInfo);
                this.Close();
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            uBillity.deleteCustomer(customer.getSocialSecurity());
            this.Close();
        }

        private void billButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Generating a bill will credit a customer account until they pay.\nCurrent meter readings will also be archived, are you sure you want to continue? ", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                BillControl bill = new BillControl(customer); // Generates a bill for that customer
                bill.ShowDialog(); // Shows bill, and no other operations can commence while it's open.
                // Update money owed;
                moneyOwedBox.Text = customer.getMoneyOwed().ToString();
            } 
        }
    }
}
