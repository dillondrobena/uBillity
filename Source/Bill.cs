/****************************************************************************************************************
 * Dillon Drobena
 * 
 * 
 * Bill class - Serves as a template to create each bill.
 *			  - Contains all the information that will be displayed when generating a bill.
 *                
 * Changes on development methodology: A Bill class was never discussed in the original design, but would've fallen
 *									   between back-end logic, and UI design. The information provided by the bill
 *									   was chosen by our prototype bill that was included in the design document.
 *									   Several fields were left out for this current prototype.
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
    public partial class Bill : Form
    {
        double deliveryRateFee = .016;
        public Bill(Customer customer) //Initializes a customer bill WARNING: Very brute force, prepare for confusion...have to look at design doc's to understand which label is which
        {
            InitializeComponent();
            //Initialize the labels by brute force
            //Left Side address
            billName.Text = customer.getFirstName() + " " + customer.getLastName();
            billStreet.Text = customer.getBillStreet();
            billCity.Text = customer.getBillCity() + ", " + customer.getBillState() + " " + customer.getBillZip();
            //Right Side Address
            serviceName.Text = customer.getFirstName() + " " + customer.getLastName();
            serviceStreet.Text = customer.getServiceStreet();
            serviceCity.Text = customer.getServiceCity() + ", " + customer.getServiceState() + " " + customer.getServiceZip();
            //Top panel
            accountNum.Text = customer.getAccountNumber();
            billingDate.Text = DateTime.Today.ToShortDateString();
            nextBillingDate.Text = DateTime.Today.AddMonths(1).ToShortDateString();
            //Middle panel 1
            prevRead.Text = customer.getPastMeterReading().ToString();
            curDate.Text = billingDate.Text;
            curReading.Text = customer.getCurrentMeterReading().ToString();
            curRate.Text = customer.getBillRate().ToString();
            //Middle panel 2
                //First Half
            double moneyOwed = customer.getMoneyOwed();
            prevBalance.Text = "$" + customer.getPreviousBalance().ToString("0.##");
            payRec.Text = "$" + (customer.getPreviousBalance() - customer.getMoneyOwed()).ToString("0.##");
            balanceBefore.Text = "$" + moneyOwed.ToString("0.##"); //Get previous balance owed
                //Second Half
            //Random base fee per month
            double baseFee = 10.00;
            baseCharge.Text = "$" + baseFee.ToString("0.##");
            energyUsage.Text = customer.getCurrentMeterReading().ToString();
            energyRate.Text = customer.getBillRate().ToString();
            //Gets meter reading
            double meterReading = customer.getCurrentMeterReading();
            //Gets energy due by meter reading * 2 since getBillAmount() expects a different number than initial meter reading
            double energyChargeDue = customer.getBillAmount(meterReading);
            energyCharge.Text = "$" + energyChargeDue.ToString("0.##");
            deliveryUsage.Text = (meterReading).ToString();
            deliveryRate.Text = deliveryRateFee.ToString();
            //Gets delivery charge due by meter reading * delivery rate
            double deliveryChargeDue = meterReading * deliveryRateFee;
            deliveryCharge.Text = "$" + ((meterReading) * deliveryRateFee).ToString("0.##");
            //Calculate total charge, delivery charge + energy charge + base fee, or energychargedue + deliverychargedue
            double totalDue = energyChargeDue + deliveryChargeDue + baseFee + moneyOwed;
            totalCharge.Text = "$" + totalDue.ToString("0.##");
            // Bottom panel
            totalDate.Text = DateTime.Today.AddDays(15).ToShortDateString();
            paymentDate.Text = totalDate.Text;
            afterDate.Text = totalDate.Text;
            total.Text = "$" + totalDue.ToString("0.##");
            latePaymentAfter.Text = "$" + (totalDue * .10).ToString("0.##");
            totalAfter.Text = "$" + (totalDue += (totalDue * .10)).ToString("0.##");
            //Set meter reading for verification purposes since I think it gets changed in the process of reading the current amount due
            customer.setCurrentMeterReading(meterReading.ToString());
            customer.setPastMeterReading(customer.getCurrentMeterReading()); //Push meter reading back
            //Update money owed to include base fee and delivery fee
            customer.setMoneyOwed(10 + deliveryChargeDue);
            customer.setPreviousBalance(customer.getMoneyOwed());
            //Update customer
            uBillity.updateCustomer(customer);
        }
        public Panel getPanel() //Needed to bind the information provided by this page, to the tab control from the BillControl.cs class
        {
            return panel1;
        }
    }
}
