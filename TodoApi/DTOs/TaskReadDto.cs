namespace TodoApi.DTOs;

// DTO de salida: lo que se devuelve al cliente.
public class TaskReadDto
{
    // DTO de salida: datos que devolvemos al cliente.
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }
}
