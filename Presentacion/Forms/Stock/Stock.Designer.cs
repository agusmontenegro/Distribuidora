
namespace Presentacion.Forms.Stock
{
    partial class Stock
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblPtoReposicion = new System.Windows.Forms.Label();
            this.txtPtoReposicion = new System.Windows.Forms.TextBox();
            this.lblFechaUltimaReposicion = new System.Windows.Forms.Label();
            this.txtFechaUltimaReposicion = new System.Windows.Forms.TextBox();
            this.txtCantidadReponer = new System.Windows.Forms.TextBox();
            this.lblCantidadReponer = new System.Windows.Forms.Label();
            this.txtCantidadActual = new System.Windows.Forms.TextBox();
            this.lblCantidadActual = new System.Windows.Forms.Label();
            this.lblDetalleProducto = new System.Windows.Forms.Label();
            this.txtDetalleProducto = new System.Windows.Forms.TextBox();
            this.txtCodigoProducto = new System.Windows.Forms.TextBox();
            this.lblCodigoProducto = new System.Windows.Forms.Label();
            this.btnRealizarReposicionStock = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnConfirmar = new System.Windows.Forms.Button();
            this.grdStock = new System.Windows.Forms.DataGridView();
            this.CodigoProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DetalleProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StockActual = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StockAReponer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductoId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnEliminarItem = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdStock)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblPtoReposicion);
            this.groupBox1.Controls.Add(this.txtPtoReposicion);
            this.groupBox1.Controls.Add(this.lblFechaUltimaReposicion);
            this.groupBox1.Controls.Add(this.txtFechaUltimaReposicion);
            this.groupBox1.Controls.Add(this.txtCantidadReponer);
            this.groupBox1.Controls.Add(this.lblCantidadReponer);
            this.groupBox1.Controls.Add(this.txtCantidadActual);
            this.groupBox1.Controls.Add(this.lblCantidadActual);
            this.groupBox1.Controls.Add(this.lblDetalleProducto);
            this.groupBox1.Controls.Add(this.txtDetalleProducto);
            this.groupBox1.Controls.Add(this.txtCodigoProducto);
            this.groupBox1.Controls.Add(this.lblCodigoProducto);
            this.groupBox1.Location = new System.Drawing.Point(12, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(520, 102);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos de reposición";
            // 
            // lblPtoReposicion
            // 
            this.lblPtoReposicion.AutoSize = true;
            this.lblPtoReposicion.Location = new System.Drawing.Point(310, 55);
            this.lblPtoReposicion.Name = "lblPtoReposicion";
            this.lblPtoReposicion.Size = new System.Drawing.Size(82, 13);
            this.lblPtoReposicion.TabIndex = 11;
            this.lblPtoReposicion.Text = "Pto. Reposición";
            // 
            // txtPtoReposicion
            // 
            this.txtPtoReposicion.Location = new System.Drawing.Point(313, 71);
            this.txtPtoReposicion.Name = "txtPtoReposicion";
            this.txtPtoReposicion.Size = new System.Drawing.Size(98, 20);
            this.txtPtoReposicion.TabIndex = 10;
            // 
            // lblFechaUltimaReposicion
            // 
            this.lblFechaUltimaReposicion.AutoSize = true;
            this.lblFechaUltimaReposicion.Location = new System.Drawing.Point(158, 55);
            this.lblFechaUltimaReposicion.Name = "lblFechaUltimaReposicion";
            this.lblFechaUltimaReposicion.Size = new System.Drawing.Size(133, 13);
            this.lblFechaUltimaReposicion.TabIndex = 9;
            this.lblFechaUltimaReposicion.Text = "Fecha de última reposición";
            // 
            // txtFechaUltimaReposicion
            // 
            this.txtFechaUltimaReposicion.Location = new System.Drawing.Point(161, 71);
            this.txtFechaUltimaReposicion.Name = "txtFechaUltimaReposicion";
            this.txtFechaUltimaReposicion.Size = new System.Drawing.Size(146, 20);
            this.txtFechaUltimaReposicion.TabIndex = 8;
            // 
            // txtCantidadReponer
            // 
            this.txtCantidadReponer.Location = new System.Drawing.Point(417, 71);
            this.txtCantidadReponer.Name = "txtCantidadReponer";
            this.txtCantidadReponer.Size = new System.Drawing.Size(97, 20);
            this.txtCantidadReponer.TabIndex = 7;
            this.txtCantidadReponer.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCantidadReponer_KeyPress);
            // 
            // lblCantidadReponer
            // 
            this.lblCantidadReponer.AutoSize = true;
            this.lblCantidadReponer.Location = new System.Drawing.Point(414, 55);
            this.lblCantidadReponer.Name = "lblCantidadReponer";
            this.lblCantidadReponer.Size = new System.Drawing.Size(97, 13);
            this.lblCantidadReponer.TabIndex = 6;
            this.lblCantidadReponer.Text = "Cantidad a reponer";
            // 
            // txtCantidadActual
            // 
            this.txtCantidadActual.Location = new System.Drawing.Point(9, 71);
            this.txtCantidadActual.Name = "txtCantidadActual";
            this.txtCantidadActual.Size = new System.Drawing.Size(146, 20);
            this.txtCantidadActual.TabIndex = 5;
            // 
            // lblCantidadActual
            // 
            this.lblCantidadActual.AutoSize = true;
            this.lblCantidadActual.Location = new System.Drawing.Point(6, 55);
            this.lblCantidadActual.Name = "lblCantidadActual";
            this.lblCantidadActual.Size = new System.Drawing.Size(125, 13);
            this.lblCantidadActual.TabIndex = 4;
            this.lblCantidadActual.Text = "Cantidad de stock actual";
            // 
            // lblDetalleProducto
            // 
            this.lblDetalleProducto.AutoSize = true;
            this.lblDetalleProducto.Location = new System.Drawing.Point(158, 16);
            this.lblDetalleProducto.Name = "lblDetalleProducto";
            this.lblDetalleProducto.Size = new System.Drawing.Size(100, 13);
            this.lblDetalleProducto.TabIndex = 3;
            this.lblDetalleProducto.Text = "Detalle de producto";
            // 
            // txtDetalleProducto
            // 
            this.txtDetalleProducto.Location = new System.Drawing.Point(161, 32);
            this.txtDetalleProducto.Name = "txtDetalleProducto";
            this.txtDetalleProducto.Size = new System.Drawing.Size(353, 20);
            this.txtDetalleProducto.TabIndex = 2;
            // 
            // txtCodigoProducto
            // 
            this.txtCodigoProducto.Location = new System.Drawing.Point(9, 32);
            this.txtCodigoProducto.Name = "txtCodigoProducto";
            this.txtCodigoProducto.Size = new System.Drawing.Size(146, 20);
            this.txtCodigoProducto.TabIndex = 1;
            this.txtCodigoProducto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodigoProducto_KeyPress);
            // 
            // lblCodigoProducto
            // 
            this.lblCodigoProducto.AutoSize = true;
            this.lblCodigoProducto.Location = new System.Drawing.Point(6, 16);
            this.lblCodigoProducto.Name = "lblCodigoProducto";
            this.lblCodigoProducto.Size = new System.Drawing.Size(100, 13);
            this.lblCodigoProducto.TabIndex = 0;
            this.lblCodigoProducto.Text = "Código de producto";
            // 
            // btnRealizarReposicionStock
            // 
            this.btnRealizarReposicionStock.Location = new System.Drawing.Point(12, 566);
            this.btnRealizarReposicionStock.Name = "btnRealizarReposicionStock";
            this.btnRealizarReposicionStock.Size = new System.Drawing.Size(208, 60);
            this.btnRealizarReposicionStock.TabIndex = 1;
            this.btnRealizarReposicionStock.Text = "Realizar reposición de STOCK";
            this.btnRealizarReposicionStock.UseVisualStyleBackColor = true;
            this.btnRealizarReposicionStock.Click += new System.EventHandler(this.btnRealizarReposicionStock_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(538, 12);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(84, 44);
            this.btnCancelar.TabIndex = 2;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnConfirmar
            // 
            this.btnConfirmar.Location = new System.Drawing.Point(538, 68);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(84, 44);
            this.btnConfirmar.TabIndex = 3;
            this.btnConfirmar.Text = "Confirmar";
            this.btnConfirmar.UseVisualStyleBackColor = true;
            this.btnConfirmar.Click += new System.EventHandler(this.btnConfirmar_Click);
            // 
            // grdStock
            // 
            this.grdStock.AllowUserToAddRows = false;
            this.grdStock.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdStock.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CodigoProducto,
            this.DetalleProducto,
            this.StockActual,
            this.StockAReponer,
            this.ProductoId});
            this.grdStock.Location = new System.Drawing.Point(12, 124);
            this.grdStock.MultiSelect = false;
            this.grdStock.Name = "grdStock";
            this.grdStock.RowHeadersVisible = false;
            this.grdStock.Size = new System.Drawing.Size(574, 436);
            this.grdStock.TabIndex = 4;
            this.grdStock.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdStock_CellClick);
            // 
            // CodigoProducto
            // 
            this.CodigoProducto.HeaderText = "Cod. producto";
            this.CodigoProducto.Name = "CodigoProducto";
            this.CodigoProducto.ReadOnly = true;
            this.CodigoProducto.Width = 97;
            // 
            // DetalleProducto
            // 
            this.DetalleProducto.HeaderText = "Detalle de producto";
            this.DetalleProducto.Name = "DetalleProducto";
            this.DetalleProducto.ReadOnly = true;
            this.DetalleProducto.Width = 260;
            // 
            // StockActual
            // 
            this.StockActual.HeaderText = "Stock actual";
            this.StockActual.Name = "StockActual";
            this.StockActual.ReadOnly = true;
            this.StockActual.Width = 103;
            // 
            // StockAReponer
            // 
            this.StockAReponer.HeaderText = "Stock a reponer";
            this.StockAReponer.Name = "StockAReponer";
            this.StockAReponer.ReadOnly = true;
            this.StockAReponer.Width = 110;
            // 
            // ProductoId
            // 
            this.ProductoId.HeaderText = "ProductoId";
            this.ProductoId.Name = "ProductoId";
            this.ProductoId.Visible = false;
            // 
            // btnEliminarItem
            // 
            this.btnEliminarItem.Location = new System.Drawing.Point(592, 124);
            this.btnEliminarItem.Name = "btnEliminarItem";
            this.btnEliminarItem.Size = new System.Drawing.Size(30, 28);
            this.btnEliminarItem.TabIndex = 5;
            this.btnEliminarItem.UseVisualStyleBackColor = true;
            this.btnEliminarItem.Click += new System.EventHandler(this.btnEliminarItem_Click);
            // 
            // Stock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 638);
            this.Controls.Add(this.btnEliminarItem);
            this.Controls.Add(this.grdStock);
            this.Controls.Add(this.btnConfirmar);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnRealizarReposicionStock);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Stock";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Stock";
            this.Load += new System.EventHandler(this.Stock_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdStock)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblFechaUltimaReposicion;
        private System.Windows.Forms.TextBox txtFechaUltimaReposicion;
        private System.Windows.Forms.TextBox txtCantidadReponer;
        private System.Windows.Forms.Label lblCantidadReponer;
        private System.Windows.Forms.TextBox txtCantidadActual;
        private System.Windows.Forms.Label lblCantidadActual;
        private System.Windows.Forms.Label lblDetalleProducto;
        private System.Windows.Forms.TextBox txtDetalleProducto;
        private System.Windows.Forms.TextBox txtCodigoProducto;
        private System.Windows.Forms.Label lblCodigoProducto;
        private System.Windows.Forms.Button btnRealizarReposicionStock;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnConfirmar;
        private System.Windows.Forms.DataGridView grdStock;
        private System.Windows.Forms.Button btnEliminarItem;
        private System.Windows.Forms.Label lblPtoReposicion;
        private System.Windows.Forms.TextBox txtPtoReposicion;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodigoProducto;
        private System.Windows.Forms.DataGridViewTextBoxColumn DetalleProducto;
        private System.Windows.Forms.DataGridViewTextBoxColumn StockActual;
        private System.Windows.Forms.DataGridViewTextBoxColumn StockAReponer;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductoId;
    }
}