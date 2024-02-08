﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Stories.Infrastructure.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Story> Stories { get; set; }
    public DbSet<Vote> Votes { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Story>(entity =>
        {
            entity.ToTable("Stories");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(255);
            entity.Property(e => e.Description).IsRequired().HasMaxLength(250);
            entity.Property(e => e.Department).HasMaxLength(100);

        });

        modelBuilder.Entity<Vote>(entity =>
        {
            entity.ToTable("Votes");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.VoteValue).IsRequired();
            entity.HasOne(e => e.Story).WithMany(s => s.Votes).HasForeignKey(e => e.IdStory);
            entity.HasOne(e => e.User).WithMany(u => u.Votes).HasForeignKey(e => e.IdUser);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("Users");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(255);
        });
    }
}
