using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace RVT_DataLayer.Entities
{
    public partial class SFBDContext : DbContext
    {
        public SFBDContext()
        {
        }

        public SFBDContext(DbContextOptions<SFBDContext> options)
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
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost;Database=SFBD;Trusted_Connection=True;");
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

                entity.Property(e => e.Idbd)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.PreviousHash)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.HasOne(d => d.PartyChoosedNavigation)
                    .WithMany(p => p.Blocks)
                    .HasForeignKey(d => d.PartyChoosed)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Blocks_Parties");

                entity.HasOne(d => d.RegionChoosedNavigation)
                    .WithMany(p => p.Blocks)
                    .HasForeignKey(d => d.RegionChoosed)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Blocks_Regions");
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
                entity.HasKey(e => e.Idvn);

                entity.ToTable("Idvn_accounts");

                entity.Property(e => e.Idvn)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("IDVN");

                entity.Property(e => e.BirthDate)
                    .HasColumnType("date")
                    .HasColumnName("Birth_date");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .HasMaxLength(25)
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

                entity.HasOne(d => d.IdvnNavigation)
                    .WithOne(p => p.IdvnAccount)
                    .HasForeignKey<IdvnAccount>(d => d.Idvn)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Idvn_accounts_VoteStatus");

                entity.HasOne(d => d.RegionNavigation)
                    .WithMany(p => p.IdvnAccounts)
                    .HasForeignKey(d => d.Region)
                    .HasConstraintName("FK_Idvn_accounts_Regions");
            });

            modelBuilder.Entity<Party>(entity =>
            {
                entity.HasKey(e => e.Idpart);

                entity.Property(e => e.Idpart).HasColumnName("IDPart");

                entity.Property(e => e.Color)
                    .HasMaxLength(255)
                    .IsUnicode(false);

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
                entity.HasKey(e => e.Idvn);

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
