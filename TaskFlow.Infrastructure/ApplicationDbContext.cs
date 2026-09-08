using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Enums;

namespace TaskFlow.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<TaskItem> TaskItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = Guid.Parse("7f3b2c91-4d68-4a15-9e27-81c6f5b90342"),
                    Name = "Admin",
                    Email = "admin@gmail.com",
                    Password = "$2a$11$tcNLAG/B/Hu/4jqUmCrw3eBV6OsxWtSD8JLUhS6duobkNIGkgieWW",
                    Role = Role.Admin
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}
