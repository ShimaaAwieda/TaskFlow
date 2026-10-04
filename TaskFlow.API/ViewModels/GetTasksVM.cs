using TaskFlow.Domain.Enums;

namespace TaskFlow.API.ViewModels
{
    public class GetTasksVM
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public Status? Status { get; set; }
        public Sort? SortBy { get; set; }
        public SortOrder? Order { get; set; }
    }
}
