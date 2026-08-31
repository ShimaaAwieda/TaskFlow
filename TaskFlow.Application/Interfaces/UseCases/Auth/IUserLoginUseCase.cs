using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.Application.DTOs;

namespace TaskFlow.Application.Interfaces.UseCases.Auth
{
    public interface IUserLoginUseCase
    {
        Task<string> LoginAsync(LoginDto dto);
    }
}
