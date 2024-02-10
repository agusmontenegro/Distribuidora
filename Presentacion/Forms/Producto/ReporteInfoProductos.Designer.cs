
namespace Presentacion.Forms.Producto
{
    partial class ReporteInfoProductos
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
            this.rptInfoProductos = new Telerik.ReportViewer.WinForms.ReportViewer();
            this.SuspendLayout();
            // 
            // rptInfoProductos
            // 
            this.rptInfoProductos.AccessibilityKeyMap = null;
            this.rptInfoProductos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rptInfoProductos.Location = new System.Drawing.Point(0, 0);
            this.rptInfoProductos.Name = "rptInfoProductos";
            this.rptInfoProductos.Size = new System.Drawing.Size(1058, 661);
            this.rptInfoProductos.TabIndex = 0;
            // 
            // ReporteInfoProductos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1058, 661);
            this.Controls.Add(this.rptInfoProductos);
            this.Name = "ReporteInfoProductos";
            this.Text = "ReporteInfoProductos";
            this.Load += new System.EventHandler(this.ReporteInfoProductos_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.ReportViewer.WinForms.ReportViewer rptInfoProductos;
    }
}