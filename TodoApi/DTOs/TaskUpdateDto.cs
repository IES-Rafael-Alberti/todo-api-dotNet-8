using System.ComponentModel.DataAnnotations;

namespace TodoApi.DTOs;

public class TaskUpdateDto
{
    // DTO de entrada para editar: incluye los campos modificables.
    [Required(ErrorMessage = "El título es obligatorio.")]
    [StringLength(100, MinimumLength = 3, 
        ErrorMessage = "El título debe tener entre 3 y 100 caracteres.")]
    public string Title { get; set; } = string.Empty;
    // Boolean en C#: true/false (equivalente a boolean en Java).
    public bool IsCompleted { get; set; }
}
