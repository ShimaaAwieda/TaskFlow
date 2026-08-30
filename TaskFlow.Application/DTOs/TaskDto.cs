using System;
using System.Collections.Generic;
using System.Text;

namespace TaskFlow.Application.DTOs
{
    public class TaskDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public bool isDone { get; set; }
        public DateTime? DueDate { get; set; }
        public Guid AssignedUserId { get; set; }
    }
}
