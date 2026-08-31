using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.Domain.Enums;

namespace TaskFlow.Application.Interfaces.Services
{
    public interface IJwtService
    {
        public string GenerateToken(Guid userId, string email, Role role);
    }
}
