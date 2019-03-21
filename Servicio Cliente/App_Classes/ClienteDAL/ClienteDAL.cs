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

        public List<Models.Cliente>  clientesUsuario(int Usuario)
        {
            try
            {
                string Conexion = ConfigurationManager.AppSettings["CORPORATIVO"].ToString();

                List<SqlClass.SqlParametro> parametro = new List<SqlClass.SqlParametro> ();
                parametro.Add(new SqlClass.SqlParametro("@Cod_usuario", DbType.Int32, Usuario));
                DataSet ds = SqlClass.SqlServer.ObtieneDataSet(parametro, "SVC_QRY_CLIENTESPORUSUARIO", Conexion);

                List<Models.Cliente> lista = new List<Models.Cliente>();
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
                return new List<Models.Cliente>(); ;
                
            }catch(Exception e)
            {
                return new List<Models.Cliente>();
            }
        }

        public List<Models.Flota> flotasUsuario(int Usuario)
        {
            try
            {
                string Conexion = ConfigurationManager.AppSettings["MONITOR"].ToString();

                List<SqlClass.SqlParametro> parametro = new List<SqlClass.SqlParametro>();
                parametro.Add(new SqlClass.SqlParametro("@Cod_usuario", DbType.Int32, Usuario));
                DataSet ds = SqlClass.SqlServer.ObtieneDataSet(parametro, "SVC_QRY_FLOTAPORUSUARIO", Conexion);

                List<Models.Flota> lista = new List<Models.Flota>();
                if (ds != null)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        int codflota = int.Parse(row["Cod_Flota"].ToString());
                        string nomFlota = row["NomFlota"].ToString();
                        lista.Add(new Models.Flota(codflota, nomFlota));
                    }

                    return lista;

                }
                return new List<Models.Flota>(); ;

            }
            catch (Exception e)
            {
                return new List<Models.Flota>();
            }
        }

        public List<Models.Conductor> conductoresFlota(int Usuario, int CodFlota)
        {
            try
            {
                string Conexion = ConfigurationManager.AppSettings["MONITOR"].ToString();

                List<SqlClass.SqlParametro> parametro = new List<SqlClass.SqlParametro>();
                parametro.Add(new SqlClass.SqlParametro("@Cod_Flota", DbType.Int32, CodFlota));
                parametro.Add(new SqlClass.SqlParametro("@Cod_Usuario", DbType.Int32, Usuario));
                DataSet ds = SqlClass.SqlServer.ObtieneDataSet(parametro, "SVC_QRY_CONDUCTORPORFLOTAUSUARIO", Conexion);

                List<Models.Conductor> lista = new List<Models.Conductor>();
                if (ds != null)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        int codconductor = int.Parse(row["Cod_Conductor"].ToString());
                        string nomconductor = row["NomConductor"].ToString();
                        string pinconductor = row["PinConductor"].ToString();
                        int codflota = int.Parse(row["Cod_Flota"].ToString());
                        lista.Add(new Models.Conductor(codconductor,nomconductor,pinconductor,codflota));
                    }

                    return lista;

                }
                return new List<Models.Conductor>(); ;

            }
            catch (Exception e)
            {
                return new List<Models.Conductor>();
            }
        }
    }
}