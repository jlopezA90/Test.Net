using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Configuration;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace api.test.Models
{
    public partial class testContext : DbContext
    {
        private IConfiguration _config;
        public testContext()
        {
        }

        public testContext(DbContextOptions<testContext> options, IConfiguration config)
            : base(options)
        {
            _config = config;
        }

        public virtual DbSet<CustomerRating> CustomerRatings { get; set; }
        public virtual DbSet<Hotel> Hotels { get; set; }
        public virtual DbSet<HotelPhoto> HotelPhotos { get; set; }
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySQL(Startup.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerRating>(entity =>
            {
                entity.HasKey(e => e.ClientId)
                    .HasName("PRIMARY");

                entity.ToTable("customer_ratings");

                entity.Property(e => e.ClientId).HasColumnName("ClientID");

                entity.Property(e => e.HotelId).HasColumnName("HotelID");

                entity.Property(e => e.Rating).HasColumnType("tinyint(1) unsigned zerofill");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(45);
            });

            modelBuilder.Entity<Hotel>(entity =>
            {
                entity.ToTable("hotel");

                entity.Property(e => e.HotelId).HasColumnName("HotelID");

                entity.Property(e => e.Category).HasColumnType("tinyint(1) unsigned zerofill");

                entity.Property(e => e.HotelName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Price).HasColumnType("decimal(15,2)");
            });

            modelBuilder.Entity<HotelPhoto>(entity =>
            {
                entity.HasKey(e => e.PhotoId)
                    .HasName("PRIMARY");

                entity.ToTable("hotel_photos");

                entity.Property(e => e.PhotoId).HasColumnName("PhotoID");

                entity.Property(e => e.HotelId).HasColumnName("HotelID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Url).HasMaxLength(500);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
