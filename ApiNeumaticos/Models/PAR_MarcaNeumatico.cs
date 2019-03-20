namespace ApiNeumaticos.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PAR_MarcaNeumatico
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PAR_MarcaNeumatico()
        {
            PAR_Neumatico = new HashSet<PAR_Neumatico>();
        }

        [Key]
        [StringLength(50)]
        public string Id_MarcaNeumatico { get; set; }

        [StringLength(500)]
        public string Desc_Glosa { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PAR_Neumatico> PAR_Neumatico { get; set; }
    }
}
