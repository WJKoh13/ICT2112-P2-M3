using Microsoft.EntityFrameworkCore;

namespace ProRental.Data.UnitOfWork;

public class RuntimeAppDbContext : AppDbContext
{
    public RuntimeAppDbContext(DbContextOptions<RuntimeAppDbContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }
}