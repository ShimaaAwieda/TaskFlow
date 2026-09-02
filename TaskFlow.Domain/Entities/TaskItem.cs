using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.Domain.Enums;

namespace TaskFlow.Domain.Entities
{
    public class TaskItem
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public Status status { get; set; } = Status.New;
        public DateTime? DueDate { get; set; }
        public Guid AssignedUserId { get; set; }
        public User AssignedUser { get; set; }
    }
}
