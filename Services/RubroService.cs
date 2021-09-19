using Distribuidora.DTOs;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace Distribuidora.Services
{
    public class RubroService
    {
        public List<Rubro> ObtenerRubros()
        {
            var rubros = new List<Rubro>();
            var query = "select * from dbo.Rubro";
            string ConnectionString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
            SqlConnection Connection = new SqlConnection(ConnectionString);
            using (Connection)
            {
                Connection.Open();
                using (var cmd = new SqlCommand(query, Connection))
                {
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        rubros.Add(new Rubro
                        {
                            Codigo = reader["rubr_codigo"].ToString(),
                            Detalle = reader["rubr_detalle"].ToString(),
                        });
                    }
                }
            }

            return rubros;
        }
    }
}