/****************************************************************************************************************
 * Dillon Drobena
 * 
 * 
 * BillControl class - Contains the tab control information and printing specifications for bills.
 *					 - Retrieves the initial panel from the Bill class and creates a new Tab based on that data.
 *					 - Includes several different printing options for the user that will capture the current screen.
 *                
 * Changes on development methodology: Along with the Bill.cs class, this was not originally intended to be in the design
 *									   until we realized there needed to be a way to organize and traverse a large amount
 *									   of bills without filling up the user's screen with forms. Several resizing issues
 *									   also occurred, so the sample bills included with this program may not look exactly
 *									   as they appear within the program due to screen resolution conflicts.
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
    public partial class BillControl : Form
    {
        public BillControl(Dictionary<int, Customer> customerList) //Constructor to generate ALL bills
        {
            ProgressClass progress = new ProgressClass();
            progress.Show();
            //Used to keep track of current customer
            int count = 0;
            InitializeComponent();
            progress.init(customerList.Count);
            // Iteratre through array, take name to set as Tab name, create new Bill using information from array
            foreach (KeyValuePair<int, Customer> entry in customerList) //Iterate through all the customers
            {
                billTab.TabPages.Add(entry.Value.getFirstName() + " " + entry.Value.getLastName()); //Add tab and set the tab's name
                billTab.TabPages[count].Controls.Add(new Bill(entry.Value).getPanel()); //Needed to bind information to this page
                count++;
                progress.update(1);
            }
            progress.Close();
        }   
        public BillControl(Customer customer) //Constructor to generate ONE bill
        {
            InitializeComponent();
            billTab.TabPages.Add(customer.getFirstName() + " " + customer.getLastName()); //Add tab and set the tab's name
            billTab.TabPages[0].Controls.Add(new Bill(customer).getPanel()); //Bind panel information to tab control
        }

        Bitmap memoryImage; //Bitmap to capture screen area, will be used twice so it needs to be global
        private void captureScreen() // Gets the current tab's screeen area
        {
            memoryImage = new Bitmap(billTab.SelectedTab.Width, billTab.SelectedTab.Height); 
            billTab.SelectedTab.DrawToBitmap(memoryImage, new Rectangle(0, 0, billTab.SelectedTab.Width, billTab.SelectedTab.Height));
        }
   /*     protected override void OnPaint(PaintEventArgs e) //Error catching for null image, overriding OnPaint listener
        {
            if (memoryImage != null)
            {
                e.Graphics.DrawImage(memoryImage, 0, 0);
                base.OnPaint(e);
            }
        }*/
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e) //Indicates the print function for printdocument object
        {
            captureScreen();
            Rectangle pageArea = e.PageBounds; //Set page printing area
            e.Graphics.DrawImage(memoryImage, (pageArea.Width / 2) - ((billTab.SelectedTab.Width / 2) - 60), billTab.Location.Y); //Draw image to page, can't seem to set it perfectly centered
        }
       
        private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e) //Sets up the print preview
        {
            captureScreen();
            printPreviewDialog1.Document = printDocument1; //Point preview to printdocument
            ((Form)printPreviewDialog1).WindowState = FormWindowState.Maximized; //Maximize print preview
            printPreviewDialog1.ShowDialog();
        }

        private void selectPrinterToolStripMenuItem_Click(object sender, EventArgs e) //Selects printer view printdialog box
        {
            printDialog1.Document = printDocument1;
            DialogResult result = printDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printDocument1.Print();
        }

        private void printDocument2_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            billTab.SelectedTab = billTab.TabPages[count]; //Set tab
            captureScreen(); //Create bitmap image
            Rectangle pageArea = e.PageBounds; //Set page printing area
            e.Graphics.DrawImage(memoryImage, (pageArea.Width / 2) - ((billTab.SelectedTab.Width / 2) - 60), 50); //Draw image to page
            if (count < numOfTabs - 1) //Check if there are more tabs
            {
                e.HasMorePages = true;
                count++;
            }
            else e.HasMorePages = false; //No more tabs, end printing
        }

        int numOfTabs; //Necessary global variable to keep track of how many tabs need to be printed
        int count; //Needed to indicate which tab we are currently on
        private void printAllPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Initialize values
            numOfTabs = 0;
            count = 0;
            foreach (TabPage page in billTab.TabPages) //Count tabs
            {
                numOfTabs++;
            }
            printPreviewDialog1.Document = printDocument2;
            ((Form)printPreviewDialog1).WindowState = FormWindowState.Maximized; //Maximize print preview
            printPreviewDialog1.ShowDialog();
        }

        private void printAllToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            printDocument2.Print();
        }
    }
}
