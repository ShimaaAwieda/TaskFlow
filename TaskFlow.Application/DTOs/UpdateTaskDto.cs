using System;
using System.Collections.Generic;
using System.Text;

namespace TaskFlow.Application.DTOs
{
    public class UpdateTaskDto
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public bool isDone { get; set; }
        public DateTime? DueDate { get; set; }
        public Guid? AssignedUserId { get; set; }
    }
}
