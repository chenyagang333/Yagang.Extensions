using Yagang.EntityFrameworkCore.Interfaces;

namespace Yagang.EntityFrameworkCore.Entities;

public class BaseEntity<TKey, TDateTime> : IBaseEntity<TKey>, ICreationAuditable<TKey, TDateTime>, IModificationAuditable<TKey, TDateTime>, ISoftDeletable<TKey, TDateTime>
    where TKey : IEquatable<TKey>
    where TDateTime : struct
{
    public virtual TKey Id { get; set; }
    public TDateTime CreationTime { get; set; }
    public TKey CreatorId { get; set; }
    public TDateTime UpdateTime { get; set; }
    public TKey UpdaterId { get; set; }
    public TDateTime? DeletionTime { get; set; }
    public TKey? DeleterId { get; set; }
    public bool IsDeleted { get; set; }
}
