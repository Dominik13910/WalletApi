using System;
using System.Reflection;
using System.Text;
using Azure.Messaging;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NLog.Web;
using webapi.Authentications;
using webapi.Dto.Users;
using webapi.Dto.Wallet;
using webapi.HttpClients;
using webapi.Interfaces;
using webapi.Mappers;
using webapi.Middleware;
using webapi.Models;
using webapi.Seeders;
using webapi.Services;
using webapi.Validations;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers().AddFluentValidation();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var aurhenticationSetting = new AuthenticationSetting();
builder.Configuration.GetSection("Authentication").Bind(aurhenticationSetting);
builder.Services.AddSingleton(aurhenticationSetting);
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = "Bearer";
    option.DefaultScheme = "Bearer";
    option.DefaultChallengeScheme = "Bearer";

}).AddJwtBearer(cfg =>
{
    cfg.RequireHttpsMetadata = false;
    cfg.SaveToken = true;
    cfg.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = aurhenticationSetting.JwtIssuer,
        ValidAudience = aurhenticationSetting.JwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(aurhenticationSetting.JwtKey)),



    };
});

builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(
    option => option.UseSqlServer(builder.Configuration.GetConnectionString("MyBoardsConnectionString")));
builder.Services.AddScoped<RolesSeeder>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<UserInterface, UserServices>();
builder.Services.AddScoped<IRolesInterface, RolesServices>();
builder.Services.AddScoped<IWalletInterface, WalletServices>();

builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Logging.ClearProviders();
builder.Host.UseNLog();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<IValidator<CreateUserDto>, CreateUserValidator>();
var app = builder.Build();

var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<RolesSeeder>();
seeder.Seed();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

    app.UseSwagger();
    app.UseSwaggerUI();

}


app.UseAuthentication();

app.UseHttpsRedirection();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
