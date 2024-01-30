using ClosedXML.Excel;
using Persistencia.DAOs;
using System;
using System.Configuration;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace Persistencia.Helpers
{
    public class ExcelHelper
    {
        private readonly DAOProducto DAOProducto;

        public ExcelHelper()
        {
            DAOProducto = new DAOProducto();
        }

        public string ImportarProductos()
        {
            string ssqltable = "dbo.Producto";
            string myexceldataquery = "select Codigo, Nombre, Precio from Productos";
            try
            {
                //create our connection strings
                var path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string sexcelconnectionstring = @"provider=microsoft.jet.oledb.4.0;data source=" + path + @"\Productos.xlsx;extended properties=" + "\"excel 8.0;hdr=yes;\"";
                //series of commands to bulk copy data from the excel file into our sql table
                var oledbconn = new OleDbConnection(sexcelconnectionstring);
                var oledbcmd = new OleDbCommand(myexceldataquery, oledbconn);
                oledbconn.Open();
                var dr = oledbcmd.ExecuteReader();
                var conn = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
                var bulkcopy = new SqlBulkCopy(conn);
                bulkcopy.DestinationTableName = ssqltable;
                while (dr.Read())
                {
                    bulkcopy.WriteToServer(dr);
                }
                dr.Close();
                oledbconn.Close();
                return "Se ha importado correctamente";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        public string ExportarProductos()
        {
            try
            {
                XLWorkbook wb = new XLWorkbook();
                var dt = DAOProducto.ObtenerProductosParaExcel();
                var path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                wb.Worksheets.Add(dt, "Productos");
                wb.SaveAs(path + @"\Productos.xlsx");
                return "Se ha exportado correctamente en " + path;
            }
            catch (Exception ex)
            {
                return "Error al exportar " + ex.Message;
            }
        }
    }
}
