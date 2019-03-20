namespace ApiNeumaticos.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class NEG_ConfRuedaEje
    {
        [Key]
        public int Id_ConfRuedaEje { get; set; }

        public int? Psi { get; set; }

        public int? Km { get; set; }

        public int? Años { get; set; }

        public int? Recauchajes { get; set; }

        public DateTime? Fec_Compra { get; set; }

        public int? Precio_Compra { get; set; }

        public int? Garantia_Años_Compra { get; set; }

        public int? Id_Neumatico { get; set; }

        public int? Id_ConfEjeRuedas { get; set; }

        public int? Nro_Rueda { get; set; }

        public virtual NEG_ConfEjeRuedas NEG_ConfEjeRuedas { get; set; }

        public virtual PAR_Neumatico PAR_Neumatico { get; set; }
    }
}
