using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using TaskFlow.Application.Interfaces.Services;
using TaskFlow.Application.Interfaces.UseCases.Auth;
using TaskFlow.Application.Interfaces.UseCases.Tasks;
using TaskFlow.Application.Settings;
using TaskFlow.Domain.Interfaces;
using TaskFlow.Infrastructure.Implementations.Services;
using TaskFlow.Infrastructure.Implementations.UseCases.Auth;
using TaskFlow.Infrastructure.Implementations.UseCases.Tasks;
using TaskFlow.Infrastructure.Repositories;

namespace TaskFlow.API.Extensions
{
    public static class ServiceExtensions
    {
        public static void RegisterUseCases(this IServiceCollection services)
        {
            services.AddScoped<IUserRegisterUseCase, UserRegisterUseCase>();
            services.AddScoped<IUserLoginUseCase, UserLoginUseCase>();
            services.AddScoped<ICreateTaskUseCase, CreateTaskUseCase>();
            services.AddScoped<IGetAllTasksUseCase, GetAllTasksUseCase>();
        }
        
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
        }

        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITaskItemRepository, TaskItemRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static void RegisterAuthentication(this IServiceCollection services, JwtSettings jwtSettings)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = jwtSettings.ValidateIssuer,
                    ValidateAudience = jwtSettings.ValidateAudience,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    RequireExpirationTime = true,

                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),
                    RoleClaimType = ClaimTypes.Role
                };
            });
        }
    }
}
