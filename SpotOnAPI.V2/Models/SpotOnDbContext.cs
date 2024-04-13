using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SpotOnAPI.V2.Models;

public partial class SpotOnDbContext : DbContext
{
    public SpotOnDbContext()
    {
    }

    public SpotOnDbContext(DbContextOptions<SpotOnDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Collar> Collars { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=SpotOnDB;Integrated Security=True;Trust Server Certificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Collar>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__COLLAR__F3BEEBFFCBC955E3");

            entity.ToTable("COLLAR");

            entity.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasColumnName("USER_ID");
            entity.Property(e => e.CollarId).HasColumnName("COLLAR_ID");
            entity.Property(e => e.Latitude)
                .HasColumnType("decimal(6, 4)")
                .HasColumnName("LATITUDE");
            entity.Property(e => e.Longitude)
                .HasColumnType("decimal(7, 4)")
                .HasColumnName("LONGITUDE");
            entity.Property(e => e.Nickname)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("NICKNAME");
            entity.Property(e => e.Timestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("TIMESTAMP");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__CUSTOMER__F3BEEBFF0859FB10");

            entity.ToTable("CUSTOMER");

            entity.HasIndex(e => e.Username, "UQ__CUSTOMER__B15BE12EDC1B6D66").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("USER_ID");
            entity.Property(e => e.FirstName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("PASSWORD");
            entity.Property(e => e.Username)
                .HasMaxLength(64)
                .IsUnicode(false)
                .HasColumnName("USERNAME");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
