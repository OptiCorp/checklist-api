

using MediatR;
using MobDeMob.Domain.Entities.ChecklistAggregate;

namespace MobDeMob.Application.Punches.Queries;

public class GetPunchQuery : IRequest<PunchDto?>
{
    public required string punchId {get; set;}
}