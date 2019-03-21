using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Servicio_Cliente.Models
{
    public class Conductor
    {
        public Conductor()
        {
            CodConductor = 0;
            NomConductor = "";
            PinConductor = "";
            CodFlota = 0;
        }

        public Conductor(int codConductor, string nomConductor, string pinConductor, int codFlota)
        {
            CodConductor = codConductor;
            NomConductor = nomConductor;
            PinConductor = pinConductor;
            CodFlota = codFlota;
        }
        public int CodConductor { get; set; }
        public String NomConductor { get; set; }
        public String PinConductor { get; set; }
        public int CodFlota { get; set; }
    }
}