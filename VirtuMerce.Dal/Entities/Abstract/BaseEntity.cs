namespace VirtuMerce.Dal.Entities.Abstract;

public class BaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreationDate { get; set; } = DateTime.UtcNow;
    public bool IsDeleted { get; set; } = true;
}