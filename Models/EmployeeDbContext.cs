using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Schemas
            modelBuilder.HasDefaultSchema("public"); // fallback


            modelBuilder.Entity<EmployeeRegister>().ToTable("EmployeeRegister", schema: "Employee");
            modelBuilder.Entity<Department>().ToTable("Department", schema: "Core");
            modelBuilder.Entity<Region>().ToTable("Region", schema: "Core");

            modelBuilder.Entity<LoginPage>().ToTable("LoginPage", schema: "Jwt");


            modelBuilder.Entity<Sale>().ToTable("Sale", schema: "Sale");
            modelBuilder.Entity<Item>().ToTable("Item", schema: "Sale");


            modelBuilder.Entity<Calender>().ToTable("Calender", schema: "Calender");

            modelBuilder.Entity<Analysis>().ToTable("Analysis", schema: "Core");
            modelBuilder.Entity<Career>().ToTable("Career", schema: "Employee");




        }
    }

}
