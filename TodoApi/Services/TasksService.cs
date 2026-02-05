using TodoApi.DTOs;
using TodoApi.Mapping;
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

    public IEnumerable<TaskReadDto> GetAll(TaskStatus? status = null)
    {
        // Select aplica el mapeo a cada elemento (similar a stream().map()).
        return _repository.GetAll(status)
            .Select(t => t.ToReadDto());
    }

    public TaskReadDto? GetById(int id)
    {
        // Lanza excepcion de dominio si no existe (se traduce a 404).
        var task = _repository.GetById(id);
        if (task == null)
            throw new NotFoundException($"No existe la tarea con ID {id}.");
        return task.ToReadDto();
    }

    public TaskReadDto Create(TaskCreateDto dto)
    {
        if (dto.DueDate is null)
            throw new BadRequestException(
                errorCode: "DUE_DATE_REQUIRED",
                message: "La fecha límite (DueDate) es obligatoria.");

        var now = DateTime.UtcNow;
        if (dto.DueDate.Value < now)
            throw new BadRequestException(
                errorCode: "DUE_DATE_INVALID",
                message: "La fecha límite (DueDate) no puede ser anterior a la fecha actual.");

        // DTO -> modelo de dominio.
        var task = dto.ToEntity();

        var created = _repository.Add(task);
        return created.ToReadDto();
    }

    public void Update(int id, TaskUpdateDto dto)
    {
        if (dto.DueDate is null)
            throw new BadRequestException(
                errorCode: "DUE_DATE_REQUIRED",
                message: "La fecha límite (DueDate) es obligatoria.");

        var existing = _repository.GetById(id);
        if (existing == null)
            throw new NotFoundException($"No existe la tarea con ID {id}.");

        if (dto.DueDate.Value < existing.CreationDate)
            throw new BadRequestException(
                errorCode: "DUE_DATE_INVALID",
                message: "La fecha límite (DueDate) no puede ser anterior a la fecha de creación.");

        // Pasamos un modelo "nuevo" con los datos editados.
        var updated = _repository.Update(id, task: new TodoTask
        {
            Title = dto.Title.Trim(),
            Description = string.IsNullOrWhiteSpace(dto.Description) ? null : dto.Description.Trim(),
            DueDate = dto.DueDate.Value,
            Status = dto.Status
        });

        if (!updated)
            throw new NotFoundException($"No existe la tarea con ID {id}.");
    }

    public void Delete(int id)
    {
        var deleted = _repository.Delete(id);
        if (!deleted)
            throw new NotFoundException($"No existe la tarea con ID {id}.");
    }

    // 🔹 Mapeo manual (sin AutoMapper) en TasksMapping.
}
