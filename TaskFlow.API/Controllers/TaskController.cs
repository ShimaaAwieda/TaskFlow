using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.API.ViewModels;
using TaskFlow.Application.DTOs;
using TaskFlow.Application.Interfaces.UseCases.Tasks;

namespace TaskFlow.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TaskController : ControllerBase
    {
        private readonly ICreateTaskUseCase _createTaskUseCase;
        private readonly IGetAllTasksUseCase _getAllTasksUseCase;
        private readonly IGetTaskByIdUseCase _getTaskByIdUseCase;
        private readonly IUpdateTaskUseCase _updateTaskUseCase;
        private readonly IDeleteTaskUseCase _deleteTaskUseCase;

        public TaskController(ICreateTaskUseCase createTaskUseCase, IGetAllTasksUseCase getAllTasksUseCase, IGetTaskByIdUseCase getTaskByIdUseCase, IUpdateTaskUseCase updateTaskUseCase, IDeleteTaskUseCase deleteTaskUseCase)
        {
            _createTaskUseCase = createTaskUseCase;
            _getAllTasksUseCase = getAllTasksUseCase;
            _getTaskByIdUseCase = getTaskByIdUseCase;
            _updateTaskUseCase = updateTaskUseCase;
            _deleteTaskUseCase = deleteTaskUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTaskVM vm)
        {
            var dto = new CreateTaskDto
            {
                Title = vm.Title,
                Description = vm.Description,
                DueDate = vm.DueDate,
                AssignedUserId = vm.AssignedUserId
            };

            await _createTaskUseCase.ExecuteAsync(dto);
            return Created();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetTasksVM vm)
        {
            var dto = new GetTasksDto
            {
                PageNumber = vm.PageNumber,
                PageSize = vm.PageSize,
                Status = vm.Status,
                SortBy = vm.SortBy,
                Order = vm.Order
            };

            var tasks = await _getAllTasksUseCase.ExecuteAsync(dto);
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var task = await _getTaskByIdUseCase.ExecuteAsync(id);
            return Ok(task);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateTaskVM vm)
        {
            var dto = new UpdateTaskDto
            {
                Title = vm.Title,
                Description = vm.Description,
                Status = vm.Status,
                DueDate = vm.DueDate,
                AssignedUserId = vm.AssignedUserId
            };

            var task = await _updateTaskUseCase.ExecuteAsync(id, dto);
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
