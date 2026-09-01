using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.Domain.Enums;

namespace TaskFlow.Application.DTOs
{
    public class TaskStatusDto
    {
        public Status isDone { get; set; }
    }
}
