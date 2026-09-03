using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.Domain.Enums;

namespace TaskFlow.Application.DTOs
{
    public class GetTasksDto
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public Status? status { get; set; }
        public Sort? SortBy { get; set; }
        public SortOrder? Order { get; set; }
    }
}
