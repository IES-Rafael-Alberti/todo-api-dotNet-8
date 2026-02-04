using TodoApi.Repositories;
using TodoApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Registra controladores (endpoints REST).
builder.Services.AddControllers();

// Swagger (solo en desarrollo) para explorar y probar la API.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Repositorio in-memory (sin base de datos) para trabajar en clase.
builder.Services.AddSingleton<ITasksRepository, TasksInMemoryRepository>();
// Capa de servicio: contiene la logica de negocio.
builder.Services.AddScoped<ITasksService, TasksService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Fuerza HTTPS cuando sea posible.
app.UseHttpsRedirection();

// FRONTEND ESTATICO (wwwroot)
app.UseDefaultFiles();   // busca index.html
app.UseStaticFiles();    // sirve HTML, JS, CSS

// Autorizacion (aunque todavia no haya auth real).
app.UseAuthorization();

// Mapea los controladores a rutas HTTP.
app.MapControllers();

// ❌ Eliminamos la mini app WeatherForecast
// (no la necesitamos para este proyecto)

// Arranque de la aplicacion.
app.Run();
