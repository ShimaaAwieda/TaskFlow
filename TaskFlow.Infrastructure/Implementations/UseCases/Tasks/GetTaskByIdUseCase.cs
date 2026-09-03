using TaskFlow.Application.DTOs;
using TaskFlow.Application.Exceptions;
using TaskFlow.Application.Interfaces.Services;
using TaskFlow.Application.Interfaces.UseCases.Tasks;
using TaskFlow.Domain.Interfaces;

namespace TaskFlow.Infrastructure.Implementations.UseCases.Tasks
{
    public class GetTaskByIdUseCase : IGetTaskByIdUseCase
    {
        private readonly ITaskItemRepository _taskItemRepository;
        private readonly ICurrentUserService _currentUserService;

        public GetTaskByIdUseCase(ITaskItemRepository taskItemRepository, ICurrentUserService currentUserService)
        {
            _taskItemRepository = taskItemRepository;
            _currentUserService = currentUserService;
        }
        public async Task<TaskDto> ExecuteAsync(Guid id)
        {
            var task = await _taskItemRepository.GetByIdAsync(id);

            if (task == null)
                throw new NotFoundException("Task not found");

            if (!_currentUserService.IsInRole("Admin") && _currentUserService.UserId != task.AssignedUserId)
                throw new ForbiddenException("You are not allowed to get this task");

            return new TaskDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                status = task.status,
                DueDate = task.DueDate,
                AssignedUserId = task.AssignedUserId
            };
        }
    }
}
