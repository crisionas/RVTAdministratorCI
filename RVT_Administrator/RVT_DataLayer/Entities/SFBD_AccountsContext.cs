using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace RVT_DataLayer.Entities
{
    public partial class SFBD_AccountsContext : DbContext
    {
        public SFBD_AccountsContext()
        {
        }

        public SFBD_AccountsContext(DbContextOptions<SFBD_AccountsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Block> Blocks { get; set; }
        public virtual DbSet<FiscDatum> FiscData { get; set; }
        public virtual DbSet<IdvnAccount> IdvnAccounts { get; set; }
        public virtual DbSet<Party> Parties { get; set; }
        public virtual DbSet<Region> Regions { get; set; }
        public virtual DbSet<VoteStatus> VoteStatuses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
optionsBuilder.UseSqlServer("Server=localhost;Database=SFBD_Accounts;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Block>(entity =>
            {
                entity.Property(e => e.BlockId).HasColumnName("BlockID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Hash)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.PreviousHash)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<FiscDatum>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Fisc_Data");

                entity.Property(e => e.BirthDate)
                    .HasColumnType("date")
                    .HasColumnName("Birth_date");

                entity.Property(e => e.Gender)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Idnp)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("IDNP");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Region)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Surname)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<IdvnAccount>(entity =>
            {
                entity.HasKey(e => e.Idvn)
                    .HasName("PK__idvn_acc__B87C0A442B488B7E");

                entity.ToTable("idvn_accounts");

                entity.Property(e => e.Idvn)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("IDVN");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.IpAddress)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("IP_address");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Phone_Number");

                entity.Property(e => e.RegisterDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Register_Date");

                entity.Property(e => e.StatusNumber)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Status_Number");

                entity.Property(e => e.VnPassword)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("vn_password");
            });

            modelBuilder.Entity<Party>(entity =>
            {
                entity.HasKey(e => e.Idpart);

                entity.Property(e => e.Idpart).HasColumnName("IDPart");

                entity.Property(e => e.Party1)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Party");
            });

            modelBuilder.Entity<Region>(entity =>
            {
                entity.HasKey(e => e.Idreg);

                entity.Property(e => e.Idreg).HasColumnName("IDReg");

                entity.Property(e => e.Region1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Region");
            });

            modelBuilder.Entity<VoteStatus>(entity =>
            {
                entity.HasKey(e => e.Idvn)
                    .HasName("PK__VoteStat__B87C0A44044B386E");

                entity.ToTable("VoteStatus");

                entity.Property(e => e.Idvn)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("IDVN");

                entity.Property(e => e.VoteState)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
