using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.Domain.Interfaces;

namespace TaskFlow.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IUserRepository Users { get; }
        public ITaskItemRepository TaskItems { get; }
        public UnitOfWork(ApplicationDbContext context, IUserRepository userRepository, ITaskItemRepository taskItemRepository)
        {
            _context = context;
            Users = userRepository;
            TaskItems = taskItemRepository;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
