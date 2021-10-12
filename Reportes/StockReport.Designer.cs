
namespace Distribuidora.Reportes
{
    partial class StockReport
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Telerik.Reporting.TableGroup tableGroup1 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup2 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup3 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup4 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup5 = new Telerik.Reporting.TableGroup();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StockReport));
            Telerik.Reporting.Drawing.StyleRule styleRule1 = new Telerik.Reporting.Drawing.StyleRule();
            this.pageHeaderSection1 = new Telerik.Reporting.PageHeaderSection();
            this.detail = new Telerik.Reporting.DetailSection();
            this.txtFechaReposicion = new Telerik.Reporting.TextBox();
            this.tblStock = new Telerik.Reporting.Table();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.textBox7 = new Telerik.Reporting.TextBox();
            this.textBox9 = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.textBox6 = new Telerik.Reporting.TextBox();
            this.textBox8 = new Telerik.Reporting.TextBox();
            this.picLogo = new Telerik.Reporting.PictureBox();
            this.txtTitulo = new Telerik.Reporting.TextBox();
            this.txtFechaReposicionParametro = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageHeaderSection1
            // 
            this.pageHeaderSection1.Height = Telerik.Reporting.Drawing.Unit.Cm(4D);
            this.pageHeaderSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.txtFechaReposicion,
            this.txtTitulo,
            this.picLogo,
            this.txtFechaReposicionParametro});
            this.pageHeaderSection1.Name = "pageHeaderSection1";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Cm(2.3D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.tblStock});
            this.detail.Name = "detail";
            // 
            // txtFechaReposicion
            // 
            this.txtFechaReposicion.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(5.7D), Telerik.Reporting.Drawing.Unit.Cm(2D));
            this.txtFechaReposicion.Name = "txtFechaReposicion";
            this.txtFechaReposicion.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(5.067D), Telerik.Reporting.Drawing.Unit.Cm(0.556D));
            this.txtFechaReposicion.Style.Font.Name = "Malgun Gothic";
            this.txtFechaReposicion.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(14D);
            this.txtFechaReposicion.Value = "Fecha de reposición:";
            // 
            // tblStock
            // 
            this.tblStock.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Cm(7.169D)));
            this.tblStock.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Cm(3.097D)));
            this.tblStock.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Cm(3.244D)));
            this.tblStock.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Cm(2.489D)));
            this.tblStock.Body.Rows.Add(new Telerik.Reporting.TableBodyRow(Telerik.Reporting.Drawing.Unit.Cm(0.639D)));
            this.tblStock.Body.SetCellContent(0, 0, this.textBox3);
            this.tblStock.Body.SetCellContent(0, 1, this.textBox5);
            this.tblStock.Body.SetCellContent(0, 2, this.textBox7);
            this.tblStock.Body.SetCellContent(0, 3, this.textBox9);
            tableGroup1.Name = "tableGroup";
            tableGroup1.ReportItem = this.textBox2;
            tableGroup2.Name = "tableGroup1";
            tableGroup2.ReportItem = this.textBox4;
            tableGroup3.Name = "tableGroup2";
            tableGroup3.ReportItem = this.textBox6;
            tableGroup4.Name = "group";
            tableGroup4.ReportItem = this.textBox8;
            this.tblStock.ColumnGroups.Add(tableGroup1);
            this.tblStock.ColumnGroups.Add(tableGroup2);
            this.tblStock.ColumnGroups.Add(tableGroup3);
            this.tblStock.ColumnGroups.Add(tableGroup4);
            this.tblStock.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox3,
            this.textBox5,
            this.textBox7,
            this.textBox9,
            this.textBox2,
            this.textBox4,
            this.textBox6,
            this.textBox8});
            this.tblStock.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.5D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.tblStock.Name = "tblStock";
            tableGroup5.Groupings.Add(new Telerik.Reporting.Grouping(null));
            tableGroup5.Name = "detailTableGroup";
            this.tblStock.RowGroups.Add(tableGroup5);
            this.tblStock.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(16D), Telerik.Reporting.Drawing.Unit.Cm(1.5D));
            this.tblStock.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            // 
            // textBox3
            // 
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(7.169D), Telerik.Reporting.Drawing.Unit.Cm(0.639D));
            this.textBox3.Value = "=Fields.Detalle";
            // 
            // textBox5
            // 
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.097D), Telerik.Reporting.Drawing.Unit.Cm(0.639D));
            this.textBox5.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox5.Value = "=Fields.Cantidad";
            // 
            // textBox7
            // 
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.244D), Telerik.Reporting.Drawing.Unit.Cm(0.639D));
            this.textBox7.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox7.Value = "=Fields.PrecioUnitario";
            // 
            // textBox9
            // 
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.489D), Telerik.Reporting.Drawing.Unit.Cm(0.639D));
            this.textBox9.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox9.StyleName = "";
            this.textBox9.Value = "=Fields.Subtotal";
            // 
            // textBox2
            // 
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(7.169D), Telerik.Reporting.Drawing.Unit.Cm(0.861D));
            this.textBox2.Style.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.textBox2.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox2.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox2.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox2.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox2.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox2.Value = "DETALLE DE PRODUCTO";
            // 
            // textBox4
            // 
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.097D), Telerik.Reporting.Drawing.Unit.Cm(0.861D));
            this.textBox4.Style.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.textBox4.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox4.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox4.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox4.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox4.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox4.Value = "CANT. ANTERIOR";
            // 
            // textBox6
            // 
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.244D), Telerik.Reporting.Drawing.Unit.Cm(0.861D));
            this.textBox6.Style.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.textBox6.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox6.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox6.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox6.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox6.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox6.Value = "CANT. REPUESTA";
            // 
            // textBox8
            // 
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.489D), Telerik.Reporting.Drawing.Unit.Cm(0.861D));
            this.textBox8.Style.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.textBox8.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox8.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox8.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox8.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox8.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox8.StyleName = "";
            this.textBox8.Value = "CANT. TOTAL";
            // 
            // picLogo
            // 
            this.picLogo.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.5D), Telerik.Reporting.Drawing.Unit.Cm(0.4D));
            this.picLogo.MimeType = "image/jpeg";
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(4.7D), Telerik.Reporting.Drawing.Unit.Cm(3.3D));
            this.picLogo.Sizing = Telerik.Reporting.Drawing.ImageSizeMode.ScaleProportional;
            this.picLogo.Value = ((object)(resources.GetObject("picLogo.Value")));
            // 
            // txtTitulo
            // 
            this.txtTitulo.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(5.7D), Telerik.Reporting.Drawing.Unit.Cm(0.4D));
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
            // txtFechaReposicionParametro
            // 
            this.txtFechaReposicionParametro.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(10.767D), Telerik.Reporting.Drawing.Unit.Cm(2D));
            this.txtFechaReposicionParametro.Name = "txtFechaReposicionParametro";
            this.txtFechaReposicionParametro.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.933D), Telerik.Reporting.Drawing.Unit.Cm(0.556D));
            this.txtFechaReposicionParametro.Style.Font.Name = "Malgun Gothic";
            this.txtFechaReposicionParametro.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(14D);
            this.txtFechaReposicionParametro.Value = "=Fields.FechaReposicion";
            // 
            // StockReport
            // 
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pageHeaderSection1,
            this.detail});
            this.Name = "StockReport";
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
        private Telerik.Reporting.TextBox txtFechaReposicion;
        public Telerik.Reporting.Table tblStock;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.TextBox textBox7;
        private Telerik.Reporting.TextBox textBox9;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox textBox6;
        private Telerik.Reporting.TextBox textBox8;
        private Telerik.Reporting.TextBox txtTitulo;
        private Telerik.Reporting.PictureBox picLogo;
        private Telerik.Reporting.TextBox txtFechaReposicionParametro;
    }
}