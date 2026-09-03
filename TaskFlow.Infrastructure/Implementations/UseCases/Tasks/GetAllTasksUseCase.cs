using TaskFlow.Application.DTOs;
using TaskFlow.Application.Interfaces.Services;
using TaskFlow.Application.Interfaces.UseCases.Tasks;
using TaskFlow.Domain.Enums;
using TaskFlow.Domain.Interfaces;

namespace TaskFlow.Infrastructure.Implementations.UseCases.Tasks
{
    public class GetAllTasksUseCase : IGetAllTasksUseCase
    {
        private readonly ITaskItemRepository _taskItemRepository;
        private readonly ICurrentUserService _currentUserService;

        public GetAllTasksUseCase(ITaskItemRepository taskItemRepository, ICurrentUserService currentUserService)
        {
            _taskItemRepository = taskItemRepository;
            _currentUserService = currentUserService;
        }

        public async Task<IEnumerable<TaskDto>> ExecuteAsync(GetTasksDto dto)
        {
            if (dto.SortBy.HasValue && !dto.Order.HasValue)
                dto.Order = SortOrder.Ascending;

            Guid? userId = null;
            if (!_currentUserService.IsInRole("Admin"))
                userId = _currentUserService.UserId;

            var tasks = await _taskItemRepository.GetAllAsync(
                userId,
                dto.PageNumber,
                dto.PageSize,
                dto.status,
                dto.SortBy,
                dto.Order
                );

            return tasks.Select(t => new TaskDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                status = t.status,
                DueDate = t.DueDate,
                AssignedUserId = t.AssignedUserId
            });
        }
    }
}
