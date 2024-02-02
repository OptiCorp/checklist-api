

using MediatR;
using MobDeMob.Domain.Entities.Mobilization;

namespace MobDeMob.Application.Mobilizations.Queries;

public class GetAllMobilizationsQuery : IRequest<IEnumerable<Mobilization>>
{

}