using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MobDeMob.Application.Items.Queries;
using MobDeMob.Application.Parts.Queries;
using MobDeMob.Domain.Entities;
using MobDeMob.Domain.ItemAggregate;
using MobDeMob.Infrastructure;



namespace Api.Controllers;

[ApiController]
[Route("[api/checklists]")]
public class ChecklistController : ControllerBase
{

    private readonly ILogger<ChecklistController> _logger;


    private readonly ISender _sender;

    public ChecklistController(ILogger<ChecklistController> logger, ISender sender)
    {
        _logger = logger;
        _sender = sender;
    }

}
