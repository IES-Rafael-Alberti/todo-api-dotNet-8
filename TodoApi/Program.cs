using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.Repositories;
using TodoApi.Middleware;

// Host/Builder: prepara configuracion, DI y logging.
var builder = WebApplication.CreateBuilder(args);

// Registro de controladores (API REST con atributos).
builder.Services.AddControllers();

// Swagger/OpenAPI (solo en desarrollo).
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DbContext de EF Core con SQLite (cadena en appsettings.json).
builder.Services.AddDbContext<TodoDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("TodoDb")));
// DI: cada request obtiene su repositorio (scope).
builder.Services.AddScoped<ITasksRepository, TasksEfRepository>();
// Repositorio in-memory
// builder.Services.AddSingleton<ITasksRepository, TasksInMemoryRepository>();

// Construye la app con todo lo registrado.
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    // Solo en desarrollo: UI de Swagger.
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Redirige HTTP a HTTPS.
app.UseHttpsRedirection();

// 🔹 FRONTEND ESTÁTICO (wwwroot)
app.UseDefaultFiles();   // busca index.html
app.UseStaticFiles();    // sirve HTML, JS, CSS

// Autorización (aunque todavía no haya auth real).
app.UseAuthorization();

// 🔹 Middleware de manejo de errores
app.UseMiddleware<ErrorHandlingMiddleware>();

// API REST: mapea rutas de controladores.
app.MapControllers();

// ❌ Eliminamos la mini app WeatherForecast
// (no la necesitamos para este proyecto)

// Arranque
app.Run();
