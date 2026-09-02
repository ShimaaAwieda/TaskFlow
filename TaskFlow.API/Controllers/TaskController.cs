using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.Application.DTOs;
using TaskFlow.Application.Interfaces.UseCases.Tasks;

namespace TaskFlow.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ICreateTaskUseCase _createTaskUseCase;
        private readonly IGetAllTasksUseCase _getAllTasksUseCase;
        private readonly IGetTaskByIdUseCase _getTaskByIdUseCase;
        private readonly IUpdateTaskUseCase _updateTaskUseCase;
        private readonly IUpdateTaskStatusUseCase _updateTaskStatusUseCase;
        private readonly IDeleteTaskUseCase _deleteTaskUseCase;

        public TaskController(ICreateTaskUseCase createTaskUseCase, IGetAllTasksUseCase getAllTasksUseCase, IGetTaskByIdUseCase getTaskByIdUseCase, IUpdateTaskUseCase updateTaskUseCase, IUpdateTaskStatusUseCase updateTaskStatusUseCase, IDeleteTaskUseCase deleteTaskUseCase)
        {
            _createTaskUseCase = createTaskUseCase;
            _getAllTasksUseCase = getAllTasksUseCase;
            _getTaskByIdUseCase = getTaskByIdUseCase;
            _updateTaskUseCase = updateTaskUseCase;
            _updateTaskStatusUseCase = updateTaskStatusUseCase;
            _deleteTaskUseCase = deleteTaskUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTaskDto dto)
        {
            await _createTaskUseCase.ExecuteAsync(dto);
            return Created();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetTasksDto dto)
        {
            var tasks = await _getAllTasksUseCase.ExecuteAsync(dto);
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var task = await _getTaskByIdUseCase.ExecuteAsync(id);
            return Ok(task);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateTaskDto dto)
        {
            var task = await _updateTaskUseCase.ExecuteAsync(id, dto);
            return Ok(task);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateStatus(Guid id, TaskStatusDto dto)
        {
            var task = await _updateTaskStatusUseCase.ExecuteAsync(id, dto);
            return Ok(task);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _deleteTaskUseCase.ExecuteAsync(id);
            return NoContent();
        }
    }
}
