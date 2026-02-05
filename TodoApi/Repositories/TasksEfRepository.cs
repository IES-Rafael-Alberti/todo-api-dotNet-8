using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.Exceptions;
using TodoApi.Models;

namespace TodoApi.Repositories;

public class TasksEfRepository : ITasksRepository
{
    private const int MaxPending = 10;
    private readonly TodoDbContext _db;

    public TasksEfRepository(TodoDbContext db)
    {
        _db = db;
    }

    public IEnumerable<TodoTask> GetAll()
    {
        // AsNoTracking: lectura sin seguimiento de cambios (mas rapido para GET).
        return _db.Tasks.AsNoTracking().ToList();
    }

    public TodoTask? GetById(int id)
    {
        // FirstOrDefault devuelve null si no existe.
        return _db.Tasks.AsNoTracking()
            .FirstOrDefault(t => t.Id == id);
    }

    public TodoTask Add(TodoTask task)
    {
        if (!task.IsCompleted && CountPending() >= MaxPending)
            throw new ConflictException(
                errorCode: "MAX_PENDING_REACHED",
                message: $"No se pueden crear más de {MaxPending} tareas pendientes.");

        _db.Tasks.Add(task);
        _db.SaveChanges();
        return task;
    }

    public int CountPending()
    {
        return _db.Tasks.Count(t => !t.IsCompleted);
    }

    public bool Update(int id, TodoTask task)
    {
        // Find usa la clave primaria y puede devolver null si no existe.
        var existing = _db.Tasks.Find(id);
        if (existing == null)
            return false;

        existing.Title = task.Title;
        existing.IsCompleted = task.IsCompleted;

        _db.SaveChanges();
        return true;
    }

    public bool Delete(int id)
    {
        var task = _db.Tasks.Find(id);
        if (task == null)
            return false;

        _db.Tasks.Remove(task);
        _db.SaveChanges();
        return true;
    }
}
