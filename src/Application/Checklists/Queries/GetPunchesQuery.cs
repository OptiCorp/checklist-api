
using Application.Punches.Dtos;
using MediatR;
using MobDeMob.Domain.ItemAggregate;

namespace Application.Checklists.Queries;

public class GetPunchesQuery : IRequest<PunchListDto>
{
    public Guid MobilizationId {get; init;}
}
