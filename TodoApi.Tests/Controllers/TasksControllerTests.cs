using Microsoft.AspNetCore.Mvc;
using Moq;
using TodoApi.Controllers;
using TodoApi.DTOs;
using TodoApi.Services;
using Xunit;

namespace TodoApi.Tests.Controllers;

public class TasksControllerTests
{
    private readonly Mock<ITasksService> _serviceMock;
    private readonly TasksController _controller;

    public TasksControllerTests()
    {
        _serviceMock = new Mock<ITasksService>();
        _controller = new TasksController(_serviceMock.Object);
    }

    [Fact]
    public void GetAll_ReturnsOkWithTasks()
    {
        _serviceMock.Setup(s => s.GetAll()).Returns(new[]
        {
            new TaskReadDto { Id = 1, Title = "Test", IsCompleted = false }
        });

        var result = _controller.GetAll();

        var ok = Assert.IsType<OkObjectResult>(result.Result);
        var tasks = Assert.IsAssignableFrom<IEnumerable<TaskReadDto>>(ok.Value);

        Assert.Single(tasks);
    }

    [Fact]
    public void Create_ReturnsCreatedAt()
    {
        var dto = new TaskCreateDto { Title = "Nueva" };
        var created = new TaskReadDto { Id = 1, Title = "Nueva", IsCompleted = false };

        _serviceMock.Setup(s => s.Create(dto)).Returns(created);

        var result = _controller.Create(dto);

        Assert.IsType<CreatedAtActionResult>(result.Result);
    }
}