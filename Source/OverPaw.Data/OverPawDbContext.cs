namespace OverPaw.Data
{
    using System.Reflection;
    using Microsoft.EntityFrameworkCore;

    using Data.Models;

    public class OverPawDbContext : DbContext
    {
        public OverPawDbContext(DbContextOptions<OverPawDbContext> options) 
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
