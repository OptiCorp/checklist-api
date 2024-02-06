using MediatR;
using MobDeMob.Application.Parts;
using MobDeMob.Domain.Entities.Mobilization;

namespace MobDeMob.Application.Mobilizations.Queries;

public class GetAllPartsInMobilizationQuery : IRequest<IEnumerable<PartDto>>
{
    public required string id {get; init;}

    public bool includeChildren {get; set;}
}