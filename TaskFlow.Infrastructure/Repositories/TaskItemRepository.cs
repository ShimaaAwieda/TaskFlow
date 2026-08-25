using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.Domain.Entities;
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

        // *****
        public async Task<IEnumerable<TaskItem>> GetAllAsync(int pageNumber, int pageSize, bool? isDone, string? sortBy)
        {
            IQueryable<TaskItem> query = _context.TaskItems;

            if(isDone != null)
                query = query.Where(t => t.IsDone == isDone);

            if (sortBy == "title")
                query = query.OrderBy(t => t.Title);
            else if (sortBy == "dueDate")
                query = query.OrderBy(t => t.DueDate);

            query = query.Skip((pageNumber - 1) * pageSize)
                         .Take(pageSize);

            return await query.ToListAsync();
        }
        
        public async Task<TaskItem?> FindByIdAsync(Guid id)
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

        public void Delete(int id)
        {
            var item = _context.TaskItems.Find(id);
            _context.TaskItems.Remove(item);
        }
    }
}
