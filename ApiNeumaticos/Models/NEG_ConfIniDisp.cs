namespace ApiNeumaticos.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class NEG_ConfIniDisp
    {
        [Key]
        public int Id_ConfIniDisp { get; set; }

        public int? Cant_MaxDeEjes { get; set; }

        public int? Cant_MaxDeRuedasXEje { get; set; }

        [StringLength(50)]
        public string Id_MarcaDispositivo { get; set; }

        [StringLength(50)]
        public string Id_TipoVehiculo { get; set; }

        public DateTime? Fec_Creacion { get; set; }

        public DateTime? Fec_Modificacion { get; set; }

        [StringLength(50)]
        public string Ins_Id { get; set; }

        [StringLength(50)]
        public string Mod_Id { get; set; }

        public virtual PAR_MarcaDispositivo PAR_MarcaDispositivo { get; set; }

        public virtual PAR_TipoVehiculo PAR_TipoVehiculo { get; set; }
    }
}
