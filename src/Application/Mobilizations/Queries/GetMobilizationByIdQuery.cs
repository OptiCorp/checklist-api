using MediatR;
using MobDeMob.Domain.Entities;

namespace MobDeMob.Application.Mobilizations.Queries;

public class GetMobilizationByIdQuery : IRequest<MobilizationDto?>
{
    public required string id {get; init;}
}