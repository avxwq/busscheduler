using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using RozkladApi.Models;

var builder = WebApplication.CreateBuilder(args);

SQLitePCL.Batteries.Init();

builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
