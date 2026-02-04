using TodoApi.Models;

namespace TodoApi.Repositories;

// Implementacion en memoria: no persiste entre reinicios.
public class TasksInMemoryRepository : ITasksRepository
{
    private readonly List<TodoTask> _tasks = new();
    private int _nextId = 1;

    // Devuelve la misma lista en memoria.
    public IEnumerable<TodoTask> GetAll()
    {
        return _tasks;
    }

    // Busca por id usando LINQ.
    public TodoTask? GetById(int id)
    {
        return _tasks.FirstOrDefault(t => t.Id == id);
    }

    // Asigna un id incremental y guarda.
    public TodoTask Add(TodoTask task)
    {
        task.Id = _nextId++;
        _tasks.Add(task);
        return task;
    }

    // Sustituye los campos editables si existe.
    public bool Update(int id, TodoTask task)
    {
        var existing = GetById(id);
        if (existing == null)
            return false;

        existing.Title = task.Title;
        existing.IsCompleted = task.IsCompleted;
        return true;
    }

    // Elimina por id si existe.
    public bool Delete(int id)
    {
        var task = GetById(id);
        if (task == null)
            return false;

        _tasks.Remove(task);
        return true;
    }
}
