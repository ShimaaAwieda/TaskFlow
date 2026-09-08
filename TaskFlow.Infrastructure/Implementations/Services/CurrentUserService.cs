using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskFlow.Application.Exceptions;
using TaskFlow.Application.Interfaces.Services;

namespace TaskFlow.Infrastructure.Implementations.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid UserId
        {
            get
            {
                var claim = _httpContextAccessor.HttpContext?.User.FindFirstValue(JwtRegisteredClaimNames.Sub);

                if (string.IsNullOrEmpty(claim) || !Guid.TryParse(claim, out var userId))
                    throw new UnauthorizedException("User ID not found in token");

                return userId;
            }
        }

        public bool IsAuthenticated =>
            _httpContextAccessor.HttpContext?.User.Identity?.IsAuthenticated ?? false;

        public bool IsInRole(string role) =>
            _httpContextAccessor.HttpContext?.User.IsInRole(role) ?? false;
    }
}
