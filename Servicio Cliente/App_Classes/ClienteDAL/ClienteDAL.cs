using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;


namespace Servicio_Cliente.App_Classes.ClienteDAL
{
    public class ClienteDAL
    {

        public List<Servicio_Cliente.Models.Cliente>  clientesUsuario(int Usuario)
        {
            try
            {
                string Conexion = ConfigurationManager.AppSettings["CORPORATIVO"].ToString();

                List<SqlClass.SqlParametro> parametro = new List<SqlClass.SqlParametro> ();
                parametro.Add(new SqlClass.SqlParametro("@Cod_usuario", DbType.Int32, Usuario));
                DataSet ds = SqlClass.SqlServer.ObtieneDataSet(parametro, "SVC_QRY_CLIENTESPORUSUARIO", Conexion);

                List<Servicio_Cliente.Models.Cliente> lista = new List<Models.Cliente>();
                if (ds!= null)
                {
                    foreach(DataRow row in ds.Tables[0].Rows)
                    {
                        string idcliente = row["ID_CLIENTE"].ToString();
                        string nomcliente = row["NOMCLIENTE"].ToString();
                        lista.Add(new Models.Cliente(idcliente, nomcliente));
                    }

                    return lista;
                    
                }
                return new List<Servicio_Cliente.Models.Cliente>(); ;
                
            }catch(Exception e)
            {
                return new List<Servicio_Cliente.Models.Cliente>();
            }
        }
    }
}