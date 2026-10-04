using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.Application.DTOs;

namespace TaskFlow.Application.Interfaces.UseCases.Tasks
{
    public interface ICreateTaskUseCase
    {
        Task ExecuteAsync(CreateTaskDto dto);
    }
}
