using Microsoft.EntityFrameworkCore;
using RealtyApp.Core.Domain.Common;
using RealtyApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RealtyApp.Infrastructure.Persistence.Contexts
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        public DbSet<ImmovableAsset> ImmovableAssets { get; set; }
        public DbSet<ImmovableAssetType> ImmovableAssetTypes { get; set; }
        public DbSet<Improvement> Improvements { get; set; }
        public DbSet<SellType> SellTypes { get; set; }
        public DbSet<Improvement_Immovable> Improvement_Immovables { get; set; }
        public DbSet<FavoriteImmovable> FavoriteImmovables { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = DateTime.Now;
                        entry.Entity.CreatedBy = "DefaultAppUser";
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModified = DateTime.Now;
                        entry.Entity.LastModifiedBy = "DefaultAppUser";
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //FLUENT API

            #region Tables

            modelBuilder.Entity<ImmovableAsset>()
                .ToTable("ImmovableAssets");

            modelBuilder.Entity<ImmovableAssetType>()
                .ToTable("ImmovableAssetTypes");

            modelBuilder.Entity<Improvement>()
                .ToTable("Improvements");

            modelBuilder.Entity<SellType>()
                .ToTable("SellTypes");

            modelBuilder.Entity<Improvement_Immovable>()
                .ToTable("Improvement_Immovables");

            modelBuilder.Entity<FavoriteImmovable>()
                .ToTable("FavoriteImmovables");
            #endregion

            #region Primary keys
            modelBuilder.Entity<ImmovableAsset>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<ImmovableAssetType>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Improvement>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<SellType>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Improvement_Immovable>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<FavoriteImmovable>()
                .HasKey(x => x.Id);
            #endregion

            #region Relationships

            #region ImmovableAsset
            modelBuilder.Entity<ImmovableAsset>()
                .HasOne<ImmovableAssetType>(x => x.ImmovableAssetType)
                .WithMany(x => x.ImmovableAssets)
                .HasForeignKey(x => x.ImmovableAssetTypeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ImmovableAsset>()
                .HasOne<SellType>(x => x.SellType)
                .WithMany(x => x.ImmovableAssets)
                .HasForeignKey(x => x.SellTypeId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region Improvement_Immovable
            modelBuilder.Entity<Improvement_Immovable>()
                .HasOne<Improvement>(x => x.Improvement)
                .WithMany(x => x.Improvement_Immovables)
                .HasForeignKey(x => x.ImprovementId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Improvement_Immovable>()
                .HasOne<ImmovableAsset>(x => x.ImmovableAsset)
                .WithMany(x => x.Improvement_Immovables)
                .HasForeignKey(x => x.ImmovableAssetId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region FavoriteImmovable
            modelBuilder.Entity<FavoriteImmovable>()
                .HasOne<ImmovableAsset>(x => x.ImmovableAsset)
                .WithMany(x => x.FavoriteImmovables)
                .HasForeignKey(x => x.ImmovableAssetId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion

            #endregion

            #region Property configurations

            #region ImmovableAssets

            modelBuilder.Entity<ImmovableAsset>().
                Property(x => x.Description)
                .HasMaxLength(int.MaxValue);

            #endregion

            #endregion
        }

    }
}
