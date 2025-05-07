namespace Yagang.EntityFrameworkCore.Interfaces;

public interface ICreationAuditable<TKey, TDateTime>
    where TKey : IEquatable<TKey>
    where TDateTime : struct
{
    TDateTime CreationTime { get; set; }
    TKey CreatorId { get; set; }
}
