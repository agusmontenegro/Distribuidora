using Distribuidora.Factories;
using Distribuidora.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Distribuidora.Services
{
    public class VentaService
    {
        private readonly DataBaseHelper dataBaseHelper;

        public VentaService()
        {
            dataBaseHelper = DataBaseHelperFactory.Crear();
        }

        public int GuardarVenta(string precioTotal)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            SqlParameter precioTotalParameter = new SqlParameter("@precioTotal", SqlDbType.Decimal);
            precioTotalParameter.Value = decimal.Parse(precioTotal);

            SqlParameter codigoOuput = new SqlParameter("@codigo", SqlDbType.Int);
            codigoOuput.Direction = ParameterDirection.Output;

            parameters.Add(precioTotalParameter);
            parameters.Add(codigoOuput);

            dataBaseHelper.ExecStoredProcedure("dbo.InsertarVenta", parameters);

            return Convert.ToInt32(parameters[1].Value.ToString());
        }

        public void GuardarItem(int codigoVenta, int producto, decimal precioUnitario, int cantidad)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            SqlParameter codigoVentaParameter = new SqlParameter("@codigoVenta", SqlDbType.Int);
            codigoVentaParameter.Value = codigoVenta;

            SqlParameter productoParameter = new SqlParameter("@producto", SqlDbType.Int);
            productoParameter.Value = producto;

            SqlParameter precioUnitarioParameter = new SqlParameter("@precioUnitario", SqlDbType.Decimal);
            precioUnitarioParameter.Value = precioUnitario;

            SqlParameter cantidadParameter = new SqlParameter("@cantidad", SqlDbType.Int);
            cantidadParameter.Value = cantidad;

            parameters.Add(codigoVentaParameter);
            parameters.Add(productoParameter);
            parameters.Add(precioUnitarioParameter);
            parameters.Add(cantidadParameter);

            dataBaseHelper.ExecStoredProcedure("dbo.InsertarItem", parameters);
        }
    }
}