using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyStore.Business.Entities;

namespace MyStore.Business
{
    public class MyStoreContext : DbContext
    {
        public MyStoreContext() { }

        public MyStoreContext(DbContextOptions<MyStoreContext> options) : base(options) { }

        public virtual DbSet<AccountMember> AccountMembers { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=MyStoreDB.db");
            }
        }

        private string GetConnectionString()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .Build();
            return configuration["ConnectionStrings:DefaultConnectionString"] 
                ?? "Server=localhost;Database=MyStoreDB;Trusted_Connection=True;TrustServerCertificate=True;";
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryID);
        }
    }
}
