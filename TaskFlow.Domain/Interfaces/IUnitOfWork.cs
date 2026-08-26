using System;
using System.Collections.Generic;
using System.Text;

namespace TaskFlow.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync();
    }
}
