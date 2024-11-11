using Microsoft.EntityFrameworkCore;
using SistemaDeViajes.Models;

namespace SistemaDeViajes.Context;

public partial class SiViajeContext : DbContext
{
    public SiViajeContext()
    {
    }

    public SiViajeContext(DbContextOptions<SiViajeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AsignarSucursale> AsignarSucursales { get; set; }

    public virtual DbSet<DetalleViaje> DetalleViajes { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<Sucursale> Sucursales { get; set; }

    public virtual DbSet<Transportista> Transportistas { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Viaje> Viajes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=PC;Database=SiViaje;Trusted_Connection=True;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AsignarSucursale>(entity =>
        {
            entity.HasKey(e => e.AsignacionId);

            entity.Property(e => e.AsignacionId).HasColumnName("Asignacion_Id");
            entity.Property(e => e.DistanciaKm)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("Distancia_Km");
            entity.Property(e => e.EmpleadoId).HasColumnName("Empleado_Id");
            entity.Property(e => e.SucursalId).HasColumnName("Sucursal_Id");

            entity.HasOne(d => d.Empleado).WithMany(p => p.AsignarSucursales)
                .HasForeignKey(d => d.EmpleadoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AsignarSucursales_Empleados");

            entity.HasOne(d => d.Sucursal).WithMany(p => p.AsignarSucursales)
                .HasForeignKey(d => d.SucursalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AsignarSucursales_Sucursales");
        });

        modelBuilder.Entity<DetalleViaje>(entity =>
        {
            entity.HasKey(e => e.DetalleId);

            entity.ToTable("Detalle_Viajes");

            entity.Property(e => e.DetalleId).HasColumnName("Detalle_Id");
            entity.Property(e => e.DistanciaKm)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("Distancia_Km");
            entity.Property(e => e.EmpleadoId).HasColumnName("Empleado_Id");
            entity.Property(e => e.ViajeId).HasColumnName("Viaje_Id");

            entity.HasOne(d => d.Viaje).WithMany(p => p.DetalleViajes)
                .HasForeignKey(d => d.ViajeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Detalle_Viajes_Viajes");
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.Property(e => e.EmpleadoId).HasColumnName("Empleado_Id");
            entity.Property(e => e.Direccion)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.NombreEmpleado)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Sucursale>(entity =>
        {
            entity.HasKey(e => e.SucursalId);

            entity.Property(e => e.SucursalId).HasColumnName("Sucursal_Id");
            entity.Property(e => e.Direccion)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Transportista>(entity =>
        {
            entity.Property(e => e.TransportistaId).HasColumnName("Transportista_Id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.TarifaPorKm)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("Tarifa_Por_KM");
            entity.Property(e => e.Telefono)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.Property(e => e.UsuarioId).HasColumnName("Usuario_Id");
            entity.Property(e => e.Password)
                .HasMaxLength(150)
                .IsFixedLength();
            entity.Property(e => e.Correo)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.NombreUsuario)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Rol)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Viaje>(entity =>
        {
            entity.Property(e => e.ViajeId).HasColumnName("Viaje_Id");
            entity.Property(e => e.Fecha).HasColumnType("date");
            entity.Property(e => e.SucursalId).HasColumnName("Sucursal_Id");
            entity.Property(e => e.TotalCosto)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("Total_Costo");
            entity.Property(e => e.TotalDistancia)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("Total_Distancia");
            entity.Property(e => e.TransportistaId).HasColumnName("Transportista_Id");
            entity.Property(e => e.UsuarioId).HasColumnName("Usuario_Id");

            entity.HasOne(d => d.Sucursal).WithMany(p => p.Viajes)
                .HasForeignKey(d => d.SucursalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Viajes_Sucursales");

            entity.HasOne(d => d.Transportista).WithMany(p => p.Viajes)
                .HasForeignKey(d => d.TransportistaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Viajes_Transportistas");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Viajes)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Viajes_Usuarios");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
