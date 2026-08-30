using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.Application.DTOs;
using TaskFlow.Domain.Enums;

namespace TaskFlow.Application.Interfaces.Services
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskDto>> GetAllAsync(
            int pageNumber = 1,
            int pageSize = 10,
            bool? isDone = null,
            Sort? sortBy = null
            );

        Task<TaskDto?> GetByIdAsync(Guid id);
        Task AddAsync(CreateTaskDto dto);
        Task UpdateAsync(Guid id, UpdateTaskDto dto);
        Task UpdateStatusAsync(Guid id, TaskStatusDto dto);
        Task DeleteAsync(Guid id);
    }
}
