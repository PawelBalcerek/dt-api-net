using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Data.Models
{
    public partial class RepositoryContext : DbContext
    {
        public RepositoryContext()
        {
        }

        public RepositoryContext(DbContextOptions<RepositoryContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BuyOffer> BuyOffers { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Configuration> Configurations { get; set; }
        public virtual DbSet<FlywaySchemaHistory> FlywaySchemaHistory { get; set; }
        public virtual DbSet<Resource> Resources { get; set; }
        public virtual DbSet<SellOffer> SellOffers { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            }

            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BuyOffer>(entity =>
            {
                entity.ToTable("buy_offers");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("timestamp with time zone");

                entity.Property(e => e.IsValid).HasColumnName("is_valid");

                entity.Property(e => e.MaxPrice).HasColumnName("max_price");

                entity.Property(e => e.ResourceId)
                    .HasColumnName("resource_id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.StartAmount).HasColumnName("start_amount");

                entity.HasOne(d => d.Resource)
                    .WithMany(p => p.BuyOffers)
                    .HasForeignKey(d => d.ResourceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("buy_offers_resource_id_fkey");
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.ToTable("companies");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("character varying(255)");
            });

            modelBuilder.Entity<Configuration>(entity =>
            {
                entity.HasKey(e => e.Name);

                entity.ToTable("configurations");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("character varying(255)")
                    .ValueGeneratedNever();

                entity.Property(e => e.Value).HasColumnName("number");
            });

            modelBuilder.Entity<FlywaySchemaHistory>(entity =>
            {
                entity.HasKey(e => e.InstalledRank);

                entity.ToTable("flyway_schema_history");

                entity.HasIndex(e => e.Success)
                    .HasName("flyway_schema_history_s_idx");

                entity.Property(e => e.InstalledRank)
                    .HasColumnName("installed_rank")
                    .ValueGeneratedNever();

                entity.Property(e => e.Checksum).HasColumnName("checksum");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasColumnType("character varying(200)");

                entity.Property(e => e.ExecutionTime).HasColumnName("execution_time");

                entity.Property(e => e.InstalledBy)
                    .IsRequired()
                    .HasColumnName("installed_by")
                    .HasColumnType("character varying(100)");

                entity.Property(e => e.InstalledOn)
                    .HasColumnName("installed_on")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Script)
                    .IsRequired()
                    .HasColumnName("script")
                    .HasColumnType("character varying(1000)");

                entity.Property(e => e.Success).HasColumnName("success");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasColumnName("type")
                    .HasColumnType("character varying(20)");

                entity.Property(e => e.Version)
                    .HasColumnName("version")
                    .HasColumnType("character varying(50)");
            });

            modelBuilder.Entity<Resource>(entity =>
            {
                entity.ToTable("resources");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.CompId)
                    .HasColumnName("comp_id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .ValueGeneratedOnAdd();

                entity.HasOne(d => d.Comp)
                    .WithMany(p => p.Resources)
                    .HasForeignKey(d => d.CompId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("resources_comp_id_fkey");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Resources)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("resources_user_id_fkey");
            });

            modelBuilder.Entity<SellOffer>(entity =>
            {
                entity.ToTable("sell_offers");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("timestamp with time zone");

                entity.Property(e => e.IsValid).HasColumnName("is_valid");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.ResourceId)
                    .HasColumnName("resource_id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.StartAmount).HasColumnName("start_amount");

                entity.HasOne(d => d.Resource)
                    .WithMany(p => p.SellOffers)
                    .HasForeignKey(d => d.ResourceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("sell_offers_resource_id_fkey");
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.ToTable("transactions");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.BuyOfferId)
                    .HasColumnName("buy_offer_id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("timestamp with time zone");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.SellOfferId)
                    .HasColumnName("sell_offer_id")
                    .ValueGeneratedOnAdd();

                entity.HasOne(d => d.BuyOffer)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.BuyOfferId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("transactions_buy_offer_id_fkey");

                entity.HasOne(d => d.SellOffer)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.SellOfferId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("transactions_sell_offer_id_fkey");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.Id).HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Cash).HasColumnName("cash");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasColumnType("character varying(255)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("character varying(255)");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasColumnType("character varying(255)");
            });
        }
    }
}
