using Employee_Management_System.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Employee_Management_System.Data
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Employee> Employees { get; set; }
    }
}
