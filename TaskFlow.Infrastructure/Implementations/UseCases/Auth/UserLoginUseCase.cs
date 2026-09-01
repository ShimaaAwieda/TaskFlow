using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.Application.DTOs;
using TaskFlow.Application.Interfaces.Services;
using TaskFlow.Application.Interfaces.UseCases.Auth;
using TaskFlow.Application.Exceptions;
using TaskFlow.Domain.Interfaces;

namespace TaskFlow.Infrastructure.Implementations.UseCases.Auth
{
    public class UserLoginUseCase : IUserLoginUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;

        public UserLoginUseCase(IUserRepository userRepository, IJwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }
        public async Task<string> ExecuteAsync(LoginDto dto)
        {
            var user = await _userRepository.FindByEmailAsync(dto.Email);

            if (user == null)
                throw new UnauthorizedException("Invalid Email or Password");

            if(user.Password != dto.Password)
                throw new UnauthorizedException("Invalid Email or Password");

            var token = _jwtService.GenerateToken(
                user.Id,
                user.Email,
                user.Role
                );

            return token;
        }
    }
}
