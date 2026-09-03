using TaskFlow.Application.DTOs;
using TaskFlow.Application.Exceptions;
using TaskFlow.Application.Interfaces.Services;
using TaskFlow.Application.Interfaces.UseCases.Tasks;
using TaskFlow.Domain.Interfaces;

namespace TaskFlow.Infrastructure.Implementations.UseCases.Tasks
{
    public class UpdateTaskUseCase : IUpdateTaskUseCase
    {
        private readonly ITaskItemRepository _taskItemRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;

        public UpdateTaskUseCase(ITaskItemRepository taskItemRepository, IUserRepository userRepository, IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
        {
            _taskItemRepository = taskItemRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
        }

        public async Task<TaskDto> ExecuteAsync(Guid id, UpdateTaskDto dto)
        {
            var task = await _taskItemRepository.GetByIdAsync(id);

            if (task == null)
                throw new NotFoundException("Task not found");

            if (!_currentUserService.IsInRole("Admin") && _currentUserService.UserId != task.AssignedUserId)
                throw new ForbiddenException("You are not allowed to update this task");

            task.Title = dto.Title;
            task.Description = dto.Description;
            task.status = dto.status;
            task.DueDate = dto.DueDate;

            if (_currentUserService.IsInRole("Admin"))
            {
                var assignedUserId = dto.AssignedUserId
                    ?? throw new BadRequestException("Assigned user is required");

                var assignedUser = await _userRepository.FindByIdAsync(assignedUserId);

                if (assignedUser == null)
                    throw new NotFoundException("Assigned user not found");

                task.AssignedUserId = assignedUserId;
            }

            _taskItemRepository.Update(task);
            await _unitOfWork.SaveChangesAsync();

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
