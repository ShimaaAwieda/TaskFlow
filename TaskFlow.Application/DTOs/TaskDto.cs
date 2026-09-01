using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.Domain.Enums;

namespace TaskFlow.Application.DTOs
{
    public class TaskDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public Status isDone { get; set; }
        public DateTime? DueDate { get; set; }
        public Guid AssignedUserId { get; set; }
    }
}
