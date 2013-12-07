/****************************************************************************************************************
 * Dillon Drobena
 * 
 * 
 * Progress class - Added this as an extra feature because generating bills for 100 test clients took a long period of time
 *					so this class simply provides a progress bar to indicate when all bills had been generated
 *                
 * Changes on development methodology: Was never discussed in the design document, added for simplicity and time. Mostly asthetic changes.
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
    public partial class ProgressClass : Form
    {
        public ProgressClass()
        {
            InitializeComponent();
        }
        public void init(int maximum)
        {
            progressBar1.Maximum = maximum;
            progressBar1.Value = 0;
        }
        public void update(int value)
        {
            progressBar1.Increment(value);
        }
    }
}
