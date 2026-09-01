using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.Application.DTOs;
using TaskFlow.Application.Interfaces.UseCases.Auth;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Enums;
using TaskFlow.Domain.Exceptions;
using TaskFlow.Domain.Interfaces;

namespace TaskFlow.Infrastructure.Implementations.UseCases.Auth
{
    public class UserRegisterUseCase : IUserRegisterUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserRegisterUseCase(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task RegisterAsync(RegisterDto dto)
        {
            var existingUser = await _userRepository.FindByEmailAsync(dto.Email);

            if (existingUser != null)
                throw new ConflictException("Email already exists");

            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Email = dto.Email,
                Password = dto.Password,
                Role = Role.Member
            };
            await _userRepository.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
