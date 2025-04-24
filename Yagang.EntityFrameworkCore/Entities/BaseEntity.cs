using Yagang.EntityFrameworkCore.IEntities;

namespace Yagang.EntityFrameworkCore.Entities;

public class BaseEntity<T> : IBaseEntity<T>
{
    public virtual required T Id { get; set; }
}
