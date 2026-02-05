using Microsoft.AspNetCore.Mvc;
using TodoApi.DTOs;
using TodoApi.Services;

namespace TodoApi.Controllers;

[ApiController]
// [Route] define la ruta base del recurso.
[Route("api/tasks")]
// Hereda de ControllerBase para usar helpers de API (Ok, CreatedAtAction, etc.).
public class TasksController : ControllerBase
{
    private readonly ITasksService _service;

    public TasksController(ITasksService service)
    {
        // Inyeccion de dependencias (DI): el framework nos pasa el servicio.
        _service = service;
    }

    [HttpGet]
    public ActionResult<IEnumerable<TaskReadDto>> GetAll()
    {
        // 200 OK con la lista de tareas.
        return Ok(_service.GetAll());
    }

    [HttpGet("{id}")]
    public ActionResult<TaskReadDto> GetById(int id)
    {
        // {id} en la ruta se enlaza al parametro id.
        return Ok(_service.GetById(id));
    }

    [HttpPost]
    public ActionResult<TaskReadDto> Create(TaskCreateDto dto)
    {
        // CreatedAtAction genera 201 con Location apuntando al GET por id.
        var created = _service.Create(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, TaskUpdateDto dto)
    {
        // 204 NoContent cuando la actualizacion es correcta.
        _service.Update(id, dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        // 204 NoContent al borrar.
       _service.Delete(id);
       return NoContent();
    }
}
