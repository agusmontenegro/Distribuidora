
namespace Distribuidora.Forms
{
    partial class Menu
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnSale = new System.Windows.Forms.Button();
            this.btnProductos = new System.Windows.Forms.Button();
            this.btnStock = new System.Windows.Forms.Button();
            this.btnEstadistica = new System.Windows.Forms.Button();
            this.btnAlertas = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSale
            // 
            this.btnSale.Location = new System.Drawing.Point(12, 12);
            this.btnSale.Name = "btnSale";
            this.btnSale.Size = new System.Drawing.Size(776, 68);
            this.btnSale.TabIndex = 1;
            this.btnSale.Text = "NUEVA VENTA";
            this.btnSale.UseVisualStyleBackColor = true;
            this.btnSale.Click += new System.EventHandler(this.btnSale_Click);
            // 
            // btnProductos
            // 
            this.btnProductos.Location = new System.Drawing.Point(12, 160);
            this.btnProductos.Name = "btnProductos";
            this.btnProductos.Size = new System.Drawing.Size(776, 68);
            this.btnProductos.TabIndex = 3;
            this.btnProductos.Text = "PRODUCTOS";
            this.btnProductos.UseVisualStyleBackColor = true;
            this.btnProductos.Click += new System.EventHandler(this.btnProductos_Click);
            // 
            // btnStock
            // 
            this.btnStock.Location = new System.Drawing.Point(12, 86);
            this.btnStock.Name = "btnStock";
            this.btnStock.Size = new System.Drawing.Size(776, 68);
            this.btnStock.TabIndex = 4;
            this.btnStock.Text = "SEGUIMIENTO DE STOCK";
            this.btnStock.UseVisualStyleBackColor = true;
            this.btnStock.Click += new System.EventHandler(this.btnStock_Click);
            // 
            // btnEstadistica
            // 
            this.btnEstadistica.Location = new System.Drawing.Point(12, 234);
            this.btnEstadistica.Name = "btnEstadistica";
            this.btnEstadistica.Size = new System.Drawing.Size(776, 68);
            this.btnEstadistica.TabIndex = 5;
            this.btnEstadistica.Text = "ESTADÍSTICAS";
            this.btnEstadistica.UseVisualStyleBackColor = true;
            this.btnEstadistica.Click += new System.EventHandler(this.btnEstadistica_Click);
            // 
            // btnAlertas
            // 
            this.btnAlertas.Location = new System.Drawing.Point(12, 308);
            this.btnAlertas.Name = "btnAlertas";
            this.btnAlertas.Size = new System.Drawing.Size(776, 68);
            this.btnAlertas.TabIndex = 6;
            this.btnAlertas.Text = "ALERTAS";
            this.btnAlertas.UseVisualStyleBackColor = true;
            this.btnAlertas.Click += new System.EventHandler(this.btnAlertas_Click);
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 388);
            this.Controls.Add(this.btnAlertas);
            this.Controls.Add(this.btnEstadistica);
            this.Controls.Add(this.btnStock);
            this.Controls.Add(this.btnProductos);
            this.Controls.Add(this.btnSale);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Menu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Menú principal";
            this.Load += new System.EventHandler(this.Menu_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSale;
        private System.Windows.Forms.Button btnProductos;
        private System.Windows.Forms.Button btnStock;
        private System.Windows.Forms.Button btnEstadistica;
        private System.Windows.Forms.Button btnAlertas;
    }
}

