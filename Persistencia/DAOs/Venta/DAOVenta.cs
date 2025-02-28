using Persistencia.Helpers.DataBase;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Persistencia.DAOs.Venta
{
    public class DAOVenta : IDAOVenta
    {
        private readonly IDataBaseHelper dataBaseHelper;

        public DAOVenta(IDataBaseHelper dataBaseHelper)
        {
            this.dataBaseHelper = dataBaseHelper;
        }

        public int GuardarVenta(string precioTotal)
        {
            dataBaseHelper.AgregarParametroEntrada(precioTotal, "@precioTotal", SqlDbType.Decimal);
            dataBaseHelper.AgregarParametroSalida("@codigo", SqlDbType.Int);

            var salidas = dataBaseHelper.ExecStoredProcedure("dbo.InsertarVenta");

            return int.Parse(salidas[0]);
        }

        public void GuardarItem(int codigoVenta, int IdProducto, decimal precioUnitario, int cantidad)
        {
            dataBaseHelper.AgregarParametroEntrada(codigoVenta.ToString(), "@codigoVenta", SqlDbType.Int);
            dataBaseHelper.AgregarParametroEntrada(IdProducto.ToString(), "@producto", SqlDbType.Int);
            dataBaseHelper.AgregarParametroEntrada(precioUnitario.ToString(), "@precioUnitario", SqlDbType.Decimal);
            dataBaseHelper.AgregarParametroEntrada(cantidad.ToString(), "@cantidad", SqlDbType.Int);

            _ = dataBaseHelper.ExecStoredProcedure("dbo.InsertarItem");
        }

        public DataTable ObtenerVenta(string codigoVenta)
        {
            string query = "select * from dbo.Venta_View where Codigo = @CodigoVenta";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@CodigoVenta", codigoVenta)
            };
            var result = dataBaseHelper.ExecQuery(query, parameters);
            return result;
        }
    }
}
