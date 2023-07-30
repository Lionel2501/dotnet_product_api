using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProductApiVSC.Models;

public partial class LearnDbContext : DbContext
{
    public LearnDbContext()
    {
    }

    public LearnDbContext(DbContextOptions<LearnDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<TblUser> TblUsers { get; set; }
    public virtual DbSet<TblRole> TbleRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            // optionsBuilder.UseSqlServer("Server=.;Database=LearnDb;Trusted_Connection=True;");
            optionsBuilder.UseSqlServer("Server=localhost; Database=LEARN_DB;user=sa;TrustServerCertificate=true; password=Tandil2019");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity
                // .HasNoKey()
                .ToTable("products");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("productname");
            // entity.Property(e => e.Price).HasColumnName("price");
        });

        modelBuilder.Entity<TblUser>(entity =>
            {
                entity.HasKey(e => e.Userid);

                entity.ToTable("tbl_user");

                entity.Property(e => e.Userid)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("userid");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.Role)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblRole>(entity =>
            {
                entity.HasKey(e => e.Roleid);

                entity.ToTable("tbl_role");

                entity.Property(e => e.Roleid)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("roleid");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
