using TodoApi.DTOs;
using TodoApi.Models;
using TodoApi.Repositories;

namespace TodoApi.Services;

// Implementa la logica de negocio y el mapeo entre modelos y DTOs.
public class TasksService : ITasksService
{
    private readonly ITasksRepository _repository;

    // El repositorio se inyecta para aislar el acceso a datos.
    public TasksService(ITasksRepository repository)
    {
        _repository = repository;
    }

    // Lista de tareas convertidas a DTO de lectura.
    public IEnumerable<TaskReadDto> GetAll()
    {
        return _repository.GetAll()
            .Select(ToReadDto);
    }

    // Devuelve una tarea por id o null si no existe.
    public TaskReadDto? GetById(int id)
    {
        var task = _repository.GetById(id);
        return task == null ? null : ToReadDto(task);
    }

    // Crea un modelo interno a partir del DTO de entrada.
    public TaskReadDto Create(TaskCreateDto dto)
    {
        var task = new TodoTask
        {
            Title = dto.Title,
            IsCompleted = false
        };

        var created = _repository.Add(task);
        return ToReadDto(created);
    }

    // Actualiza todo el estado de una tarea.
    public bool Update(int id, TaskUpdateDto dto)
    {
        var task = new TodoTask
        {
            Title = dto.Title,
            IsCompleted = dto.IsCompleted
        };

        return _repository.Update(id, task);
    }

    // Borra una tarea por id.
    public bool Delete(int id)
    {
        return _repository.Delete(id);
    }

    // Mapeo manual (sin AutoMapper) para no depender de librerias extras.
    private static TaskReadDto ToReadDto(TodoTask task)
    {
        return new TaskReadDto
        {
            Id = task.Id,
            Title = task.Title,
            IsCompleted = task.IsCompleted
        };
    }
}
