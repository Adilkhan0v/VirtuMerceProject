using VirtuMerce.Dal.Entities.Abstract;

namespace VirtuMerce.Dal.Entities;

public class ProductEntity : BaseEntity
{
    public string Title { get; set; }
    public string Details { get; set; }
    public float Price { get; set; }
    public UserEntity User { get; set; }
}