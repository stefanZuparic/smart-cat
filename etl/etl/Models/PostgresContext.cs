using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace etl.Models;

public partial class PostgresContext : DbContext
{
    public PostgresContext()
    {
    }

    public PostgresContext(DbContextOptions<PostgresContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Allowance> Allowances { get; set; }

    public virtual DbSet<AwardInterpretation> AwardInterpretations { get; set; }

    public virtual DbSet<Break> Breaks { get; set; }

    public virtual DbSet<Kpi> Kpis { get; set; }

    public virtual DbSet<Shift> Shifts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=postgres");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("uuid-ossp");

        modelBuilder.Entity<Allowance>(entity =>
        {
            entity.HasKey(e => e.AllowanceId).HasName("allowances_pkey");

            entity.ToTable("allowances");

            entity.Property(e => e.AllowanceId)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("allowance_id");
            entity.Property(e => e.AllowanceCost)
                .HasPrecision(13, 4)
                .HasColumnName("allowance_cost");
            entity.Property(e => e.AllowanceValue).HasColumnName("allowance_value");
            entity.Property(e => e.ShiftId).HasColumnName("shift_id");

            entity.HasOne(d => d.Shift).WithMany(p => p.Allowances)
                .HasForeignKey(d => d.ShiftId)
                .HasConstraintName("allowances_shift_id_fkey");
        });

        modelBuilder.Entity<AwardInterpretation>(entity =>
        {
            entity.HasKey(e => e.AwardId).HasName("award_interpretations_pkey");

            entity.ToTable("award_interpretations");

            entity.Property(e => e.AwardId)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("award_id");
            entity.Property(e => e.AwardCost)
                .HasPrecision(13, 4)
                .HasColumnName("award_cost");
            entity.Property(e => e.AwardDate).HasColumnName("award_date");
            entity.Property(e => e.AwardUnits).HasColumnName("award_units");
            entity.Property(e => e.ShiftId).HasColumnName("shift_id");

            entity.HasOne(d => d.Shift).WithMany(p => p.AwardInterpretations)
                .HasForeignKey(d => d.ShiftId)
                .HasConstraintName("award_interpretations_shift_id_fkey");
        });

        modelBuilder.Entity<Break>(entity =>
        {
            entity.HasKey(e => e.BreakId).HasName("breaks_pkey");

            entity.ToTable("breaks");

            entity.Property(e => e.BreakId)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("break_id");
            entity.Property(e => e.BreakFinish)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("break_finish");
            entity.Property(e => e.BreakStart)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("break_start");
            entity.Property(e => e.IsPaid)
                .HasDefaultValueSql("false")
                .HasColumnName("is_paid");
            entity.Property(e => e.ShiftId).HasColumnName("shift_id");

            entity.HasOne(d => d.Shift).WithMany(p => p.Breaks)
                .HasForeignKey(d => d.ShiftId)
                .HasConstraintName("breaks_shift_id_fkey");
        });

        modelBuilder.Entity<Kpi>(entity =>
        {
            entity.HasKey(e => e.KpiId).HasName("kpis_pkey");

            entity.ToTable("kpis");

            entity.HasIndex(e => new { e.KpiName, e.KpiDate }, "kpis_kpi_name_kpi_date_key").IsUnique();

            entity.Property(e => e.KpiId).HasColumnName("kpi_id");
            entity.Property(e => e.KpiDate).HasColumnName("kpi_date");
            entity.Property(e => e.KpiName)
                .HasMaxLength(255)
                .HasColumnName("kpi_name");
            entity.Property(e => e.KpiValue)
                .HasPrecision(8, 2)
                .HasColumnName("kpi_value");
        });

        modelBuilder.Entity<Shift>(entity =>
        {
            entity.HasKey(e => e.ShiftId).HasName("shifts_pkey");

            entity.ToTable("shifts");

            entity.Property(e => e.ShiftId)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("shift_id");
            entity.Property(e => e.ShiftCost)
                .HasPrecision(13, 4)
                .HasColumnName("shift_cost");
            entity.Property(e => e.ShiftDate).HasColumnName("shift_date");
            entity.Property(e => e.ShiftFinish)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("shift_finish");
            entity.Property(e => e.ShiftStart)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("shift_start");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
