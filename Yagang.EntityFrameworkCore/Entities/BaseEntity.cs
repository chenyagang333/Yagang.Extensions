using Yagang.EntityFrameworkCore.IEntities;

namespace Yagang.EntityFrameworkCore.Entities;

public class BaseEntity<TKey> : IBaseEntity<TKey> where TKey : IEquatable<TKey>
{
    public virtual TKey Id { get; set; }
    public bool IsDeleted { get; set; }
    public DateTimeOffset CreationTime { get; set; }
    public DateTimeOffset UpdateTime { get; set; }
    public DateTimeOffset? DeletionTime { get; set; }
    public TKey CreatorId { get; set; }
    public TKey UpdaterId { get; set; }
    public TKey? DeleterId { get; set; }
}
