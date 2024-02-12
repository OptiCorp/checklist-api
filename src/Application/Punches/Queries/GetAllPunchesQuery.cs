

using MediatR;
using MobDeMob.Domain.Entities.ChecklistAggregate;

namespace MobDeMob.Application.Punches.Queries;

public class GetAllPartPunchesQuery : IRequest<IEnumerable<PunchDto?>>
{
    public required string partId {get; init;}
}