namespace TodoApi.DTOs;

// DTO de entrada: datos minimos para crear una tarea.
public class TaskCreateDto
{
    public string Title { get; set; } = string.Empty;
}
