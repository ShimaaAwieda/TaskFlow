using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.Application.DTOs;
using TaskFlow.Domain.Enums;

namespace TaskFlow.Application.Interfaces.UseCases.Tasks
{
    public interface IGetAllTasksUseCase
    {
        Task<IEnumerable<TaskDto>> ExecuteAsync(GetTasksDto dto);
    }
}
