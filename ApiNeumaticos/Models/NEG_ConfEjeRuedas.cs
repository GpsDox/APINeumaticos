namespace ApiNeumaticos.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class NEG_ConfEjeRuedas
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NEG_ConfEjeRuedas()
        {
            NEG_ConfRuedaEje = new HashSet<NEG_ConfRuedaEje>();
        }

        [Key]
        public int Id_ConfEjeRuedas { get; set; }

        public int? Nro_Eje { get; set; }

        public int? Cant_Ruedas { get; set; }

        public int? Id_ConfNeumatico { get; set; }

        public virtual NEG_ConfNeumatico NEG_ConfNeumatico { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NEG_ConfRuedaEje> NEG_ConfRuedaEje { get; set; }
    }
}
