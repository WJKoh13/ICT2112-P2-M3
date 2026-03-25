using Microsoft.EntityFrameworkCore;

namespace ProRental.Data.UnitOfWork;

public partial class AppDbContext
{
    public AppDbContext(DbContextOptions options)
        : base(options)
    {
    }
}