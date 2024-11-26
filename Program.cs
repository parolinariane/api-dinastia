using Microsoft.EntityFrameworkCore;
using SistemaCadastroLogin.Data;

var builder = WebApplication.CreateBuilder(args);

// Adicionar a política de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost", policy =>
    {
        policy.WithOrigins("http://localhost:8080")  // Permite o front-end no localhost:8080
              .AllowAnyMethod()                     // Permite qualquer método HTTP (GET, POST, etc)
              .AllowAnyHeader();                    // Permite qualquer cabeçalho
    });
});

// Configurar DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Adicionar serviços e dependências
builder.Services.AddControllers();

var app = builder.Build();

app.UseAuthorization();

// Aplicar a política de CORS
app.UseCors("AllowLocalhost");

app.MapControllers();

app.Run();
