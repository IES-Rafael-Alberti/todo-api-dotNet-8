using System.ComponentModel.DataAnnotations;

namespace TodoApi.DTOs;

public class TaskCreateDto
{
    // DTO de entrada: solo los datos que el cliente puede enviar al crear.
    [Required(ErrorMessage = "El título es obligatorio.")]
    [StringLength(100, MinimumLength = 3, 
        ErrorMessage = "El título debe tener entre 3 y 100 caracteres.")]
    public string Title { get; set; } = string.Empty;
}
