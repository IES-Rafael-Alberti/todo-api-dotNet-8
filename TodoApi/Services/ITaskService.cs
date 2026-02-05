using TodoApi.DTOs;

namespace TodoApi.Services;

public interface ITasksService
{
    IEnumerable<TaskReadDto> GetAll();
    TaskReadDto? GetById(int id);
    TaskReadDto Create(TaskCreateDto dto);
    void Update(int id, TaskUpdateDto dto);
    void Delete(int id);
}