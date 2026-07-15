using Microsoft.EntityFrameworkCore;
using Expense.Api.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// SQLite: os dados ficam em arquivo e persistem depois de fechar a API.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=controlegastos.db"));

builder.Services.AddCors(options =>{
    options.AddPolicy("AllowAnyFrontend", policy => {
        policy.WithOrigins("http://localhost:5173")
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseCors("AllowAnyFrontend");
app.MapControllers();
app.Run();
