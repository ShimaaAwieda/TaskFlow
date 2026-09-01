using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.Domain.Interfaces;

namespace TaskFlow.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
   
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
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
