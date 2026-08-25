using System;
using System.Collections.Generic;
using System.Text;

namespace TaskFlow.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        ITaskItemRepository TaskItems { get; }
        Task<int> SaveChangesAsync();
    }
}
