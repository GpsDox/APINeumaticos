using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Servicio_Cliente.App_Classes.ClienteBL
{
    public class ClienteBL
    {

        public List<Models.Cliente> clientesUsuario(int Usuario)
        {
            try
            {
                App_Classes.ClienteDAL.ClienteDAL clienteDal = new App_Classes.ClienteDAL.ClienteDAL();
                return clienteDal.clientesUsuario(Usuario);
                

            }catch(Exception e)
            {
                return null;
            }
        }
    }
}