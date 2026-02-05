using TodoApi.DTOs;
using TodoApi.Models;
using TodoApi.Repositories;
using TodoApi.Exceptions;


namespace TodoApi.Services;

// Implementa la interfaz para poder cambiar la logica sin tocar el controlador.
public class TasksService : ITasksService
{
    private readonly ITasksRepository _repository;

    public TasksService(ITasksRepository repository)
    {
        // DI: el repositorio concreto se resuelve en Program.cs.
        _repository = repository;
    }

    public IEnumerable<TaskReadDto> GetAll()
    {
        // Select aplica el mapeo a cada elemento (similar a stream().map()).
        return _repository.GetAll()
            .Select(ToReadDto);
    }

    public TaskReadDto? GetById(int id)
    {
        // Lanza excepcion de dominio si no existe (se traduce a 404).
        var task = _repository.GetById(id);
        if (task == null)
            throw new NotFoundException($"No existe la tarea con ID {id}.");
        return  ToReadDto(task);
    }

    public TaskReadDto Create(TaskCreateDto dto)
    {
        // DTO -> modelo de dominio.
        var task = new TodoTask
        {
            Title = dto.Title,
            IsCompleted = false
        };

        var created = _repository.Add(task);
        return ToReadDto(created);
    }

    public void Update(int id, TaskUpdateDto dto)
    {
        // Pasamos un modelo "nuevo" con los datos editados.
        var updated = _repository.Update(id, task: new TodoTask
        {
            Title = dto.Title,
            IsCompleted = dto.IsCompleted
        });

        if(!updated)
            // Si no se encontro la entidad, devolvemos 404 via excepcion.
            throw new NotFoundException($"No existe la tarea con ID {id}.");
    }

    public void Delete(int id)
    {
        var deleted = _repository.Delete(id);
        if (!deleted)
            throw new NotFoundException($"No existe la tarea con ID {id}.");
    }

    // 🔹 Mapeo manual (sin AutoMapper)
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
