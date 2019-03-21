using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Servicio_Cliente.Models
{
    public class Cliente
    {
        public Cliente()
        {
            IdCliente = "";
            NomCliente = "";
        }

        public Cliente(string idCliente, string nomCliente)
        {
            IdCliente = idCliente;
            NomCliente = nomCliente;
        }
        public String IdCliente { get; set; }
        public String NomCliente { get; set; }
    }
}