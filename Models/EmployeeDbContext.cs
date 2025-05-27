using Microsoft.EntityFrameworkCore;

namespace EmployeeHierachy12345.Models
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options)
        {

        }
        public DbSet<EmployeeRegister> EmployeeRegister { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Region> Region { get; set; }
        public DbSet<LoginPage> LoginPage { get; set; }
        public DbSet<Sale> Sale { get; set; }
        public DbSet<Calender> Calender { get; set; }
        public DbSet<Analysis> Analysis { get; set; }
        public DbSet<Career> Career { get; set; }
        public DbSet<Item> Item { get; set; }


    }

}
