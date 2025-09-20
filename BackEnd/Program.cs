using Microsoft.EntityFrameworkCore;
using Old_School_Runescape_Project.Models;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

// Load environment variables from .env file (for local development)
Env.Load();

// Build connection string from environment variables for Supabase Session Pooler
var host = Environment.GetEnvironmentVariable("DB_SERVER");
var port = Environment.GetEnvironmentVariable("DB_PORT") ?? "5432";
var database = Environment.GetEnvironmentVariable("DB_NAME") ?? "postgres";
var username = Environment.GetEnvironmentVariable("DB_USER");
var password = Environment.GetEnvironmentVariable("DB_PASSWORD");

string connectionString;
if (host != null && username != null && password != null)
{
    connectionString = $"User Id={username};Password={password};Server={host};Port={port};Database={database}";
}
else
{
    throw new InvalidOperationException("Required database environment variables are not set.");
}

// Add DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
