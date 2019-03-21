using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Servicio_Cliente.App_Classes.SqlClass
{
    public class SqlParametro
    {
        private string v;
        private DbType int32;
        private object numAvl;

        public SqlParametro(String nombre, DbType tipo, Object valor)
        {
            this.Nombre = nombre;
            this.Tipo = tipo;
            this.Valor = valor;
        }

        public SqlParametro() { }       


        public String Nombre { get; set; }

        public DbType Tipo { get; set; }

        public Object Valor { get; set; }

        public void addParametro(String nombre, object valor, DbType tipo, SqlCommand comando)
        {
            try
            {
                IDataParameter dbParam_param = new SqlParameter();
                dbParam_param.ParameterName = nombre;
                dbParam_param.Value = valor == null ? DBNull.Value : valor;
                if (valor != null) dbParam_param.DbType = tipo;
                comando.Parameters.Add(dbParam_param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}