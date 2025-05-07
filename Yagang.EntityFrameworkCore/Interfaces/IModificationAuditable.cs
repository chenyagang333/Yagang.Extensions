namespace Yagang.EntityFrameworkCore.Interfaces;

public interface IModificationAuditable<TKey, TDateTime>
    where TKey : IEquatable<TKey>
    where TDateTime : struct
{
    TDateTime UpdateTime { get; set; }
    TKey UpdaterId { get; set; }
}