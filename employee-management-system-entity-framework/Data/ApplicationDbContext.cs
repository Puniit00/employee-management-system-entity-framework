using EmployeeManagementSystemEntityFramework.Model;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Model;

public class ApplicationDbContext: DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Employees> Employees { get; set; }
    public DbSet<Goals> Goals { get; set; }
}
