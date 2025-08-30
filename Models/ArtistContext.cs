using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Artist_api1.Models
{
    public partial class ArtistContext : DbContext
    {
        public ArtistContext()
        {
        }

        public ArtistContext(DbContextOptions<ArtistContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<UserArtist> UserArtists { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-V82IM2M\\MSSQLSERVER01;Database=Artist;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.ProductAlcoholCategory).HasMaxLength(50);

                entity.Property(e => e.ProductMainCategory).HasMaxLength(50);

                entity.Property(e => e.ProductName).HasMaxLength(50);

                entity.Property(e => e.ProductPhoto).HasMaxLength(100);

                entity.Property(e => e.ProductSubCategory).HasMaxLength(50);

                entity.Property(e => e.ProductPrice).HasMaxLength(50);


                entity.Property(e => e.ProductAlcoholCategory).HasMaxLength(50);

            });

            modelBuilder.Entity<UserArtist>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__Users__1788CC4C022408D5");

                entity.ToTable("UserArtist");

                entity.Property(e => e.UserName).HasMaxLength(50);

                entity.Property(e => e.UserPassword).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
