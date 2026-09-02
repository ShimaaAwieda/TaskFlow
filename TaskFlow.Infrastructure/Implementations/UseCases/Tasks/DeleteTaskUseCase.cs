using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.Application.Exceptions;
using TaskFlow.Application.Interfaces.Services;
using TaskFlow.Application.Interfaces.UseCases.Tasks;
using TaskFlow.Domain.Interfaces;

namespace TaskFlow.Infrastructure.Implementations.UseCases.Tasks
{
    public class DeleteTaskUseCase : IDeleteTaskUseCase
    {
        private readonly ITaskItemRepository _taskItemRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;

        public DeleteTaskUseCase(ITaskItemRepository taskItemRepository, IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
        {
            _taskItemRepository = taskItemRepository;
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
        }

        public async Task ExecuteAsync(Guid id)
        {
            var task = await _taskItemRepository.GetByIdAsync(id);

            if (task == null)
                throw new NotFoundException("Task not found");

            if (!_currentUserService.IsInRole("Admin") && _currentUserService.UserId != task.AssignedUserId)
                throw new UnauthorizedException("You are not allowed to delete this task");

            _taskItemRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
