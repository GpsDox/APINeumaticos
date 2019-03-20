namespace ApiNeumaticos.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class NEG_Conductor
    {
        [Key]
        public int Id_Conductor { get; set; }

        public int Rut_Conductor { get; set; }

        [StringLength(1)]
        public string Dv_Conductor { get; set; }

        [StringLength(500)]
        public string Fono_Conductor { get; set; }

        public int? Cod_Conductor { get; set; }

        public DateTime? Fec_Creacion { get; set; }

        public DateTime? Fec_Modificacion { get; set; }

        [StringLength(50)]
        public string Ins_Id { get; set; }

        [StringLength(50)]
        public string Mod_Id { get; set; }

        public bool? Flag_Eliminado { get; set; }

        public int? Cod_Vehiculo { get; set; }
    }
}
