namespace Yagang.EntityFrameworkCore.Interfaces;

public interface ISoftDeletable<TKey, TDateTime>
    where TKey : IEquatable<TKey>
    where TDateTime : struct
{
    bool IsDeleted { get; set; }
    TDateTime? DeletionTime { get; set; }
    TKey? DeleterId { get; set; }
}