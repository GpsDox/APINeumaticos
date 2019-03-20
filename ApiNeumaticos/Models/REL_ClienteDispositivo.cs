namespace ApiNeumaticos.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class REL_ClienteDispositivo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public REL_ClienteDispositivo()
        {
            NEG_ConfNeumatico = new HashSet<NEG_ConfNeumatico>();
        }

        [Key]
        public int Id_ClienteDispositivo { get; set; }

        [StringLength(250)]
        public string Cod_AsignadoCliente { get; set; }

        [StringLength(250)]
        public string Nomb_Referencial { get; set; }

        [StringLength(50)]
        public string Id_Cliente { get; set; }

        [StringLength(50)]
        public string Id_TipoDispositivo { get; set; }

        [StringLength(50)]
        public string Id_MarcaDispositivo { get; set; }

        public DateTime? Fec_Creacion { get; set; }

        public DateTime? Fec_Modificacion { get; set; }

        [StringLength(50)]
        public string Ins_Id { get; set; }

        [StringLength(50)]
        public string Mod_Id { get; set; }

        public bool? Flag_Eliminado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NEG_ConfNeumatico> NEG_ConfNeumatico { get; set; }

        public virtual PAR_MarcaDispositivo PAR_MarcaDispositivo { get; set; }

        public virtual PAR_TipoDispositivo PAR_TipoDispositivo { get; set; }
    }
}
