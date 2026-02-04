using TodoApi.DTOs;

namespace TodoApi.Services;

// Capa de negocio: define operaciones sobre tareas.
public interface ITasksService
{
    // Devuelve la lista de tareas para leer (DTO de salida).
    IEnumerable<TaskReadDto> GetAll();
    // Busca una tarea por id; null si no existe.
    TaskReadDto? GetById(int id);
    // Crea una tarea a partir del DTO de entrada.
    TaskReadDto Create(TaskCreateDto dto);
    // Actualiza una tarea existente.
    bool Update(int id, TaskUpdateDto dto);
    // Elimina una tarea.
    bool Delete(int id);
}
