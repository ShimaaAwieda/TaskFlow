using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.Application.DTOs;

namespace TaskFlow.Application.Interfaces.UseCases.Tasks
{
    public interface IUpdateTaskUseCase
    {
        Task<TaskDto> ExecuteAsync(Guid id, UpdateTaskDto dto);
    }
}
