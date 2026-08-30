using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Enums;

namespace TaskFlow.Domain.Interfaces
{
    public interface ITaskItemRepository
    {
        Task<IEnumerable<TaskItem>> GetAllAsync(
            Guid? userId,
            int pageNumber = 1,
            int pageSize = 10,
            bool? isDone = null,
            Sort? sortBy = null
            );
        Task<TaskItem?> GetByIdAsync(Guid id);
        Task AddAsync(TaskItem item);
        void Update(TaskItem item);
        void Delete(Guid id);
    }
}
