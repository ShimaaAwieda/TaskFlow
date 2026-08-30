using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.Application.DTOs;
using TaskFlow.Application.Interfaces.Services;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Enums;
using TaskFlow.Domain.Interfaces;

namespace TaskFlow.Infrastructure.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskItemRepository _taskRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TaskService(ITaskItemRepository taskRepository, IUnitOfWork unitOfWork)
        {
            _taskRepository = taskRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<TaskDto>> GetAllAsync(int pageNumber = 1, int pageSize = 10, bool? isDone = null, Sort? sortBy = null)
        {
            // allowed to get (member - admin)

            var tasks = await _taskRepository.GetAllAsync(userId, pageNumber, pageSize, isDone, sortBy);

            return tasks.Select(tasks => new TaskDto
            {
                Id = tasks.Id,
                Title = tasks.Title,
                Description = tasks.Description,
                isDone = tasks.IsDone,
                DueDate = tasks.DueDate,
                AssignedUserId = tasks.AssignedUserId
            });
        }

        public async Task<TaskDto?> GetByIdAsync(Guid id)
        {
            var task = await _taskRepository.GetByIdAsync(id);

            if (task == null)
                throw new KeyNotFoundException("Task not Found");

            // allowed to get (member - admin)

            return new TaskDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                isDone = task.IsDone,
                DueDate = task.DueDate,
                AssignedUserId = task.AssignedUserId,
            };
        }

        public async Task AddAsync(CreateTaskDto dto)
        {
            var task = new TaskItem
            {
                Title = dto.Title,
                Description = dto.Description,
                DueDate = dto.DueDate,
                // AssignedUserId = dto.AssignedUserId
            };

            // AssignedUserID (member - admin)

            await _taskRepository.AddAsync(task);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(Guid id, UpdateTaskDto dto)
        {
            var task = await _taskRepository.GetByIdAsync(id);

            if (task == null)
                throw new KeyNotFoundException("Task not found");

            // allowed to update (member - admin)

            task.Title = dto.Title;
            task.Description = dto.Description;
            task.IsDone = dto.isDone;
            task.DueDate = dto.DueDate;
            // task.AssignedUserId = dto.AssignedUserId;

            _taskRepository.Update(task);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateStatusAsync(Guid id, TaskStatusDto dto)
        {
            var task = await _taskRepository.GetByIdAsync(id);

            if (task == null)
                throw new KeyNotFoundException("Task not found");

            // allowed to update (member - admin)

            task.IsDone = dto.isDone;
            _taskRepository.Update(task);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var task = await _taskRepository.GetByIdAsync(id);

            if (task == null)
                throw new KeyNotFoundException("Task not found");

            // allowed to delete (member - admin)

            _taskRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
        }

    }
}
