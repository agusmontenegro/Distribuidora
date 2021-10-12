
namespace Distribuidora.Reportes
{
    partial class InfoProductos
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InfoProductos));
            Telerik.Reporting.TableGroup tableGroup1 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup2 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup3 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup4 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup5 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.Drawing.StyleRule styleRule1 = new Telerik.Reporting.Drawing.StyleRule();
            this.pageHeaderSection1 = new Telerik.Reporting.PageHeaderSection();
            this.detail = new Telerik.Reporting.DetailSection();
            this.picLogo = new Telerik.Reporting.PictureBox();
            this.txtTitulo = new Telerik.Reporting.TextBox();
            this.txtFecha = new Telerik.Reporting.TextBox();
            this.tblProductos = new Telerik.Reporting.Table();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.textBox10 = new Telerik.Reporting.TextBox();
            this.textBox6 = new Telerik.Reporting.TextBox();
            this.textBox7 = new Telerik.Reporting.TextBox();
            this.txtFechaParametro = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageHeaderSection1
            // 
            this.pageHeaderSection1.Height = Telerik.Reporting.Drawing.Unit.Cm(3.3D);
            this.pageHeaderSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.txtFecha,
            this.txtTitulo,
            this.picLogo,
            this.txtFechaParametro});
            this.pageHeaderSection1.Name = "pageHeaderSection1";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Cm(2.1D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.tblProductos});
            this.detail.Name = "detail";
            // 
            // picLogo
            // 
            this.picLogo.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.5D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.picLogo.MimeType = "image/jpeg";
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(4.7D), Telerik.Reporting.Drawing.Unit.Cm(3.3D));
            this.picLogo.Sizing = Telerik.Reporting.Drawing.ImageSizeMode.ScaleProportional;
            this.picLogo.Value = ((object)(resources.GetObject("picLogo.Value")));
            // 
            // txtTitulo
            // 
            this.txtTitulo.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(5.7D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.txtTitulo.Name = "txtTitulo";
            this.txtTitulo.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(10.8D), Telerik.Reporting.Drawing.Unit.Cm(1D));
            this.txtTitulo.Style.BorderColor.Bottom = System.Drawing.Color.Black;
            this.txtTitulo.Style.BorderColor.Default = System.Drawing.Color.Black;
            this.txtTitulo.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Ridge;
            this.txtTitulo.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.txtTitulo.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(1D);
            this.txtTitulo.Style.Font.Bold = true;
            this.txtTitulo.Style.Font.Italic = false;
            this.txtTitulo.Style.Font.Name = "Malgun Gothic Semilight";
            this.txtTitulo.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(20D);
            this.txtTitulo.Style.Font.Strikeout = false;
            this.txtTitulo.Style.Font.Underline = false;
            this.txtTitulo.Value = "Distribuidora Don Plácido";
            // 
            // txtFecha
            // 
            this.txtFecha.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(5.7D), Telerik.Reporting.Drawing.Unit.Cm(1.2D));
            this.txtFecha.Name = "txtFecha";
            this.txtFecha.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.2D), Telerik.Reporting.Drawing.Unit.Cm(0.556D));
            this.txtFecha.Style.Font.Name = "Malgun Gothic";
            this.txtFecha.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.txtFecha.Value = "Fecha:";
            // 
            // tblProductos
            // 
            this.tblProductos.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Cm(1.651D)));
            this.tblProductos.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Cm(8.389D)));
            this.tblProductos.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Cm(2.547D)));
            this.tblProductos.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Cm(3.413D)));
            this.tblProductos.Body.Rows.Add(new Telerik.Reporting.TableBodyRow(Telerik.Reporting.Drawing.Unit.Cm(0.639D)));
            this.tblProductos.Body.SetCellContent(0, 1, this.textBox3);
            this.tblProductos.Body.SetCellContent(0, 2, this.textBox5);
            this.tblProductos.Body.SetCellContent(0, 0, this.textBox10);
            this.tblProductos.Body.SetCellContent(0, 3, this.textBox7);
            tableGroup1.Name = "group1";
            tableGroup1.ReportItem = this.textBox1;
            tableGroup2.Name = "tableGroup";
            tableGroup2.ReportItem = this.textBox2;
            tableGroup3.Name = "tableGroup1";
            tableGroup3.ReportItem = this.textBox4;
            tableGroup4.Name = "group";
            tableGroup4.ReportItem = this.textBox6;
            this.tblProductos.ColumnGroups.Add(tableGroup1);
            this.tblProductos.ColumnGroups.Add(tableGroup2);
            this.tblProductos.ColumnGroups.Add(tableGroup3);
            this.tblProductos.ColumnGroups.Add(tableGroup4);
            this.tblProductos.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox3,
            this.textBox5,
            this.textBox2,
            this.textBox4,
            this.textBox1,
            this.textBox10,
            this.textBox6,
            this.textBox7});
            this.tblProductos.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.5D), Telerik.Reporting.Drawing.Unit.Cm(0.3D));
            this.tblProductos.Name = "tblProductos";
            tableGroup5.Groupings.Add(new Telerik.Reporting.Grouping(null));
            tableGroup5.Name = "detailTableGroup";
            this.tblProductos.RowGroups.Add(tableGroup5);
            this.tblProductos.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(16D), Telerik.Reporting.Drawing.Unit.Cm(1.5D));
            this.tblProductos.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            // 
            // textBox3
            // 
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(8.389D), Telerik.Reporting.Drawing.Unit.Cm(0.639D));
            this.textBox3.Value = "=Fields.Detalle";
            // 
            // textBox5
            // 
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.547D), Telerik.Reporting.Drawing.Unit.Cm(0.639D));
            this.textBox5.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox5.Value = "=Fields.Precio";
            // 
            // textBox2
            // 
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(8.389D), Telerik.Reporting.Drawing.Unit.Cm(0.861D));
            this.textBox2.Style.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.textBox2.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox2.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox2.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox2.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox2.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox2.Value = "DETALLE";
            // 
            // textBox4
            // 
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.547D), Telerik.Reporting.Drawing.Unit.Cm(0.861D));
            this.textBox4.Style.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.textBox4.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox4.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox4.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox4.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox4.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox4.Value = "PRECIO ($)";
            // 
            // textBox1
            // 
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.651D), Telerik.Reporting.Drawing.Unit.Cm(0.861D));
            this.textBox1.Style.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.textBox1.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox1.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox1.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox1.StyleName = "";
            this.textBox1.Value = "COD.";
            // 
            // textBox10
            // 
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.651D), Telerik.Reporting.Drawing.Unit.Cm(0.639D));
            this.textBox10.StyleName = "";
            this.textBox10.Value = "=Fields.Codigo";
            // 
            // textBox6
            // 
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.413D), Telerik.Reporting.Drawing.Unit.Cm(0.861D));
            this.textBox6.Style.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.textBox6.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox6.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox6.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox6.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox6.StyleName = "";
            this.textBox6.Value = "PTO. REPOSICIÓN";
            // 
            // textBox7
            // 
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.413D), Telerik.Reporting.Drawing.Unit.Cm(0.639D));
            this.textBox7.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox7.StyleName = "";
            this.textBox7.Value = "=Fields.Reposicion";
            // 
            // txtFechaParametro
            // 
            this.txtFechaParametro.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(6.9D), Telerik.Reporting.Drawing.Unit.Cm(1.2D));
            this.txtFechaParametro.Name = "txtFechaParametro";
            this.txtFechaParametro.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.7D), Telerik.Reporting.Drawing.Unit.Cm(0.556D));
            this.txtFechaParametro.Style.Font.Name = "Malgun Gothic";
            this.txtFechaParametro.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.txtFechaParametro.Value = "=Fields.Fecha";
            // 
            // InfoProductos
            // 
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pageHeaderSection1,
            this.detail});
            this.Name = "InfoProductos";
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Mm(20D), Telerik.Reporting.Drawing.Unit.Mm(20D), Telerik.Reporting.Drawing.Unit.Mm(20D), Telerik.Reporting.Drawing.Unit.Mm(20D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
            styleRule1.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.TextItemBase)),
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.HtmlTextBox))});
            styleRule1.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(2D);
            styleRule1.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Point(2D);
            this.StyleSheet.AddRange(new Telerik.Reporting.Drawing.StyleRule[] {
            styleRule1});
            this.Width = Telerik.Reporting.Drawing.Unit.Cm(17D);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.PageHeaderSection pageHeaderSection1;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.TextBox txtFecha;
        private Telerik.Reporting.TextBox txtTitulo;
        private Telerik.Reporting.PictureBox picLogo;
        public Telerik.Reporting.Table tblProductos;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox textBox10;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox textBox7;
        private Telerik.Reporting.TextBox textBox6;
        private Telerik.Reporting.TextBox txtFechaParametro;
    }
}