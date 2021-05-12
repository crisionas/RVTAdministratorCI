using Microsoft.EntityFrameworkCore;
using RVT_DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.DBContexts
{
    public class SystemDBContext:DbContext
    {
        public SystemDBContext()
        {
        }

        public SystemDBContext(DbContextOptions<SystemDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Block> Blocks { get; set; }
        public virtual DbSet<FiscData> FiscData { get; set; }
        public virtual DbSet<IdvnAccount> IdvnAccounts { get; set; }
        public virtual DbSet<Party> Parties { get; set; }
        public virtual DbSet<Region> Regions { get; set; }
        public virtual DbSet<VoteStatus> VoteStatuses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-GDI15RS\\SQLEXPRESS;Database=SystemDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Region>()
                .HasMany<Block>(ad => ad.Blocks);

            modelBuilder.Entity<Party>()
                .HasMany<Block>(ad => ad.Blocks);

            modelBuilder.Entity<Region>()
                .HasMany<IdvnAccount>(ad => ad.IdvnAccounts);

            modelBuilder.Entity<IdvnAccount>()
                .HasOne(ep => ep.VoteStatus)
                .WithOne(b => b.IdvnAccount)
                .HasForeignKey<VoteStatus>(e=>e.Idvn);
        }

    }
}
