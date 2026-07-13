using Microsoft.EntityFrameworkCore;
using Expense.Api.Data;

// Criando o builder da aplicação.
var builder = WebApplication.CreateBuilder(args);

// Adicionando o Swagger para a documentação da API.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Adicionando a conexão com o banco
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source=controlegastos.db"));

var app = builder.Build();

// Se estiver em desenvolvimento, usa o Swagger.
if (app.Environment.IsDevelopment()){
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Redireciona para HTTPS.
app.UseHttpsRedirection();

// Mapeia os controllers.
app.MapControllers();

// Executa a aplicação.
app.Run();