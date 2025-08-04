
using Microsoft.EntityFrameworkCore;
using GetChildren.Data;
using GetChildren.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                       ?? Environment.GetEnvironmentVariable("SQL_CONNECTION_STRING");

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(connectionString);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

async Task<List<Child>> GetChildren(DataContext context) => await context.Children.ToListAsync();
app.MapGet("/Children", async (DataContext context) => await GetChildren(context))
.WithName("GetChildren")
.WithOpenApi();

app.Run();