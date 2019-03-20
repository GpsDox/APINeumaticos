namespace ApiNeumaticos.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PAR_Neumatico
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PAR_Neumatico()
        {
            NEG_ConfRuedaEje = new HashSet<NEG_ConfRuedaEje>();
        }

        [Key]
        public int Id_Neumatico { get; set; }

        [StringLength(500)]
        public string Familia { get; set; }

        [StringLength(500)]
        public string Modelo { get; set; }

        public int? Psi { get; set; }

        public int? Kilometraje { get; set; }

        public int? Cant_AnosDuracion { get; set; }

        public int? Cant_Recauchajes { get; set; }

        [StringLength(50)]
        public string Id_MarcaNeumatico { get; set; }

        public DateTime Fec_Creacion { get; set; }

        [StringLength(50)]
        public string Fec_Modificacion { get; set; }

        [StringLength(50)]
        public string Ins_Id { get; set; }

        [StringLength(50)]
        public string Mod_Id { get; set; }

        public bool Flag_Eliminado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NEG_ConfRuedaEje> NEG_ConfRuedaEje { get; set; }

        public virtual PAR_MarcaNeumatico PAR_MarcaNeumatico { get; set; }
    }
}
