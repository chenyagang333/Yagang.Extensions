namespace Yagang.EntityFrameworkCore.Entities;

public class BaseEntityOfGuid : BaseEntity<Guid>
{
    public override required Guid Id { get; set; } = Guid.CreateVersion7();
}
