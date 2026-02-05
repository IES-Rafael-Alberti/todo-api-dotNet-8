using TodoApi.DTOs;
using TodoApi.Models;

namespace TodoApi.Services;

public interface ITasksService
{
    IEnumerable<TaskReadDto> GetAll(TaskStatus? status = null);
    TaskReadDto? GetById(int id);
    TaskReadDto Create(TaskCreateDto dto);
    void Update(int id, TaskUpdateDto dto);
    void Delete(int id);
}
