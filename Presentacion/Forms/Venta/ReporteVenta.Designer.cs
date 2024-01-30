
namespace Presentacion.Forms.Venta
{
    partial class ReporteVenta
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
            this.rptVenta = new Telerik.ReportViewer.WinForms.ReportViewer();
            this.SuspendLayout();
            // 
            // rptVenta
            // 
            this.rptVenta.AccessibilityKeyMap = null;
            this.rptVenta.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rptVenta.Location = new System.Drawing.Point(0, 0);
            this.rptVenta.Name = "rptVenta";
            this.rptVenta.Size = new System.Drawing.Size(800, 450);
            this.rptVenta.TabIndex = 0;
            // 
            // ReporteVenta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.rptVenta);
            this.Name = "ReporteVenta";
            this.Text = "ReporteVenta";
            this.Load += new System.EventHandler(this.ReporteVenta_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.ReportViewer.WinForms.ReportViewer rptVenta;
    }
}