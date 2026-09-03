using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using TaskFlow.API.Extensions;
using TaskFlow.API.Middlewares;
using TaskFlow.Application.Settings;
using TaskFlow.Domain.Interfaces;
using TaskFlow.Infrastructure;
using TaskFlow.Infrastructure.Repositories;


var builder = WebApplication.CreateBuilder(args);

Env.Load();
builder.Configuration.AddEnvironmentVariables();

builder.Services.AddHttpContextAccessor();

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>() ?? throw new Exception("Jwt Settings are missing");
builder.Services.RegisterAuthentication(jwtSettings);

builder.Services.RegisterRepositories();
builder.Services.RegisterServices();
builder.Services.RegisterUseCases();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<LoggingMiddleware>();
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();