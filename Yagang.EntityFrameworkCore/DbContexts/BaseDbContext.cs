using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Yagang.EntityFrameworkCore.Entities;

namespace Yagang.EntityFrameworkCore.DbContexts;
public abstract class BaseDbContext<TKey, TDateTime>(
    DbContextOptions options
    ) : DbContext(options)
    where TKey : IEquatable<TKey>
    where TDateTime : struct
{
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateBaseEntity();
        return base.SaveChangesAsync(cancellationToken);
    }

    public virtual BaseDbContext<TKey, TDateTime> UpdateBaseEntity()
    {
        var userId = GetNameIdentifier();

        var entityEntries = ChangeTracker.Entries()
            .Where(x => x.Entity is BaseEntity<TKey, TDateTime>)
            .Where(x => x.State is EntityState.Added or EntityState.Modified or EntityState.Deleted);

        foreach (var entityEntriy in entityEntries)
        {
            var entity = (BaseEntity<TKey, TDateTime>)entityEntriy.Entity;

            var now = GetUpdateDateTime();
            if (entityEntriy.State == EntityState.Added)
            {
                entity.CreationTime = now;
                entity.CreatorId = userId;
            }
            else if (entityEntriy.State == EntityState.Modified)
            {
                entity.UpdateTime = now;
                entity.UpdaterId = userId;
            }
            else if (entityEntriy.State == EntityState.Deleted)
            {
                entity.IsDeleted = true;
                entity.DeletionTime = now;
                entity.DeleterId = userId;
            }
        }

        return this;
    }

    public abstract TKey GetNameIdentifier();
    public abstract TDateTime GetUpdateDateTime();

}
