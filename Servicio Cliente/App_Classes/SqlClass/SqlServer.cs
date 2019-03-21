using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Servicio_Cliente.App_Classes.SqlClass
{
    public class SqlServer
    {
        public static DataSet ObtieneDataSet(List<SqlClass.SqlParametro> parametro, String ProcedimientoAlmacenado, string cadenaConexion)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection conexion = new SqlConnection(cadenaConexion))
                {
                    using (SqlCommand comando = new SqlCommand(ProcedimientoAlmacenado, conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.CommandTimeout = conexion.ConnectionTimeout;
                        comando.Parameters.Clear();

                        foreach (var item in parametro)
                        {
                            new SqlClass.SqlParametro().addParametro(item.Nombre, item.Valor, item.Tipo, comando);
                        }

                        da.SelectCommand = comando;
                        da.Fill(ds);
                    }
                }

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    return ds;
                }
                else
                    return null;

            }
            catch (Exception)
            {

                throw;
            }


        }


        public static DataSet ObtieneDataSet(string comandoSql, string cadenaConexion)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection conexion = new SqlConnection(cadenaConexion))
                {
                    using (SqlCommand comando = new SqlCommand(comandoSql, conexion))
                    {
                        comando.CommandType = CommandType.Text;
                        comando.CommandTimeout = conexion.ConnectionTimeout;
                        da.SelectCommand = comando;
                        da.Fill(ds);
                    }
                }
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    return ds;
                }
                else
                    return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}