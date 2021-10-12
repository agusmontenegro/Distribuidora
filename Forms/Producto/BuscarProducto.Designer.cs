
namespace Distribuidora.Forms.Producto
{
    partial class BuscarProducto
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
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.cboRubros = new System.Windows.Forms.ComboBox();
            this.lblRubro = new System.Windows.Forms.Label();
            this.txtDetalleProducto = new System.Windows.Forms.TextBox();
            this.lblDetalleProducto = new System.Windows.Forms.Label();
            this.txtCodigoProducto = new System.Windows.Forms.TextBox();
            this.lblCodigoProducto = new System.Windows.Forms.Label();
            this.grdResult = new System.Windows.Forms.DataGridView();
            this.CodigoProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DetalleProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PrecioUnitario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Rubro = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StockActual = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FechaUltimaReposicion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnNuevoProducto = new System.Windows.Forms.Button();
            this.btnEliminarProducto = new System.Windows.Forms.Button();
            this.btnEditarProducto = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdResult)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnLimpiar);
            this.groupBox1.Controls.Add(this.btnBuscar);
            this.groupBox1.Controls.Add(this.cboRubros);
            this.groupBox1.Controls.Add(this.lblRubro);
            this.groupBox1.Controls.Add(this.txtDetalleProducto);
            this.groupBox1.Controls.Add(this.lblDetalleProducto);
            this.groupBox1.Controls.Add(this.txtCodigoProducto);
            this.groupBox1.Controls.Add(this.lblCodigoProducto);
            this.groupBox1.Location = new System.Drawing.Point(8, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(781, 113);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Criterio de búsqueda";
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Location = new System.Drawing.Point(710, 63);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(65, 34);
            this.btnLimpiar.TabIndex = 7;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(710, 24);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(65, 33);
            this.btnBuscar.TabIndex = 6;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // cboRubros
            // 
            this.cboRubros.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRubros.FormattingEnabled = true;
            this.cboRubros.Location = new System.Drawing.Point(9, 71);
            this.cboRubros.Name = "cboRubros";
            this.cboRubros.Size = new System.Drawing.Size(686, 21);
            this.cboRubros.TabIndex = 5;
            // 
            // lblRubro
            // 
            this.lblRubro.AutoSize = true;
            this.lblRubro.Location = new System.Drawing.Point(6, 55);
            this.lblRubro.Name = "lblRubro";
            this.lblRubro.Size = new System.Drawing.Size(36, 13);
            this.lblRubro.TabIndex = 4;
            this.lblRubro.Text = "Rubro";
            // 
            // txtDetalleProducto
            // 
            this.txtDetalleProducto.Location = new System.Drawing.Point(153, 32);
            this.txtDetalleProducto.Name = "txtDetalleProducto";
            this.txtDetalleProducto.Size = new System.Drawing.Size(542, 20);
            this.txtDetalleProducto.TabIndex = 3;
            this.txtDetalleProducto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDetalleProducto_KeyPress);
            // 
            // lblDetalleProducto
            // 
            this.lblDetalleProducto.AutoSize = true;
            this.lblDetalleProducto.Location = new System.Drawing.Point(150, 16);
            this.lblDetalleProducto.Name = "lblDetalleProducto";
            this.lblDetalleProducto.Size = new System.Drawing.Size(100, 13);
            this.lblDetalleProducto.TabIndex = 2;
            this.lblDetalleProducto.Text = "Detalle de producto";
            // 
            // txtCodigoProducto
            // 
            this.txtCodigoProducto.Location = new System.Drawing.Point(9, 32);
            this.txtCodigoProducto.Name = "txtCodigoProducto";
            this.txtCodigoProducto.Size = new System.Drawing.Size(138, 20);
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
            // grdResult
            // 
            this.grdResult.AllowUserToAddRows = false;
            this.grdResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdResult.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CodigoProducto,
            this.DetalleProducto,
            this.PrecioUnitario,
            this.Rubro,
            this.StockActual,
            this.FechaUltimaReposicion});
            this.grdResult.Location = new System.Drawing.Point(8, 126);
            this.grdResult.MultiSelect = false;
            this.grdResult.Name = "grdResult";
            this.grdResult.RowHeadersVisible = false;
            this.grdResult.Size = new System.Drawing.Size(781, 334);
            this.grdResult.TabIndex = 1;
            this.grdResult.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdResult_CellClick);
            // 
            // CodigoProducto
            // 
            this.CodigoProducto.HeaderText = "Código producto";
            this.CodigoProducto.Name = "CodigoProducto";
            this.CodigoProducto.Width = 80;
            // 
            // DetalleProducto
            // 
            this.DetalleProducto.HeaderText = "Detalle producto";
            this.DetalleProducto.Name = "DetalleProducto";
            this.DetalleProducto.Width = 250;
            // 
            // PrecioUnitario
            // 
            this.PrecioUnitario.HeaderText = "($) P/Unidad";
            this.PrecioUnitario.Name = "PrecioUnitario";
            this.PrecioUnitario.Width = 95;
            // 
            // Rubro
            // 
            this.Rubro.HeaderText = "Rubro";
            this.Rubro.Name = "Rubro";
            this.Rubro.Width = 152;
            // 
            // StockActual
            // 
            this.StockActual.HeaderText = "S. Actual";
            this.StockActual.Name = "StockActual";
            this.StockActual.Width = 75;
            // 
            // FechaUltimaReposicion
            // 
            this.FechaUltimaReposicion.HeaderText = "F. última reposición";
            this.FechaUltimaReposicion.Name = "FechaUltimaReposicion";
            this.FechaUltimaReposicion.Width = 125;
            // 
            // btnNuevoProducto
            // 
            this.btnNuevoProducto.Location = new System.Drawing.Point(8, 466);
            this.btnNuevoProducto.Name = "btnNuevoProducto";
            this.btnNuevoProducto.Size = new System.Drawing.Size(127, 59);
            this.btnNuevoProducto.TabIndex = 2;
            this.btnNuevoProducto.Text = "Nuevo producto";
            this.btnNuevoProducto.UseVisualStyleBackColor = true;
            this.btnNuevoProducto.Click += new System.EventHandler(this.btnNuevoProducto_Click);
            // 
            // btnEliminarProducto
            // 
            this.btnEliminarProducto.Location = new System.Drawing.Point(274, 466);
            this.btnEliminarProducto.Name = "btnEliminarProducto";
            this.btnEliminarProducto.Size = new System.Drawing.Size(127, 59);
            this.btnEliminarProducto.TabIndex = 3;
            this.btnEliminarProducto.Text = "Eliminar producto";
            this.btnEliminarProducto.UseVisualStyleBackColor = true;
            this.btnEliminarProducto.Click += new System.EventHandler(this.btnEliminarProducto_Click);
            // 
            // btnEditarProducto
            // 
            this.btnEditarProducto.Location = new System.Drawing.Point(141, 466);
            this.btnEditarProducto.Name = "btnEditarProducto";
            this.btnEditarProducto.Size = new System.Drawing.Size(127, 59);
            this.btnEditarProducto.TabIndex = 4;
            this.btnEditarProducto.Text = "Editar producto";
            this.btnEditarProducto.UseVisualStyleBackColor = true;
            this.btnEditarProducto.Click += new System.EventHandler(this.btnEditarProducto_Click);
            // 
            // BuscarProducto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(801, 537);
            this.Controls.Add(this.btnEditarProducto);
            this.Controls.Add(this.btnEliminarProducto);
            this.Controls.Add(this.btnNuevoProducto);
            this.Controls.Add(this.grdResult);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BuscarProducto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Producto";
            this.Load += new System.EventHandler(this.Producto_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdResult)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView grdResult;
        private System.Windows.Forms.Button btnNuevoProducto;
        private System.Windows.Forms.Button btnEliminarProducto;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.ComboBox cboRubros;
        private System.Windows.Forms.Label lblRubro;
        private System.Windows.Forms.TextBox txtDetalleProducto;
        private System.Windows.Forms.Label lblDetalleProducto;
        private System.Windows.Forms.TextBox txtCodigoProducto;
        private System.Windows.Forms.Label lblCodigoProducto;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodigoProducto;
        private System.Windows.Forms.DataGridViewTextBoxColumn DetalleProducto;
        private System.Windows.Forms.DataGridViewTextBoxColumn PrecioUnitario;
        private System.Windows.Forms.DataGridViewTextBoxColumn Rubro;
        private System.Windows.Forms.DataGridViewTextBoxColumn StockActual;
        private System.Windows.Forms.DataGridViewTextBoxColumn FechaUltimaReposicion;
        private System.Windows.Forms.Button btnEditarProducto;
    }
}