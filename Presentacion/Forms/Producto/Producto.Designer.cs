
namespace Presentacion.Forms.Producto
{
    partial class Producto
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
            this.lblProductCode = new System.Windows.Forms.Label();
            this.txtCodigoProducto = new System.Windows.Forms.TextBox();
            this.cboRubros = new System.Windows.Forms.ComboBox();
            this.lblRubro = new System.Windows.Forms.Label();
            this.lblStockReposicion = new System.Windows.Forms.Label();
            this.txtStockMinimo = new System.Windows.Forms.TextBox();
            this.txtPrecioUnitario = new System.Windows.Forms.TextBox();
            this.lblPrecioUnitario = new System.Windows.Forms.Label();
            this.txtDetalleProducto = new System.Windows.Forms.TextBox();
            this.lblDetalleProducto = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.lblComponentes = new System.Windows.Forms.Label();
            this.txtDetalleProductoComposicion = new System.Windows.Forms.TextBox();
            this.lblDetalleProductoComposicion = new System.Windows.Forms.Label();
            this.grdComponentes = new System.Windows.Forms.DataGridView();
            this.lblCantidadComposicion = new System.Windows.Forms.Label();
            this.txtCantidadComposicion = new System.Windows.Forms.TextBox();
            this.btnAgregarComponente = new System.Windows.Forms.Button();
            this.lblCodigoProductoComposicion = new System.Windows.Forms.Label();
            this.txtCodigoProductoComposicion = new System.Windows.Forms.TextBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.CodigoProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DetalleProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdComponentes)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblProductCode);
            this.groupBox1.Controls.Add(this.txtCodigoProducto);
            this.groupBox1.Controls.Add(this.cboRubros);
            this.groupBox1.Controls.Add(this.lblRubro);
            this.groupBox1.Controls.Add(this.lblStockReposicion);
            this.groupBox1.Controls.Add(this.txtStockMinimo);
            this.groupBox1.Controls.Add(this.txtPrecioUnitario);
            this.groupBox1.Controls.Add(this.lblPrecioUnitario);
            this.groupBox1.Controls.Add(this.txtDetalleProducto);
            this.groupBox1.Controls.Add(this.lblDetalleProducto);
            this.groupBox1.Location = new System.Drawing.Point(8, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(525, 112);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos del producto";
            // 
            // lblProductCode
            // 
            this.lblProductCode.AutoSize = true;
            this.lblProductCode.Location = new System.Drawing.Point(6, 16);
            this.lblProductCode.Name = "lblProductCode";
            this.lblProductCode.Size = new System.Drawing.Size(100, 13);
            this.lblProductCode.TabIndex = 9;
            this.lblProductCode.Text = "Código de producto";
            // 
            // txtCodigoProducto
            // 
            this.txtCodigoProducto.Location = new System.Drawing.Point(9, 32);
            this.txtCodigoProducto.Name = "txtCodigoProducto";
            this.txtCodigoProducto.Size = new System.Drawing.Size(97, 20);
            this.txtCodigoProducto.TabIndex = 1;
            // 
            // cboRubros
            // 
            this.cboRubros.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRubros.FormattingEnabled = true;
            this.cboRubros.Location = new System.Drawing.Point(9, 76);
            this.cboRubros.Name = "cboRubros";
            this.cboRubros.Size = new System.Drawing.Size(397, 21);
            this.cboRubros.TabIndex = 4;
            // 
            // lblRubro
            // 
            this.lblRubro.AutoSize = true;
            this.lblRubro.Location = new System.Drawing.Point(6, 60);
            this.lblRubro.Name = "lblRubro";
            this.lblRubro.Size = new System.Drawing.Size(36, 13);
            this.lblRubro.TabIndex = 6;
            this.lblRubro.Text = "Rubro";
            // 
            // lblStockReposicion
            // 
            this.lblStockReposicion.AutoSize = true;
            this.lblStockReposicion.Location = new System.Drawing.Point(409, 60);
            this.lblStockReposicion.Name = "lblStockReposicion";
            this.lblStockReposicion.Size = new System.Drawing.Size(72, 13);
            this.lblStockReposicion.TabIndex = 5;
            this.lblStockReposicion.Text = "Stock mínimo";
            // 
            // txtStockMinimo
            // 
            this.txtStockMinimo.Location = new System.Drawing.Point(412, 76);
            this.txtStockMinimo.Name = "txtStockMinimo";
            this.txtStockMinimo.Size = new System.Drawing.Size(101, 20);
            this.txtStockMinimo.TabIndex = 5;
            this.txtStockMinimo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtStockMinimo_KeyPress);
            // 
            // txtPrecioUnitario
            // 
            this.txtPrecioUnitario.Location = new System.Drawing.Point(412, 32);
            this.txtPrecioUnitario.Name = "txtPrecioUnitario";
            this.txtPrecioUnitario.Size = new System.Drawing.Size(101, 20);
            this.txtPrecioUnitario.TabIndex = 3;
            this.txtPrecioUnitario.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPrecioUnitario_KeyPress);
            // 
            // lblPrecioUnitario
            // 
            this.lblPrecioUnitario.AutoSize = true;
            this.lblPrecioUnitario.Location = new System.Drawing.Point(409, 16);
            this.lblPrecioUnitario.Name = "lblPrecioUnitario";
            this.lblPrecioUnitario.Size = new System.Drawing.Size(74, 13);
            this.lblPrecioUnitario.TabIndex = 2;
            this.lblPrecioUnitario.Text = "Precio unitario";
            // 
            // txtDetalleProducto
            // 
            this.txtDetalleProducto.Location = new System.Drawing.Point(112, 32);
            this.txtDetalleProducto.Name = "txtDetalleProducto";
            this.txtDetalleProducto.Size = new System.Drawing.Size(294, 20);
            this.txtDetalleProducto.TabIndex = 2;
            // 
            // lblDetalleProducto
            // 
            this.lblDetalleProducto.AutoSize = true;
            this.lblDetalleProducto.Location = new System.Drawing.Point(109, 16);
            this.lblDetalleProducto.Name = "lblDetalleProducto";
            this.lblDetalleProducto.Size = new System.Drawing.Size(102, 13);
            this.lblDetalleProducto.TabIndex = 0;
            this.lblDetalleProducto.Text = "Detalle del producto";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnCancelar);
            this.groupBox2.Controls.Add(this.btnEliminar);
            this.groupBox2.Controls.Add(this.lblComponentes);
            this.groupBox2.Controls.Add(this.txtDetalleProductoComposicion);
            this.groupBox2.Controls.Add(this.lblDetalleProductoComposicion);
            this.groupBox2.Controls.Add(this.grdComponentes);
            this.groupBox2.Controls.Add(this.lblCantidadComposicion);
            this.groupBox2.Controls.Add(this.txtCantidadComposicion);
            this.groupBox2.Controls.Add(this.btnAgregarComponente);
            this.groupBox2.Controls.Add(this.lblCodigoProductoComposicion);
            this.groupBox2.Controls.Add(this.txtCodigoProductoComposicion);
            this.groupBox2.Location = new System.Drawing.Point(8, 130);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(525, 286);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Datos de composición";
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(412, 53);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(100, 20);
            this.btnCancelar.TabIndex = 12;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Location = new System.Drawing.Point(488, 79);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(24, 22);
            this.btnEliminar.TabIndex = 11;
            this.btnEliminar.Text = "X";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // lblComponentes
            // 
            this.lblComponentes.AutoSize = true;
            this.lblComponentes.Location = new System.Drawing.Point(6, 63);
            this.lblComponentes.Name = "lblComponentes";
            this.lblComponentes.Size = new System.Drawing.Size(75, 13);
            this.lblComponentes.TabIndex = 10;
            this.lblComponentes.Text = "Componentes:";
            // 
            // txtDetalleProductoComposicion
            // 
            this.txtDetalleProductoComposicion.Location = new System.Drawing.Point(112, 32);
            this.txtDetalleProductoComposicion.Name = "txtDetalleProductoComposicion";
            this.txtDetalleProductoComposicion.Size = new System.Drawing.Size(242, 20);
            this.txtDetalleProductoComposicion.TabIndex = 9;
            // 
            // lblDetalleProductoComposicion
            // 
            this.lblDetalleProductoComposicion.AutoSize = true;
            this.lblDetalleProductoComposicion.Location = new System.Drawing.Point(109, 16);
            this.lblDetalleProductoComposicion.Name = "lblDetalleProductoComposicion";
            this.lblDetalleProductoComposicion.Size = new System.Drawing.Size(102, 13);
            this.lblDetalleProductoComposicion.TabIndex = 8;
            this.lblDetalleProductoComposicion.Text = "Detalle del producto";
            // 
            // grdComponentes
            // 
            this.grdComponentes.AllowUserToAddRows = false;
            this.grdComponentes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdComponentes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CodigoProducto,
            this.DetalleProducto,
            this.Cantidad,
            this.IdProducto});
            this.grdComponentes.Location = new System.Drawing.Point(9, 79);
            this.grdComponentes.MultiSelect = false;
            this.grdComponentes.Name = "grdComponentes";
            this.grdComponentes.RowHeadersVisible = false;
            this.grdComponentes.Size = new System.Drawing.Size(472, 190);
            this.grdComponentes.TabIndex = 7;
            this.grdComponentes.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdComponentes_CellClick);
            // 
            // lblCantidadComposicion
            // 
            this.lblCantidadComposicion.AutoSize = true;
            this.lblCantidadComposicion.Location = new System.Drawing.Point(357, 17);
            this.lblCantidadComposicion.Name = "lblCantidadComposicion";
            this.lblCantidadComposicion.Size = new System.Drawing.Size(49, 13);
            this.lblCantidadComposicion.TabIndex = 6;
            this.lblCantidadComposicion.Text = "Cantidad";
            // 
            // txtCantidadComposicion
            // 
            this.txtCantidadComposicion.Location = new System.Drawing.Point(360, 32);
            this.txtCantidadComposicion.Name = "txtCantidadComposicion";
            this.txtCantidadComposicion.Size = new System.Drawing.Size(46, 20);
            this.txtCantidadComposicion.TabIndex = 5;
            this.txtCantidadComposicion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCantidadComposicion_KeyPress);
            // 
            // btnAgregarComponente
            // 
            this.btnAgregarComponente.Location = new System.Drawing.Point(412, 32);
            this.btnAgregarComponente.Name = "btnAgregarComponente";
            this.btnAgregarComponente.Size = new System.Drawing.Size(100, 20);
            this.btnAgregarComponente.TabIndex = 4;
            this.btnAgregarComponente.Text = "Confirmar";
            this.btnAgregarComponente.UseVisualStyleBackColor = true;
            this.btnAgregarComponente.Click += new System.EventHandler(this.btnAgregarComponente_Click);
            // 
            // lblCodigoProductoComposicion
            // 
            this.lblCodigoProductoComposicion.AutoSize = true;
            this.lblCodigoProductoComposicion.Location = new System.Drawing.Point(6, 16);
            this.lblCodigoProductoComposicion.Name = "lblCodigoProductoComposicion";
            this.lblCodigoProductoComposicion.Size = new System.Drawing.Size(100, 13);
            this.lblCodigoProductoComposicion.TabIndex = 3;
            this.lblCodigoProductoComposicion.Text = "Código de producto";
            // 
            // txtCodigoProductoComposicion
            // 
            this.txtCodigoProductoComposicion.Location = new System.Drawing.Point(9, 32);
            this.txtCodigoProductoComposicion.Name = "txtCodigoProductoComposicion";
            this.txtCodigoProductoComposicion.Size = new System.Drawing.Size(97, 20);
            this.txtCodigoProductoComposicion.TabIndex = 2;
            this.txtCodigoProductoComposicion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodigoProductoComposicion_KeyPress);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(8, 428);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(221, 58);
            this.btnGuardar.TabIndex = 2;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // CodigoProducto
            // 
            this.CodigoProducto.HeaderText = "Cod. producto";
            this.CodigoProducto.Name = "CodigoProducto";
            this.CodigoProducto.ReadOnly = true;
            // 
            // DetalleProducto
            // 
            this.DetalleProducto.HeaderText = "Detalle de producto";
            this.DetalleProducto.Name = "DetalleProducto";
            this.DetalleProducto.ReadOnly = true;
            this.DetalleProducto.Width = 268;
            // 
            // Cantidad
            // 
            this.Cantidad.HeaderText = "Cantidad";
            this.Cantidad.Name = "Cantidad";
            this.Cantidad.ReadOnly = true;
            // 
            // IdProducto
            // 
            this.IdProducto.HeaderText = "IdProducto";
            this.IdProducto.Name = "IdProducto";
            this.IdProducto.Visible = false;
            // 
            // Producto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(540, 498);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Producto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nuevo producto";
            this.Load += new System.EventHandler(this.AltaProducto_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdComponentes)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.ComboBox cboRubros;
        public System.Windows.Forms.Label lblRubro;
        public System.Windows.Forms.Label lblStockReposicion;
        public System.Windows.Forms.TextBox txtStockMinimo;
        public System.Windows.Forms.TextBox txtPrecioUnitario;
        public System.Windows.Forms.Label lblPrecioUnitario;
        public System.Windows.Forms.TextBox txtDetalleProducto;
        public System.Windows.Forms.Label lblDetalleProducto;
        public System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.DataGridView grdComponentes;
        public System.Windows.Forms.Label lblCantidadComposicion;
        public System.Windows.Forms.TextBox txtCantidadComposicion;
        public System.Windows.Forms.Button btnAgregarComponente;
        public System.Windows.Forms.Label lblCodigoProductoComposicion;
        public System.Windows.Forms.TextBox txtCodigoProductoComposicion;
        public System.Windows.Forms.Button btnGuardar;
        public System.Windows.Forms.TextBox txtDetalleProductoComposicion;
        public System.Windows.Forms.Label lblDetalleProductoComposicion;
        public System.Windows.Forms.Label lblComponentes;
        public System.Windows.Forms.Button btnEliminar;
        public System.Windows.Forms.Label lblProductCode;
        public System.Windows.Forms.TextBox txtCodigoProducto;
        public System.Windows.Forms.Button btnCancelar;
        public System.Windows.Forms.DataGridViewTextBoxColumn CodigoProducto;
        public System.Windows.Forms.DataGridViewTextBoxColumn DetalleProducto;
        public System.Windows.Forms.DataGridViewTextBoxColumn Cantidad;
        public System.Windows.Forms.DataGridViewTextBoxColumn IdProducto;
    }
}