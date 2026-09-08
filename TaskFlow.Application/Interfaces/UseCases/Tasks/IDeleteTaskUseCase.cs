using System;
using System.Collections.Generic;
using System.Text;

namespace TaskFlow.Application.Interfaces.UseCases.Tasks
{
    public interface IDeleteTaskUseCase
    {
        Task ExecuteAsync(Guid id);
    }
}
