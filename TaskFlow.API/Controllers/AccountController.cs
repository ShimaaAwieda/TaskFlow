using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.API.ViewModels;
using TaskFlow.Application.DTOs;
using TaskFlow.Application.Interfaces.UseCases.Auth;

namespace TaskFlow.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserRegisterUseCase _userRegisterUseCase;
        private readonly IUserLoginUseCase _userLoginUseCase;

        public AccountController(IUserRegisterUseCase userRegisterUseCase, IUserLoginUseCase userLoginUseCase)
        {
            _userRegisterUseCase = userRegisterUseCase;
            _userLoginUseCase = userLoginUseCase;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterVM vm)
        {
            var dto = new RegisterDto
            {
                Name = vm.Name,
                Email = vm.Email,
                Password = vm.Password
            };

            await _userRegisterUseCase.ExecuteAsync(dto);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginVM vm)
        {
            var dto = new LoginDto
            {
                Email = vm.Email,
                Password = vm.Password
            };

            var token = await _userLoginUseCase.ExecuteAsync(dto);
            return Ok(token);
        }
    }
}
