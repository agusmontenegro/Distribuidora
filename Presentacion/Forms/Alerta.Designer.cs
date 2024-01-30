
namespace Presentacion.Forms
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
            this.Codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Detalle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Objeto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TipoAlerta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grdAlertas)).BeginInit();
            this.SuspendLayout();
            // 
            // grdAlertas
            // 
            this.grdAlertas.AllowUserToAddRows = false;
            this.grdAlertas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdAlertas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Codigo,
            this.Detalle,
            this.Fecha,
            this.Objeto,
            this.TipoAlerta});
            this.grdAlertas.Location = new System.Drawing.Point(10, 10);
            this.grdAlertas.MultiSelect = false;
            this.grdAlertas.Name = "grdAlertas";
            this.grdAlertas.RowHeadersVisible = false;
            this.grdAlertas.Size = new System.Drawing.Size(895, 429);
            this.grdAlertas.TabIndex = 0;
            this.grdAlertas.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdAlertas_CellClick);
            this.grdAlertas.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdAlertas_CellDoubleClick);
            // 
            // Codigo
            // 
            this.Codigo.DataPropertyName = "Codigo";
            this.Codigo.HeaderText = "Codigo";
            this.Codigo.Name = "Codigo";
            this.Codigo.ReadOnly = true;
            // 
            // Detalle
            // 
            this.Detalle.DataPropertyName = "Detalle";
            this.Detalle.HeaderText = "Detalle";
            this.Detalle.Name = "Detalle";
            this.Detalle.ReadOnly = true;
            this.Detalle.Width = 577;
            // 
            // Fecha
            // 
            this.Fecha.DataPropertyName = "Fecha";
            this.Fecha.HeaderText = "Fecha";
            this.Fecha.Name = "Fecha";
            this.Fecha.ReadOnly = true;
            this.Fecha.Width = 202;
            // 
            // Objeto
            // 
            this.Objeto.DataPropertyName = "Objeto";
            this.Objeto.HeaderText = "Objeto";
            this.Objeto.Name = "Objeto";
            this.Objeto.Visible = false;
            // 
            // TipoAlerta
            // 
            this.TipoAlerta.DataPropertyName = "TipoAlerta";
            this.TipoAlerta.HeaderText = "TipoAlerta";
            this.TipoAlerta.Name = "TipoAlerta";
            this.TipoAlerta.Visible = false;
            // 
            // Alerta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(917, 450);
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
        private System.Windows.Forms.DataGridViewTextBoxColumn Codigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Detalle;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn Objeto;
        private System.Windows.Forms.DataGridViewTextBoxColumn TipoAlerta;
    }
}