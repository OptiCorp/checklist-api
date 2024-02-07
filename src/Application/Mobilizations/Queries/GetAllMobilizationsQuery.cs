

using MediatR;
using MobDeMob.Domain.Entities;

namespace MobDeMob.Application.Mobilizations.Queries;

public class GetAllMobilizationsQuery : IRequest<IEnumerable<MobilizationDto>>
{

}