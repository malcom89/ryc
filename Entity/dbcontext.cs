namespace MotoCredito.Entity
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class dbcontext : DbContext
    {
        public dbcontext()
            : base("name=dbcontext")
        {
        }

        public virtual DbSet<cajaChica> cajaChica { get; set; }
        public virtual DbSet<Clientes> Clientes { get; set; }
        public virtual DbSet<gastosFijo> gastosFijo { get; set; }
        public virtual DbSet<Marca> Marca { get; set; }
        public virtual DbSet<Modelo> Modelo { get; set; }
        public virtual DbSet<movimientosCajaChica> movimientosCajaChica { get; set; }
        public virtual DbSet<Pagos> Pagos { get; set; }
        public virtual DbSet<Permisos> Permisos { get; set; }
        public virtual DbSet<Prestamo> Prestamo { get; set; }
        public virtual DbSet<Recibo> Recibo { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<TelefonosDeContactos> TelefonosDeContactos { get; set; }
        public virtual DbSet<tipoGastosFijo> tipoGastosFijo { get; set; }
        public virtual DbSet<Tipoidentificacion> Tipoidentificacion { get; set; }
        public virtual DbSet<tipoMovimientosCajaChica> tipoMovimientosCajaChica { get; set; }
        public virtual DbSet<TipoPrestamo> TipoPrestamo { get; set; }
        public virtual DbSet<usuarios> usuarios { get; set; }
        public virtual DbSet<UserLoger> UserLoger { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<cajaChica>()
                .Property(e => e.descripcion)
                .IsFixedLength();

            modelBuilder.Entity<cajaChica>()
                .Property(e => e.montoMaximo)
                .HasPrecision(19, 4);

            modelBuilder.Entity<cajaChica>()
                .Property(e => e.balanceRestante)
                .HasPrecision(19, 4);

            modelBuilder.Entity<cajaChica>()
                .HasMany(e => e.movimientosCajaChica)
                .WithRequired(e => e.cajaChica)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Clientes>()
                .Property(e => e.Nombres)
                .IsFixedLength();

            modelBuilder.Entity<Clientes>()
                .Property(e => e.Apellidos)
                .IsFixedLength();

            modelBuilder.Entity<Clientes>()
                .Property(e => e.NoIdentificacion)
                .IsFixedLength();

            modelBuilder.Entity<Clientes>()
                .Property(e => e.numeroIdentificacion)
                .IsUnicode(false);

            modelBuilder.Entity<Clientes>()
                .Property(e => e.numeroTelefonico1)
                .IsUnicode(false);

            modelBuilder.Entity<Clientes>()
                .Property(e => e.numeroTelefonico2)
                .IsUnicode(false);

            modelBuilder.Entity<Clientes>()
                .Property(e => e.numeroTelefonicoReferencia1)
                .IsUnicode(false);

            modelBuilder.Entity<Clientes>()
                .Property(e => e.numeroTelefonicoReferencia2)
                .IsUnicode(false);

            modelBuilder.Entity<Clientes>()
                .Property(e => e.numeroTelefonicoReferencia3)
                .IsUnicode(false);

            modelBuilder.Entity<Clientes>()
                .Property(e => e.direccion)
                .IsUnicode(false);

            modelBuilder.Entity<Clientes>()
                .Property(e => e.nombreReferencia1)
                .IsUnicode(false);

            modelBuilder.Entity<Clientes>()
                .Property(e => e.nombreReferencia2)
                .IsUnicode(false);

            modelBuilder.Entity<Clientes>()
                .Property(e => e.nombreReferencia3)
                .IsUnicode(false);

            modelBuilder.Entity<Clientes>()
                .Property(e => e.fotoUrl)
                .IsUnicode(false);

            modelBuilder.Entity<Clientes>()
                .HasMany(e => e.Prestamo)
                .WithRequired(e => e.Clientes)
                .HasForeignKey(e => e.IdCliente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Clientes>()
                .HasMany(e => e.TelefonosDeContactos)
                .WithRequired(e => e.Clientes)
                .HasForeignKey(e => e.IdCliente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<gastosFijo>()
                .Property(e => e.descripcion)
                .IsFixedLength();

            modelBuilder.Entity<Marca>()
                .Property(e => e.Descripcion)
                .IsFixedLength();

            modelBuilder.Entity<Marca>()
                .HasMany(e => e.Modelo)
                .WithRequired(e => e.Marca)
                .HasForeignKey(e => e.IdMarca)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Modelo>()
                .Property(e => e.Descripcion)
                .IsFixedLength();

            modelBuilder.Entity<Modelo>()
                .Property(e => e.TipoVehiculo)
                .IsFixedLength();

            modelBuilder.Entity<Modelo>()
                .HasMany(e => e.Prestamo)
                .WithRequired(e => e.Modelo)
                .HasForeignKey(e => e.IdModelo)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<movimientosCajaChica>()
                .Property(e => e.descripcion)
                .IsFixedLength();

            modelBuilder.Entity<movimientosCajaChica>()
                .Property(e => e.monto)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Pagos>()
                .Property(e => e.Capital)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Pagos>()
                .Property(e => e.Intereses)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Pagos>()
                .Property(e => e.Mora)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Pagos>()
                .Property(e => e.Sobrante)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Pagos>()
                .HasMany(e => e.Recibo)
                .WithMany(e => e.Pagos)
                .Map(m => m.ToTable("pagoRecibo").MapLeftKey("pagoId").MapRightKey("reciboId"));

            modelBuilder.Entity<Permisos>()
                .Property(e => e.descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<Permisos>()
                .HasMany(e => e.usuarios)
                .WithMany(e => e.Permisos)
                .Map(m => m.ToTable("permisosUsuarios").MapLeftKey("permisoId").MapRightKey("usuarioid"));

            modelBuilder.Entity<Prestamo>()
                .Property(e => e.NoChasis)
                .IsFixedLength();

            modelBuilder.Entity<Prestamo>()
                .Property(e => e.NoPlaca)
                .IsFixedLength();

            modelBuilder.Entity<Prestamo>()
                .Property(e => e.MontoPrestamo)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Prestamo>()
                .Property(e => e.BalanceRestante)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Prestamo>()
                .Property(e => e.Cuota)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Prestamo>()
                .Property(e => e.interes)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Prestamo>()
                .Property(e => e.capital)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Prestamo>()
                .Property(e => e.color)
                .IsUnicode(false);

            modelBuilder.Entity<Prestamo>()
                .HasMany(e => e.Pagos)
                .WithRequired(e => e.Prestamo)
                .HasForeignKey(e => e.IdPrestamo)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Recibo>()
                .Property(e => e.InteresPagado)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Recibo>()
                .Property(e => e.moraPagada)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Recibo>()
                .Property(e => e.CapitalPagado)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Recibo>()
                .Property(e => e.abono)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Recibo>()
                .Property(e => e.TotalPagado)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Recibo>()
                .Property(e => e.comentario)
                .IsUnicode(false);

            modelBuilder.Entity<TelefonosDeContactos>()
                .Property(e => e.TipoTelefono)
                .IsFixedLength();

            modelBuilder.Entity<tipoGastosFijo>()
                .Property(e => e.descripcion)
                .IsFixedLength();

            modelBuilder.Entity<tipoGastosFijo>()
                .HasMany(e => e.gastosFijo)
                .WithRequired(e => e.tipoGastosFijo)
                .HasForeignKey(e => e.tipoGastoFijoId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tipoidentificacion>()
                .Property(e => e.Descripcion)
                .IsFixedLength();

            modelBuilder.Entity<Tipoidentificacion>()
                .HasMany(e => e.Clientes)
                .WithRequired(e => e.Tipoidentificacion1)
                .HasForeignKey(e => e.TipoIdentificacion)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tipoMovimientosCajaChica>()
                .Property(e => e.descripcion)
                .IsFixedLength();

            modelBuilder.Entity<tipoMovimientosCajaChica>()
                .HasMany(e => e.movimientosCajaChica)
                .WithRequired(e => e.tipoMovimientosCajaChica)
                .HasForeignKey(e => e.tipoMovimientoId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TipoPrestamo>()
                .Property(e => e.InteresPrestamo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<TipoPrestamo>()
                .Property(e => e.InteresMora)
                .HasPrecision(18, 0);

            modelBuilder.Entity<TipoPrestamo>()
                .Property(e => e.InteresSaldo)
                .HasPrecision(18, 0);

            modelBuilder.Entity<TipoPrestamo>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<TipoPrestamo>()
                .HasMany(e => e.Prestamo)
                .WithRequired(e => e.TipoPrestamo)
                .HasForeignKey(e => e.IdTipoPrestamo)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<usuarios>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<usuarios>()
                .Property(e => e.Apellido)
                .IsUnicode(false);

            modelBuilder.Entity<usuarios>()
                .Property(e => e.UserLogin)
                .IsFixedLength();

            modelBuilder.Entity<usuarios>()
                .Property(e => e.clave)
                .IsUnicode(false);

            modelBuilder.Entity<usuarios>()
                .HasMany(e => e.gastosFijo)
                .WithRequired(e => e.usuarios)
                .HasForeignKey(e => e.usuarioId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<usuarios>()
                .HasMany(e => e.movimientosCajaChica)
                .WithRequired(e => e.usuarios)
                .HasForeignKey(e => e.usuarioId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<usuarios>()
                .HasMany(e => e.Prestamo)
                .WithOptional(e => e.usuarios)
                .HasForeignKey(e => e.usuarioId);

            modelBuilder.Entity<usuarios>()
                .HasMany(e => e.Recibo)
                .WithOptional(e => e.usuarios)
                .HasForeignKey(e => e.usuarioId);
        }
    }
}
