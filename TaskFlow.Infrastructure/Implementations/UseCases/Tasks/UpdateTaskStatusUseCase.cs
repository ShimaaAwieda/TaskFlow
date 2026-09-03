using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.Application.DTOs;
using TaskFlow.Application.Exceptions;
using TaskFlow.Application.Interfaces.Services;
using TaskFlow.Application.Interfaces.UseCases.Tasks;
using TaskFlow.Domain.Interfaces;

namespace TaskFlow.Infrastructure.Implementations.UseCases.Tasks
{
    public class UpdateTaskStatusUseCase : IUpdateTaskStatusUseCase
    {
        private readonly ITaskItemRepository _taskItemRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;

        public UpdateTaskStatusUseCase(ITaskItemRepository taskItemRepository, IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
        {
            _taskItemRepository = taskItemRepository;
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
        }

        public async Task<TaskDto> ExecuteAsync(Guid id, TaskStatusDto dto)
        {
            var task = await _taskItemRepository.GetByIdAsync(id);

            if (task == null)
                throw new NotFoundException("Task not found");

            if (!_currentUserService.IsInRole("Admin") && _currentUserService.UserId != task.AssignedUserId)
                throw new ForbiddenException("You are not allowed to update this task");

            task.status = dto.status;

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
