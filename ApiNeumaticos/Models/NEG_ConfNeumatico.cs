namespace ApiNeumaticos.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class NEG_ConfNeumatico
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NEG_ConfNeumatico()
        {
            NEG_ConfEjeRuedas = new HashSet<NEG_ConfEjeRuedas>();
        }

        [Key]
        public int Id_ConfNeumatico { get; set; }

        public int? Cod_Vehiculo { get; set; }

        public int? Cod_Grupo { get; set; }

        public int? Cod_GrupoVehiculo { get; set; }

        [StringLength(50)]
        public string Patente { get; set; }

        [StringLength(50)]
        public string Id_TipoVehiculo { get; set; }

        public DateTime? Fec_Creacion { get; set; }

        public int? Id_ClienteDispositivo { get; set; }

        public DateTime? Fec_Modificacion { get; set; }

        [StringLength(50)]
        public string Ins_Id { get; set; }

        [StringLength(50)]
        public string Mod_Id { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NEG_ConfEjeRuedas> NEG_ConfEjeRuedas { get; set; }

        public virtual PAR_TipoVehiculo PAR_TipoVehiculo { get; set; }

        public virtual REL_ClienteDispositivo REL_ClienteDispositivo { get; set; }
    }
}
