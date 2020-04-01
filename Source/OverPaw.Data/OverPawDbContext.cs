namespace OverPaw.Data
{
    using System;
    using System.Reflection;
    using Microsoft.EntityFrameworkCore;
    using OverPaw.Data.Models;

    public class OverPawDbContext : DbContext
    {
        public OverPawDbContext(DbContextOptions<OverPawDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<User>().ToTable("Users");

            // modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
