﻿
namespace Presentacion.Forms
{
    partial class Estadistica
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
            this.chkIncludeNoActivo = new System.Windows.Forms.CheckBox();
            this.lblRubro = new System.Windows.Forms.Label();
            this.cboRubros = new System.Windows.Forms.ComboBox();
            this.cboMeses = new System.Windows.Forms.ComboBox();
            this.cboAños = new System.Windows.Forms.ComboBox();
            this.lblMes = new System.Windows.Forms.Label();
            this.lblAño = new System.Windows.Forms.Label();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.lblResultados = new System.Windows.Forms.Label();
            this.grdEstadisticas = new System.Windows.Forms.DataGridView();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.Codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Detalle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PrecioUnitario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CantidadActual = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CantidadTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Año = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Mes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Activo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Rubro = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdEstadisticas)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkIncludeNoActivo);
            this.groupBox1.Controls.Add(this.lblRubro);
            this.groupBox1.Controls.Add(this.cboRubros);
            this.groupBox1.Controls.Add(this.cboMeses);
            this.groupBox1.Controls.Add(this.cboAños);
            this.groupBox1.Controls.Add(this.lblMes);
            this.groupBox1.Controls.Add(this.lblAño);
            this.groupBox1.Location = new System.Drawing.Point(11, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(796, 62);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Criterio de estadística";
            // 
            // chkIncludeNoActivo
            // 
            this.chkIncludeNoActivo.AutoSize = true;
            this.chkIncludeNoActivo.Location = new System.Drawing.Point(634, 34);
            this.chkIncludeNoActivo.Name = "chkIncludeNoActivo";
            this.chkIncludeNoActivo.Size = new System.Drawing.Size(156, 17);
            this.chkIncludeNoActivo.TabIndex = 6;
            this.chkIncludeNoActivo.Text = "Incluir productos no activos";
            this.chkIncludeNoActivo.UseVisualStyleBackColor = true;
            // 
            // lblRubro
            // 
            this.lblRubro.AutoSize = true;
            this.lblRubro.Location = new System.Drawing.Point(223, 16);
            this.lblRubro.Name = "lblRubro";
            this.lblRubro.Size = new System.Drawing.Size(36, 13);
            this.lblRubro.TabIndex = 3;
            this.lblRubro.Text = "Rubro";
            // 
            // cboRubros
            // 
            this.cboRubros.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRubros.FormattingEnabled = true;
            this.cboRubros.Location = new System.Drawing.Point(226, 32);
            this.cboRubros.Name = "cboRubros";
            this.cboRubros.Size = new System.Drawing.Size(402, 21);
            this.cboRubros.TabIndex = 5;
            // 
            // cboMeses
            // 
            this.cboMeses.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMeses.FormattingEnabled = true;
            this.cboMeses.Location = new System.Drawing.Point(114, 32);
            this.cboMeses.Name = "cboMeses";
            this.cboMeses.Size = new System.Drawing.Size(106, 21);
            this.cboMeses.TabIndex = 4;
            // 
            // cboAños
            // 
            this.cboAños.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAños.FormattingEnabled = true;
            this.cboAños.Location = new System.Drawing.Point(9, 32);
            this.cboAños.Name = "cboAños";
            this.cboAños.Size = new System.Drawing.Size(99, 21);
            this.cboAños.TabIndex = 3;
            // 
            // lblMes
            // 
            this.lblMes.AutoSize = true;
            this.lblMes.Location = new System.Drawing.Point(114, 16);
            this.lblMes.Name = "lblMes";
            this.lblMes.Size = new System.Drawing.Size(27, 13);
            this.lblMes.TabIndex = 2;
            this.lblMes.Text = "Mes";
            // 
            // lblAño
            // 
            this.lblAño.AutoSize = true;
            this.lblAño.Location = new System.Drawing.Point(6, 16);
            this.lblAño.Name = "lblAño";
            this.lblAño.Size = new System.Drawing.Size(26, 13);
            this.lblAño.TabIndex = 0;
            this.lblAño.Text = "Año";
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Location = new System.Drawing.Point(813, 48);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(107, 29);
            this.btnLimpiar.TabIndex = 1;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // lblResultados
            // 
            this.lblResultados.AutoSize = true;
            this.lblResultados.Location = new System.Drawing.Point(8, 78);
            this.lblResultados.Name = "lblResultados";
            this.lblResultados.Size = new System.Drawing.Size(60, 13);
            this.lblResultados.TabIndex = 2;
            this.lblResultados.Text = "Resultados";
            // 
            // grdEstadisticas
            // 
            this.grdEstadisticas.AllowUserToAddRows = false;
            this.grdEstadisticas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdEstadisticas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Codigo,
            this.Detalle,
            this.PrecioUnitario,
            this.CantidadActual,
            this.CantidadTotal,
            this.Año,
            this.Mes,
            this.Activo,
            this.Rubro});
            this.grdEstadisticas.Location = new System.Drawing.Point(11, 96);
            this.grdEstadisticas.MultiSelect = false;
            this.grdEstadisticas.Name = "grdEstadisticas";
            this.grdEstadisticas.RowHeadersVisible = false;
            this.grdEstadisticas.Size = new System.Drawing.Size(909, 344);
            this.grdEstadisticas.TabIndex = 3;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(813, 13);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(107, 29);
            this.btnBuscar.TabIndex = 4;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // Codigo
            // 
            this.Codigo.DataPropertyName = "CodigoProducto";
            this.Codigo.HeaderText = "Codigo Producto";
            this.Codigo.Name = "Codigo";
            this.Codigo.ReadOnly = true;
            this.Codigo.Width = 110;
            // 
            // Detalle
            // 
            this.Detalle.DataPropertyName = "DetalleProducto";
            this.Detalle.HeaderText = "Detalle del producto";
            this.Detalle.Name = "Detalle";
            this.Detalle.ReadOnly = true;
            this.Detalle.Width = 463;
            // 
            // PrecioUnitario
            // 
            this.PrecioUnitario.DataPropertyName = "PrecioUnitarioProducto";
            this.PrecioUnitario.HeaderText = "Precio unitario";
            this.PrecioUnitario.Name = "PrecioUnitario";
            this.PrecioUnitario.ReadOnly = true;
            // 
            // CantidadActual
            // 
            this.CantidadActual.DataPropertyName = "StockActualProducto";
            this.CantidadActual.HeaderText = "Stock actual";
            this.CantidadActual.Name = "CantidadActual";
            this.CantidadActual.ReadOnly = true;
            // 
            // CantidadTotal
            // 
            this.CantidadTotal.DataPropertyName = "CantidadTotal";
            this.CantidadTotal.HeaderText = "Cantidad vendida";
            this.CantidadTotal.Name = "CantidadTotal";
            this.CantidadTotal.ReadOnly = true;
            this.CantidadTotal.Width = 133;
            // 
            // Año
            // 
            this.Año.DataPropertyName = "Año";
            this.Año.HeaderText = "Año";
            this.Año.Name = "Año";
            this.Año.Visible = false;
            // 
            // Mes
            // 
            this.Mes.DataPropertyName = "Mes";
            this.Mes.HeaderText = "Mes";
            this.Mes.Name = "Mes";
            this.Mes.Visible = false;
            // 
            // Activo
            // 
            this.Activo.DataPropertyName = "EstaActivoProducto";
            this.Activo.HeaderText = "Activo";
            this.Activo.Name = "Activo";
            this.Activo.Visible = false;
            // 
            // Rubro
            // 
            this.Rubro.DataPropertyName = "RubroProducto";
            this.Rubro.HeaderText = "Rubro";
            this.Rubro.Name = "Rubro";
            this.Rubro.Visible = false;
            // 
            // Estadistica
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(932, 450);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.grdEstadisticas);
            this.Controls.Add(this.lblResultados);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Estadistica";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Estadistica";
            this.Load += new System.EventHandler(this.Estadistica_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdEstadisticas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.Label lblRubro;
        public System.Windows.Forms.ComboBox cboRubros;
        public System.Windows.Forms.ComboBox cboMeses;
        public System.Windows.Forms.ComboBox cboAños;
        public System.Windows.Forms.Label lblMes;
        public System.Windows.Forms.Label lblAño;
        public System.Windows.Forms.Button btnLimpiar;
        public System.Windows.Forms.Label lblResultados;
        public System.Windows.Forms.DataGridView grdEstadisticas;
        public System.Windows.Forms.Button btnBuscar;
        public System.Windows.Forms.CheckBox chkIncludeNoActivo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Codigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Detalle;
        private System.Windows.Forms.DataGridViewTextBoxColumn PrecioUnitario;
        private System.Windows.Forms.DataGridViewTextBoxColumn CantidadActual;
        private System.Windows.Forms.DataGridViewTextBoxColumn CantidadTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn Año;
        private System.Windows.Forms.DataGridViewTextBoxColumn Mes;
        private System.Windows.Forms.DataGridViewTextBoxColumn Activo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Rubro;
    }
}