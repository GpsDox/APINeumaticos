﻿using System;
using System.Collections.Generic;
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
        [Route("clientesUsuario")]
        public IEnumerable<string> clientesUsuario()
        {
            Models.Cliente cliente = new Models.Cliente();
            return new string[] { "value1", "value2" };
        }

        // GET: api/Cliente/5
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
