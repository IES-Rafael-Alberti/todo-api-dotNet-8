using Microsoft.AspNetCore.Mvc;
using TodoApi.DTOs;
using TodoApi.Services;

namespace TodoApi.Controllers;

// Controlador REST: traduce peticiones HTTP a llamadas a la capa de servicio.
[ApiController]
[Route("api/tasks")]
public class TasksController : ControllerBase
{
    private readonly ITasksService _service;

    // El servicio se inyecta por constructor (inyeccion de dependencias).
    public TasksController(ITasksService service)
    {
        _service = service;
    }

    // GET /api/tasks
    [HttpGet]
    public ActionResult<IEnumerable<TaskReadDto>> GetAll()
    {
        return Ok(_service.GetAll());
    }

    // GET /api/tasks/{id}
    [HttpGet("{id}")]
    public ActionResult<TaskReadDto> GetById(int id)
    {
        var task = _service.GetById(id);
        if (task == null)
            return NotFound();

        return Ok(task);
    }

    // POST /api/tasks
    [HttpPost]
    public ActionResult<TaskReadDto> Create(TaskCreateDto dto)
    {
        var created = _service.Create(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    // PUT /api/tasks/{id}
    [HttpPut("{id}")]
    public IActionResult Update(int id, TaskUpdateDto dto)
    {
        var updated = _service.Update(id, dto);
        if (!updated)
            return NotFound();

        return NoContent();
    }

    // DELETE /api/tasks/{id}
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var deleted = _service.Delete(id);
        if (!deleted)
            return NotFound();

        return NoContent();
    }
}
