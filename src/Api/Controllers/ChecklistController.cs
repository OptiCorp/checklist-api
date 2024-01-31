using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MobDeMob.Domain.Entities;
using Model;
using Model.Context;



namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ChecklistController : ControllerBase
{

    private readonly ILogger<ChecklistController> _logger;
    private readonly ModelContextBase _context;

    public ChecklistController(ILogger<ChecklistController> logger, ModelContextBase context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<ActionResult<IEnumerable<Item>>> GetTodoItems(CancellationToken cancellationToken)
    {
        return await _context.Items.ToListAsync(cancellationToken);
    }
}
