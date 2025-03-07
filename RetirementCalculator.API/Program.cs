using Microsoft.EntityFrameworkCore;
using RetirementCalculator.API.Data;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container using DEPENDENCY INJECTION (DI).

builder.Services.AddDbContext<RetirementContext>
    (opt => opt.UseInMemoryDatabase("RetirementDb"));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<RetirementCalculator.API.Services.RetirementCalculationService>();
// OR for SQL Server (when ready)
// builder.Services.AddDbContext<RetirementContext>(options =>
//     options.UseSqlServer(builder.Configuration.GetConnectionString("RetirementConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
