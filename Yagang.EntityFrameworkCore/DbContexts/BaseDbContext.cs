using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;
using Yagang.EntityFrameworkCore.IEntities;

namespace Yagang.EntityFrameworkCore.DbContexts;
public class BaseDbContext<TKey>(
    DbContextOptions options,
    IServiceProvider serviceProvider
    ) : DbContext(options)
    where TKey : IEquatable<TKey>
{

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<Enum>().HaveConversion<string>().HaveMaxLength(50);
        base.ConfigureConventions(configurationBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateBaseEntity();
        return base.SaveChangesAsync(cancellationToken);
    }

    public virtual BaseDbContext<TKey> UpdateBaseEntity()
    {
        var userId = GetNameIdentifier();

        var entityEntries = ChangeTracker.Entries()
            .Where(x => x.Entity is IBaseEntity<TKey>)
            .Where(x => x.State is EntityState.Added or EntityState.Modified or EntityState.Deleted);

        foreach (var entityEntriy in entityEntries)
        {
            var entity = (IBaseEntity<TKey>)entityEntriy.Entity;
             
            if (entityEntriy.State == EntityState.Added)
            {
                entity.CreationTime = DateTimeOffset.UtcNow;
                entity.CreatorId = userId;
            }
            else if (entityEntriy.State == EntityState.Modified)
            {
                entity.UpdateTime = DateTimeOffset.UtcNow;
                entity.UpdaterId = userId;
            }
            else if (entityEntriy.State == EntityState.Deleted)
            {
                entity.IsDeleted = true;
                entity.DeletionTime = DateTimeOffset.UtcNow;
                entity.DeleterId = userId;
            }
        }

        return this;
    }

    public virtual TKey? GetNameIdentifier()
    {
        
        var nameIdentifier = serviceProvider.GetRequiredService<IHttpContextAccessor>().HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        return nameIdentifier switch
        {
            null => default,
            _ => typeof(TKey) switch
            {
                Type t when t == typeof(int) => (TKey)(object)int.Parse(nameIdentifier),
                Type t when t == typeof(long) => (TKey)(object)long.Parse(nameIdentifier),
                Type t when t == typeof(Guid) => (TKey)(object)Guid.Parse(nameIdentifier),
                Type t when t == typeof(string) => (TKey)(object)nameIdentifier,
                _ => throw new NotSupportedException($"Unsupported key type: {typeof(TKey).Name}")
            }
        };
    }
}
