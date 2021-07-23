
namespace Distribuidora.Forms
{
    partial class Alerta
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
            this.grdAlertas = new System.Windows.Forms.DataGridView();
            this.AlertaId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AlertaDetalle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AlertaTipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grdAlertas)).BeginInit();
            this.SuspendLayout();
            // 
            // grdAlertas
            // 
            this.grdAlertas.AllowUserToAddRows = false;
            this.grdAlertas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdAlertas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.AlertaId,
            this.AlertaDetalle,
            this.AlertaTipo});
            this.grdAlertas.Location = new System.Drawing.Point(10, 10);
            this.grdAlertas.MultiSelect = false;
            this.grdAlertas.Name = "grdAlertas";
            this.grdAlertas.RowHeadersVisible = false;
            this.grdAlertas.Size = new System.Drawing.Size(782, 429);
            this.grdAlertas.TabIndex = 0;
            this.grdAlertas.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdAlertas_CellClick);
            this.grdAlertas.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdAlertas_CellDoubleClick);
            // 
            // AlertaId
            // 
            this.AlertaId.HeaderText = "";
            this.AlertaId.Name = "AlertaId";
            this.AlertaId.ReadOnly = true;
            this.AlertaId.Visible = false;
            // 
            // AlertaDetalle
            // 
            this.AlertaDetalle.HeaderText = "Detalle";
            this.AlertaDetalle.Name = "AlertaDetalle";
            this.AlertaDetalle.ReadOnly = true;
            this.AlertaDetalle.Width = 577;
            // 
            // AlertaTipo
            // 
            this.AlertaTipo.HeaderText = "Tipo de alerta";
            this.AlertaTipo.Name = "AlertaTipo";
            this.AlertaTipo.ReadOnly = true;
            this.AlertaTipo.Width = 202;
            // 
            // Alerta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.grdAlertas);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Alerta";
            this.Text = "Alerta";
            this.Load += new System.EventHandler(this.Alerta_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdAlertas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView grdAlertas;
        private System.Windows.Forms.DataGridViewTextBoxColumn AlertaId;
        private System.Windows.Forms.DataGridViewTextBoxColumn AlertaDetalle;
        private System.Windows.Forms.DataGridViewTextBoxColumn AlertaTipo;
    }
}