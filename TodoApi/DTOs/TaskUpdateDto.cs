namespace TodoApi.DTOs;

// DTO de entrada: datos editables de una tarea.
public class TaskUpdateDto
{
    public string Title { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }
}
