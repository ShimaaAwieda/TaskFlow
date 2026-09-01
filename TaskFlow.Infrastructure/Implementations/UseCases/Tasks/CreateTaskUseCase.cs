using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.Application.DTOs;
using TaskFlow.Application.Exceptions;
using TaskFlow.Application.Interfaces.Services;
using TaskFlow.Application.Interfaces.UseCases.Tasks;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Interfaces;

namespace TaskFlow.Infrastructure.Implementations.UseCases.Tasks
{
    public class CreateTaskUseCase : ICreateTaskUseCase
    {
        private readonly ITaskItemRepository _taskItemRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;

        public CreateTaskUseCase(ITaskItemRepository taskItemRepository, IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
        {
            _taskItemRepository = taskItemRepository;
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
        }

        public async Task ExecuteAsync(CreateTaskDto dto)
        {
            Guid assignedUserId;

            if (_currentUserService.IsInRole("Admin"))
            {
                assignedUserId = dto.AssignedUserId
                    ?? throw new BadRequestException("Assigned user is required");
            }
            else
            {
                assignedUserId = _currentUserService.UserId;
            }

            var task = new TaskItem
            {
                Title = dto.Title,
                Description = dto.Description,
                DueDate = dto.DueDate,
                AssignedUserId = assignedUserId,
            };

            await _taskItemRepository.AddAsync(task);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
