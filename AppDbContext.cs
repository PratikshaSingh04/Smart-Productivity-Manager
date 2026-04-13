using Microsoft.EntityFrameworkCore;
using SmartProductivityManager.Models;

namespace SmartProductivityManager.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<TaskItem> Tasks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("Data Source=tasks.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskItem>()
                .Property(t => t.Priority)
                .HasConversion<string>();

            modelBuilder.Entity<TaskItem>()
                .Property(t => t.Status)
                .HasConversion<string>();
        }
    }
}