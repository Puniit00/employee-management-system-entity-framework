using EmployeeManagementSystemEntityFramework.Model;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext: DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Employees> Employees { get; set; }
}
