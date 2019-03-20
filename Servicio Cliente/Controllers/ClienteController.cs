using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Servicio_Cliente.Models;

namespace Servicio_Cliente.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("gpschile/neumaticos/cliente")]
    public class ClienteController : ApiController
    {
        /// <summary>
        /// GET: Obtiene todos los clientes
        /// </summary>
        /// <returns>List: Cliente</returns>
        [HttpGet]
        [Route("GetClientes")]
        public IEnumerable<string> GetClientes()
        {
            return new string[] { "valor X", "valor Y" };
        }

        // GET: api/Cliente/5
        [HttpGet]
        public string Get(int id)
        {
            return "value";
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
