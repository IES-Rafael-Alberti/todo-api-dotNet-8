using Moq;
using TodoApi.DTOs;
using TodoApi.Exceptions;
using TodoApi.Models;
using TodoApi.Repositories;
using TodoApi.Services;
using Xunit;

namespace TodoApi.Tests.Services;

public class TasksServiceTests
{
    private readonly Mock<ITasksRepository> _repoMock;
    private readonly TasksService _service;

    public TasksServiceTests()
    {
        _repoMock = new Mock<ITasksRepository>();
        _service = new TasksService(_repoMock.Object);
    }

    [Fact]
    public void GetById_WhenTaskExists_ReturnsTask()
    {
        var task = new TodoTask
        {
            Id = 1,
            Title = "Test",
            Description = null,
            CreationDate = DateTime.UtcNow.AddMinutes(-10),
            DueDate = DateTime.UtcNow.AddDays(1),
            Status = TaskStatus.Pending
        };
        _repoMock.Setup(r => r.GetById(1)).Returns(task);

        var result = _service.GetById(1);

        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("Test", result.Title);
    }

    [Fact]
    public void GetById_WhenTaskDoesNotExist_ThrowsNotFound()
    {
        _repoMock.Setup(r => r.GetById(99)).Returns((TodoTask?)null);

        Assert.Throws<NotFoundException>(() => _service.GetById(99));
    }

    [Fact]
    public void Create_CreatesTaskWithDefaults()
    {
        _repoMock
            .Setup(r => r.Add(It.IsAny<TodoTask>()))
            .Returns((TodoTask t) =>
            {
                t.Id = 1;
                return t;
            });

        var dto = new TaskCreateDto
        {
            Title = "Nueva tarea",
            DueDate = DateTime.UtcNow.AddDays(1),
            Status = TaskStatus.Pending
        };

        var result = _service.Create(dto);

        Assert.Equal("Nueva tarea", result.Title);
        Assert.Equal(TaskStatus.Pending, result.Status);
    }
}
