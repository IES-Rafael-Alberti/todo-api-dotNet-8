namespace TodoApi.Models;

// Modelo de dominio: representa una tarea dentro del servidor.
public class TodoTask
{
    // Identificador interno.
    public int Id { get; set; }
    // Titulo corto.
    public string Title { get; set; } = string.Empty;
    // Estado de finalizacion.
    public bool IsCompleted { get; set; }
}
