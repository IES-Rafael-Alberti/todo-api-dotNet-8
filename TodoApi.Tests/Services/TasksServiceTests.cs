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
            Status = TaskStatus.Pending,
            UserId = 7
        };
        _repoMock.Setup(r => r.GetById(1)).Returns(task);

        var result = _service.GetById(1, userId: 7);

        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("Test", result.Title);
    }

    [Fact]
    public void GetById_WhenTaskDoesNotExist_ThrowsNotFound()
    {
        _repoMock.Setup(r => r.GetById(99)).Returns((TodoTask?)null);

        Assert.Throws<NotFoundException>(() => _service.GetById(99, userId: 1));
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

        var result = _service.Create(dto, userId: 1);

        Assert.Equal("Nueva tarea", result.Title);
        Assert.Equal(TaskStatus.Pending, result.Status);
    }

    [Fact]
    public void GetById_WhenTaskBelongsToOtherUser_ThrowsForbidden()
    {
        _repoMock.Setup(r => r.GetById(1)).Returns(new TodoTask
        {
            Id = 1,
            Title = "Privada",
            CreationDate = DateTime.UtcNow,
            DueDate = DateTime.UtcNow.AddDays(1),
            Status = TaskStatus.Pending,
            UserId = 2
        });

        Assert.Throws<ForbiddenException>(() => _service.GetById(1, userId: 1));
    }

    [Fact]
    public void GetAll_UsesRepositoryFilterByUser()
    {
        _repoMock.Setup(r => r.GetAllByUser(5, null)).Returns(new[]
        {
            new TodoTask
            {
                Id = 1,
                Title = "Solo del usuario 5",
                CreationDate = DateTime.UtcNow,
                DueDate = DateTime.UtcNow.AddDays(1),
                Status = TaskStatus.Pending,
                UserId = 5
            }
        });

        var result = _service.GetAll(userId: 5).ToList();

        Assert.Single(result);
        Assert.Equal("Solo del usuario 5", result[0].Title);
    }

    [Fact]
    public void Update_WhenTaskBelongsToOtherUser_ThrowsForbidden()
    {
        _repoMock.Setup(r => r.GetById(1)).Returns(new TodoTask
        {
            Id = 1,
            Title = "Ajena",
            CreationDate = DateTime.UtcNow.AddHours(-1),
            DueDate = DateTime.UtcNow.AddDays(1),
            Status = TaskStatus.Pending,
            UserId = 9
        });

        var dto = new TaskUpdateDto
        {
            Title = "Editar",
            DueDate = DateTime.UtcNow.AddDays(2),
            Status = TaskStatus.InProgress
        };

        Assert.Throws<ForbiddenException>(() => _service.Update(1, dto, userId: 1));
    }

    [Fact]
    public void Delete_WhenTaskBelongsToOtherUser_ThrowsForbidden()
    {
        _repoMock.Setup(r => r.GetById(1)).Returns(new TodoTask
        {
            Id = 1,
            Title = "Ajena",
            CreationDate = DateTime.UtcNow.AddHours(-1),
            DueDate = DateTime.UtcNow.AddDays(1),
            Status = TaskStatus.Pending,
            UserId = 9
        });

        Assert.Throws<ForbiddenException>(() => _service.Delete(1, userId: 1));
    }
}
