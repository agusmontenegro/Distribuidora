using Persistencia.DAOs.Combo;
using Persistencia.DTOs;
using System.Collections.Generic;
using System.Data;

namespace Logica.Services.Combo
{
    public class ComboService : IComboService
    {
        private readonly IDAOCombo dAOCombo;

        public ComboService(IDAOCombo dAOCombo)
        {
            this.dAOCombo = dAOCombo;
        }

        public bool EsCombo_Id(string idProducto)
        {
            var esCombo = dAOCombo.EsCombo_Id(idProducto);
            return esCombo;
        }

        public bool EsCombo_Codigo(string codigoProducto)
        {
            var esCombo = dAOCombo.EsCombo_Codigo(codigoProducto);
            return esCombo;
        }

        public Persistencia.DTOs.Combo ObtenerCombo(string idProducto)
        {
            var result = dAOCombo.ObtenerCombo(idProducto);
            var combo = MapearCombo(result.Rows);
            return combo;
        }

        public void EliminarComponentes(string idProduct)
        {
            dAOCombo.EliminarComponentes(idProduct);
        }

        public void GuardarComponente(int idProducto, string idComponente, string cantidad)
        {
            dAOCombo.GuardarComponente(idProducto, idComponente, cantidad);
        }

        public string InformarStockFaltante(string idProducto, string cantidad)
        {
            var stockFaltante = dAOCombo.InformarStockFaltante(idProducto, cantidad);
            return stockFaltante;
        }

        private Persistencia.DTOs.Combo MapearCombo(DataRowCollection rows)
        {
            var combo = new Persistencia.DTOs.Combo();
            combo.Componentes = new List<Componente>();
            bool flag = true;

            foreach (DataRow row in rows)
            {
                if (flag)
                {
                    combo.Producto = new Persistencia.DTOs.Producto
                    {
                        Id = row["Id"].ToString(),
                        Codigo = row["Codigo"].ToString(),
                        Detalle = row["Detalle"].ToString(),
                        PrecioUnitario = decimal.Parse(row["Precio"].ToString())
                    };

                    flag = false;
                }
                var componente = new Componente
                {
                    Producto = new Persistencia.DTOs.Producto
                    {
                        Id = row["IdComponente"].ToString(),
                        Detalle = row["DetalleComponente"].ToString(),
                        Codigo = row["CodigoComponente"].ToString(),
                    },
                    Cantidad = row["CantidadComponente"].ToString()
                };

                combo.Componentes.Add(componente);
            }

            return combo;
        }
    }
}