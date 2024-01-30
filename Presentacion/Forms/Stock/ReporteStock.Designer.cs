
namespace Presentacion.Forms.Stock
{
    partial class ReporteStock
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
            this.rptStock = new Telerik.ReportViewer.WinForms.ReportViewer();
            this.SuspendLayout();
            // 
            // rptStock
            // 
            this.rptStock.AccessibilityKeyMap = null;
            this.rptStock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rptStock.Location = new System.Drawing.Point(0, 0);
            this.rptStock.Name = "rptStock";
            this.rptStock.Size = new System.Drawing.Size(800, 450);
            this.rptStock.TabIndex = 0;
            // 
            // ReporteStock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.rptStock);
            this.Name = "ReporteStock";
            this.Text = "ReporteStock";
            this.Load += new System.EventHandler(this.ReporteStock_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.ReportViewer.WinForms.ReportViewer rptStock;
    }
}