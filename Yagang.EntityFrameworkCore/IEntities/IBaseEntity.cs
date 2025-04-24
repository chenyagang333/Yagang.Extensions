namespace Yagang.EntityFrameworkCore.IEntities;

public interface IBaseEntity<T>
{
    T Id { get; }
}
