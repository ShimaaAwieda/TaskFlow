using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.Domain.Entities;

namespace TaskFlow.Infrastructure.Configurations
{
    public class TaskItemConfiguration : IEntityTypeConfiguration<TaskItem>
    {
        public void Configure(EntityTypeBuilder<TaskItem> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Title).IsRequired().HasMaxLength(100);
            builder.Property(t => t.Description).HasMaxLength(250);
            builder.Property(t => t.IsDone).HasDefaultValue(false);
            builder.HasOne(t => t.AssignedUser)
                .WithMany(u => u.TaskItems)
                .HasForeignKey(t => t.AssignedUserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
