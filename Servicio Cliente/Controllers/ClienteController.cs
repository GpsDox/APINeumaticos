using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Servicio_Cliente.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("gpschile/catalogo/cliente")]
    public class ClienteController : ApiController
    {
        [HttpGet]
        [Route("clientesUsuario/{Usuario}")]
        public List<Models.Cliente> clientesUsuario(int Usuario)
        {
            App_Classes.ClienteBL.ClienteBL clientebl = new App_Classes.ClienteBL.ClienteBL();
            return clientebl.clientesUsuario(Usuario);
        }

        [HttpGet]
        [Route("flotasUsuario/{Usuario}")]
        public List<Models.Flota> flotasUsuario(int Usuario)
        {
            App_Classes.ClienteBL.ClienteBL clientebl = new App_Classes.ClienteBL.ClienteBL();
            return clientebl.flotasUsuario(Usuario);
        }

        [HttpGet]
        [Route("conductoresFlota/{Usuario}/{CodFlota}")]
        public List<Models.Conductor> conductoresFlota(int Usuario, int CodFlota)
        {
            App_Classes.ClienteBL.ClienteBL clientebl = new App_Classes.ClienteBL.ClienteBL();
            return clientebl.conductoresFlota(Usuario,CodFlota);
        }

        // POST: api/Cliente
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Cliente/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Cliente/5
        public void Delete(int id)
        {
        }
    }
}
