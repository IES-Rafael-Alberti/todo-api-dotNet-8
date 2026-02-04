using TodoApi.Models;

namespace TodoApi.Repositories;

// Contrato de acceso a datos (persistencia).
public interface ITasksRepository
{
    // Devuelve todas las tareas.
    IEnumerable<TodoTask> GetAll();
    // Busca una tarea por id.
    TodoTask? GetById(int id);
    // Inserta una tarea nueva.
    TodoTask Add(TodoTask task);
    // Actualiza una tarea existente.
    bool Update(int id, TodoTask task);
    // Elimina una tarea.
    bool Delete(int id);
}
