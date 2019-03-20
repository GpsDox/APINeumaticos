namespace ApiNeumaticos.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PAR_TipoDispositivo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PAR_TipoDispositivo()
        {
            REL_ClienteDispositivo = new HashSet<REL_ClienteDispositivo>();
        }

        [Key]
        [StringLength(50)]
        public string Id_TipoDispositivo { get; set; }

        [StringLength(200)]
        public string Desc_Glosa { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<REL_ClienteDispositivo> REL_ClienteDispositivo { get; set; }
    }
}
