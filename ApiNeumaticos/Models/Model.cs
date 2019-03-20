namespace ApiNeumaticos.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model : DbContext
    {
        public Model()
            : base("name=Modelo")
        {
        }

        public virtual DbSet<NEG_Conductor> NEG_Conductor { get; set; }
        public virtual DbSet<NEG_ConfEjeRuedas> NEG_ConfEjeRuedas { get; set; }
        public virtual DbSet<NEG_ConfIniDisp> NEG_ConfIniDisp { get; set; }
        public virtual DbSet<NEG_ConfNeumatico> NEG_ConfNeumatico { get; set; }
        public virtual DbSet<NEG_ConfRuedaEje> NEG_ConfRuedaEje { get; set; }
        public virtual DbSet<PAR_MarcaDispositivo> PAR_MarcaDispositivo { get; set; }
        public virtual DbSet<PAR_MarcaNeumatico> PAR_MarcaNeumatico { get; set; }
        public virtual DbSet<PAR_Neumatico> PAR_Neumatico { get; set; }
        public virtual DbSet<PAR_TipoDispositivo> PAR_TipoDispositivo { get; set; }
        public virtual DbSet<PAR_TipoVehiculo> PAR_TipoVehiculo { get; set; }
        public virtual DbSet<REL_ClienteDispositivo> REL_ClienteDispositivo { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NEG_Conductor>()
                .Property(e => e.Dv_Conductor)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<NEG_Conductor>()
                .Property(e => e.Fono_Conductor)
                .IsUnicode(false);

            modelBuilder.Entity<NEG_Conductor>()
                .Property(e => e.Ins_Id)
                .IsUnicode(false);

            modelBuilder.Entity<NEG_Conductor>()
                .Property(e => e.Mod_Id)
                .IsUnicode(false);

            modelBuilder.Entity<NEG_ConfIniDisp>()
                .Property(e => e.Id_MarcaDispositivo)
                .IsUnicode(false);

            modelBuilder.Entity<NEG_ConfIniDisp>()
                .Property(e => e.Id_TipoVehiculo)
                .IsUnicode(false);

            modelBuilder.Entity<NEG_ConfIniDisp>()
                .Property(e => e.Ins_Id)
                .IsUnicode(false);

            modelBuilder.Entity<NEG_ConfIniDisp>()
                .Property(e => e.Mod_Id)
                .IsUnicode(false);

            modelBuilder.Entity<NEG_ConfNeumatico>()
                .Property(e => e.Patente)
                .IsUnicode(false);

            modelBuilder.Entity<NEG_ConfNeumatico>()
                .Property(e => e.Id_TipoVehiculo)
                .IsUnicode(false);

            modelBuilder.Entity<NEG_ConfNeumatico>()
                .Property(e => e.Ins_Id)
                .IsUnicode(false);

            modelBuilder.Entity<NEG_ConfNeumatico>()
                .Property(e => e.Mod_Id)
                .IsUnicode(false);

            modelBuilder.Entity<PAR_MarcaDispositivo>()
                .Property(e => e.Id_MarcaDispositivo)
                .IsUnicode(false);

            modelBuilder.Entity<PAR_MarcaDispositivo>()
                .Property(e => e.Desc_Glosa)
                .IsUnicode(false);

            modelBuilder.Entity<PAR_MarcaNeumatico>()
                .Property(e => e.Id_MarcaNeumatico)
                .IsUnicode(false);

            modelBuilder.Entity<PAR_MarcaNeumatico>()
                .Property(e => e.Desc_Glosa)
                .IsUnicode(false);

            modelBuilder.Entity<PAR_Neumatico>()
                .Property(e => e.Familia)
                .IsUnicode(false);

            modelBuilder.Entity<PAR_Neumatico>()
                .Property(e => e.Modelo)
                .IsUnicode(false);

            modelBuilder.Entity<PAR_Neumatico>()
                .Property(e => e.Id_MarcaNeumatico)
                .IsUnicode(false);

            modelBuilder.Entity<PAR_Neumatico>()
                .Property(e => e.Fec_Modificacion)
                .IsUnicode(false);

            modelBuilder.Entity<PAR_Neumatico>()
                .Property(e => e.Ins_Id)
                .IsUnicode(false);

            modelBuilder.Entity<PAR_Neumatico>()
                .Property(e => e.Mod_Id)
                .IsUnicode(false);

            modelBuilder.Entity<PAR_TipoDispositivo>()
                .Property(e => e.Id_TipoDispositivo)
                .IsUnicode(false);

            modelBuilder.Entity<PAR_TipoDispositivo>()
                .Property(e => e.Desc_Glosa)
                .IsUnicode(false);

            modelBuilder.Entity<PAR_TipoVehiculo>()
                .Property(e => e.Id_TipoVehiculo)
                .IsUnicode(false);

            modelBuilder.Entity<PAR_TipoVehiculo>()
                .Property(e => e.Desc_Glosa)
                .IsUnicode(false);

            modelBuilder.Entity<REL_ClienteDispositivo>()
                .Property(e => e.Cod_AsignadoCliente)
                .IsUnicode(false);

            modelBuilder.Entity<REL_ClienteDispositivo>()
                .Property(e => e.Nomb_Referencial)
                .IsUnicode(false);

            modelBuilder.Entity<REL_ClienteDispositivo>()
                .Property(e => e.Id_Cliente)
                .IsUnicode(false);

            modelBuilder.Entity<REL_ClienteDispositivo>()
                .Property(e => e.Id_TipoDispositivo)
                .IsUnicode(false);

            modelBuilder.Entity<REL_ClienteDispositivo>()
                .Property(e => e.Id_MarcaDispositivo)
                .IsUnicode(false);

            modelBuilder.Entity<REL_ClienteDispositivo>()
                .Property(e => e.Ins_Id)
                .IsUnicode(false);

            modelBuilder.Entity<REL_ClienteDispositivo>()
                .Property(e => e.Mod_Id)
                .IsUnicode(false);
        }
    }
}
