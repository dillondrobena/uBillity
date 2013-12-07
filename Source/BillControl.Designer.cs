/****************************************************************************************************************
 * Dillon Drobena
 * 
 * 
 * BillControl.Designer - Auto generated forms to provide UI functionality to the BillControl class
 * 
 *****************************************************************************************************************/
namespace uBillity_Prototype
{
    partial class BillControl
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BillControl));
            this.printStrip = new System.Windows.Forms.MenuStrip();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printPreviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectPrinterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printAllToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.billTab = new System.Windows.Forms.TabControl();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.printDocument2 = new System.Drawing.Printing.PrintDocument();
            this.printStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // printStrip
            // 
            this.printStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.testToolStripMenuItem});
            this.printStrip.Location = new System.Drawing.Point(0, 0);
            this.printStrip.Name = "printStrip";
            this.printStrip.Size = new System.Drawing.Size(673, 28);
            this.printStrip.TabIndex = 0;
            this.printStrip.Text = "menuStrip1";
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.printPreviewToolStripMenuItem,
            this.selectPrinterToolStripMenuItem,
            this.printToolStripMenuItem,
            this.printAllToolStripMenuItem,
            this.printAllToolStripMenuItem1});
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(51, 24);
            this.testToolStripMenuItem.Text = "Print";
            // 
            // printPreviewToolStripMenuItem
            // 
            this.printPreviewToolStripMenuItem.Name = "printPreviewToolStripMenuItem";
            this.printPreviewToolStripMenuItem.Size = new System.Drawing.Size(185, 24);
            this.printPreviewToolStripMenuItem.Text = "Print Preview";
            this.printPreviewToolStripMenuItem.Click += new System.EventHandler(this.printPreviewToolStripMenuItem_Click);
            // 
            // selectPrinterToolStripMenuItem
            // 
            this.selectPrinterToolStripMenuItem.Name = "selectPrinterToolStripMenuItem";
            this.selectPrinterToolStripMenuItem.Size = new System.Drawing.Size(185, 24);
            this.selectPrinterToolStripMenuItem.Text = "Select Printer";
            this.selectPrinterToolStripMenuItem.Click += new System.EventHandler(this.selectPrinterToolStripMenuItem_Click);
            // 
            // printToolStripMenuItem
            // 
            this.printToolStripMenuItem.Name = "printToolStripMenuItem";
            this.printToolStripMenuItem.Size = new System.Drawing.Size(185, 24);
            this.printToolStripMenuItem.Text = "Print";
            this.printToolStripMenuItem.Click += new System.EventHandler(this.printToolStripMenuItem_Click);
            // 
            // printAllToolStripMenuItem
            // 
            this.printAllToolStripMenuItem.Name = "printAllToolStripMenuItem";
            this.printAllToolStripMenuItem.Size = new System.Drawing.Size(185, 24);
            this.printAllToolStripMenuItem.Text = "Print All Preview";
            this.printAllToolStripMenuItem.Click += new System.EventHandler(this.printAllPreviewToolStripMenuItem_Click);
            // 
            // printAllToolStripMenuItem1
            // 
            this.printAllToolStripMenuItem1.Name = "printAllToolStripMenuItem1";
            this.printAllToolStripMenuItem1.Size = new System.Drawing.Size(185, 24);
            this.printAllToolStripMenuItem1.Text = "Print All";
            this.printAllToolStripMenuItem1.Click += new System.EventHandler(this.printAllToolStripMenuItem1_Click);
            // 
            // billTab
            // 
            this.billTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.billTab.Location = new System.Drawing.Point(0, 28);
            this.billTab.Name = "billTab";
            this.billTab.SelectedIndex = 0;
            this.billTab.Size = new System.Drawing.Size(673, 605);
            this.billTab.TabIndex = 1;
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Document = this.printDocument1;
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // printDocument2
            // 
            this.printDocument2.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument2_PrintPage);
            // 
            // BillControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(673, 633);
            this.Controls.Add(this.billTab);
            this.Controls.Add(this.printStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "BillControl";
            this.printStrip.ResumeLayout(false);
            this.printStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip printStrip;
        private System.Windows.Forms.TabControl billTab;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem printPreviewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectPrinterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem printToolStripMenuItem;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Windows.Forms.ToolStripMenuItem printAllToolStripMenuItem;
        private System.Drawing.Printing.PrintDocument printDocument2;
        private System.Windows.Forms.ToolStripMenuItem printAllToolStripMenuItem1;

    }
}