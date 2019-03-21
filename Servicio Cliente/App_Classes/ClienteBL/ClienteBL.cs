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
                ClienteDAL.ClienteDAL clienteDal = new ClienteDAL.ClienteDAL();
                return clienteDal.clientesUsuario(Usuario);
                

            }catch(Exception e)
            {
                return null;
            }
        }

        public List<Models.Flota> flotasUsuario(int Usuario)
        {
            try
            {
                ClienteDAL.ClienteDAL clienteDal = new ClienteDAL.ClienteDAL();
                return clienteDal.flotasUsuario(Usuario);


            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<Models.Conductor> conductoresFlota(int Usuario, int CodFlota)
        {
            try
            {
                ClienteDAL.ClienteDAL clienteDal = new ClienteDAL.ClienteDAL();
                return clienteDal.conductoresFlota(Usuario,CodFlota);


            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}