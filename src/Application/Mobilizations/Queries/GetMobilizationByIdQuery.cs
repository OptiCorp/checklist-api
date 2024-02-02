using MediatR;
using MobDeMob.Domain.Entities.Mobilization;

namespace MobDeMob.Application.Mobilizations.Queries;

public class GetMobilizationByIdQuery : IRequest<Mobilization?>
{
    public required string id {get; init;}
}