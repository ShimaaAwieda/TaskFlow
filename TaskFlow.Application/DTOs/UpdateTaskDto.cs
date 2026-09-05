using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TaskFlow.Domain.Enums;

namespace TaskFlow.Application.DTOs
{
    public class UpdateTaskDto
    {
        [Required]
        public string Title { get; set; }
        public string? Description { get; set; }
        public Status status { get; set; }
        public DateTime? DueDate { get; set; }
        public Guid? AssignedUserId { get; set; }
    }
}
