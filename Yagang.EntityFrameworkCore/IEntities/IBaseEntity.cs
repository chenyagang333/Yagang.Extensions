namespace Yagang.EntityFrameworkCore.IEntities;

public interface IBaseEntity<TKey> where TKey : IEquatable<TKey>
{
    TKey Id { get; set; }
    bool IsDeleted { get; set; }
    DateTimeOffset CreationTime { get; set; }
    DateTimeOffset UpdateTime { get; set; }
    DateTimeOffset? DeletionTime { get; set; }
    TKey CreatorId { get; set; }
    TKey UpdaterId { get; set; }
    TKey? DeleterId { get; set; }
}
