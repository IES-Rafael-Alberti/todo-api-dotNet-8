using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Reflection;
using TodoApi.Controllers;
using TodoApi.DTOs;
using TodoApi.Models;
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
        _serviceMock.Setup(s => s.GetAll(It.IsAny<TaskStatus?>())).Returns(new[]
        {
            new TaskReadDto
            {
                Id = 1,
                Title = "Test",
                Description = null,
                CreationDate = DateTime.UtcNow.AddMinutes(-5),
                DueDate = DateTime.UtcNow.AddDays(1),
                Status = TaskStatus.Pending
            }
        });

        var result = _controller.GetAll(status: null);

        var ok = Assert.IsType<OkObjectResult>(result.Result);
        var tasks = Assert.IsAssignableFrom<IEnumerable<TaskReadDto>>(ok.Value);

        Assert.Single(tasks);
    }

    [Fact]
    public void Create_ReturnsCreatedAt()
    {
        var dto = new TaskCreateDto
        {
            Title = "Nueva",
            DueDate = DateTime.UtcNow.AddDays(1),
            Status = TaskStatus.Pending
        };
        var created = new TaskReadDto
        {
            Id = 1,
            Title = "Nueva",
            Description = null,
            CreationDate = DateTime.UtcNow,
            DueDate = dto.DueDate.Value,
            Status = TaskStatus.Pending
        };

        _serviceMock.Setup(s => s.Create(dto, It.IsAny<int>())).Returns(created);

        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, "1")
                }, "test"))
            }
        };

        var result = _controller.Create(dto);

        Assert.IsType<CreatedAtActionResult>(result.Result);
    }

    [Fact]
    public void Controller_HasAuthorizeAttribute()
    {
        var attr = typeof(TasksController).GetCustomAttribute<AuthorizeAttribute>();
        Assert.NotNull(attr);
    }
}
