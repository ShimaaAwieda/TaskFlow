using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Enums;
using TaskFlow.Domain.Interfaces;

namespace TaskFlow.Infrastructure.Repositories
{
    public class TaskItemRepository : ITaskItemRepository
    {
        private readonly ApplicationDbContext _context;
        public TaskItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaskItem>> GetAllAsync(Guid? userId,int pageNumber, int pageSize, bool? isDone, Sort? sortBy)
        {
            IQueryable<TaskItem> query = _context.TaskItems;

            if(userId != null)
                query = query.Where(t => t.AssignedUserId == userId);

            if(isDone != null)
                query = query.Where(t => t.IsDone == isDone);

            if (sortBy == Sort.Title)
                query = query.OrderBy(t => t.Title);
            else if (sortBy == Sort.DueDate)
                query = query.OrderBy(t => t.DueDate);

            query = query.Skip((pageNumber - 1) * pageSize)
                         .Take(pageSize);

            return await query.ToListAsync();
        }
        
        public async Task<TaskItem?> GetByIdAsync(Guid id)
        {
            return await _context.TaskItems.FindAsync(id);
        }
        public async Task AddAsync(TaskItem item)
        {
            await _context.TaskItems.AddAsync(item);
        }

        public void Update(TaskItem item)
        {
            _context.TaskItems.Update(item);
        }

        public void Delete(Guid id)
        {
            var item = _context.TaskItems.Find(id);
            _context.TaskItems.Remove(item);
        }
    }
}
