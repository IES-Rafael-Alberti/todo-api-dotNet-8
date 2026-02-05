namespace TodoApi.Models;

using System.ComponentModel.DataAnnotations;
public class TodoTask
{
    // Propiedades auto-implementadas (get/set) similares a los campos con accesores en Java.
    public int Id { get; set; }

    // Atributos (annotations) usados por validacion y EF Core.
    [Required]
    [MaxLength]
    public string Title { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }
}
