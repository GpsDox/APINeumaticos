namespace ApiNeumaticos.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PAR_TipoVehiculo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PAR_TipoVehiculo()
        {
            NEG_ConfIniDisp = new HashSet<NEG_ConfIniDisp>();
            NEG_ConfNeumatico = new HashSet<NEG_ConfNeumatico>();
        }

        [Key]
        [StringLength(50)]
        public string Id_TipoVehiculo { get; set; }

        [StringLength(200)]
        public string Desc_Glosa { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NEG_ConfIniDisp> NEG_ConfIniDisp { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NEG_ConfNeumatico> NEG_ConfNeumatico { get; set; }
    }
}
