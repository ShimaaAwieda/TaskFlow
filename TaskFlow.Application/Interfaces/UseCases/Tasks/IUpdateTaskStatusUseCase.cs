using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.Application.DTOs;

namespace TaskFlow.Application.Interfaces.UseCases.Tasks
{
    public interface IUpdateTaskStatusUseCase
    {
        Task<TaskDto> ExecuteAsync(Guid id, TaskStatusDto dto);
    }
}
