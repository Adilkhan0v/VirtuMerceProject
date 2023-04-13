using Microsoft.EntityFrameworkCore;
using VirtuMerce.Dal.Entities;

namespace VirtuMerce.Dal;

public class ApplicationContext:DbContext
{
    public ApplicationContext(DbContextOptions options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<UserEntity> Users { get; set; }
    public DbSet<CategoryEntity> Category { get; set; }
}
