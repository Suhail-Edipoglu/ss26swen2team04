using SWEN2TourPlanner.Models;
using Microsoft.EntityFrameworkCore;

namespace SWEN2TourPlanner.Dal;

public class SWEN2TourPlannerDbContext : DbContext
{
    public DbSet<Tour> Tours { get; set; }
    public DbSet<Log> Logs { get; set; }
    public DbSet<User> Users { get; set; }

    public SWEN2TourPlannerDbContext(DbContextOptions<SWEN2TourPlannerDbContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Log>(entity =>
        {
            entity.HasKey(l => l.Id);
        });
        
        modelBuilder.Entity<Tour>(entity =>
        {
            entity.HasKey(t => t.Id);
            
            entity.HasMany(t => t.Logs)
                .WithOne(l => l.Tour)
                .HasForeignKey(l => l.TourId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.Id);
            entity.HasIndex(u => u.Username).IsUnique();
            
            entity.HasMany(u => u.Tours)
                .WithOne(t => t.User)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}