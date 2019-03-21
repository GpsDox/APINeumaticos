using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Servicio_Cliente.Models
{
    public class Flota
    {
        public Flota()
        {
            CodFlota = 0;
            NomFlota = "";
        }

        public Flota(int codFlota, string nomFlota)
        {
            CodFlota = codFlota;
            NomFlota = nomFlota;
        }
        public int CodFlota { get; set; }
        public String NomFlota { get; set; }

    }
}