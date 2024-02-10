using Application.Mobilizations.Dtos;
using MediatR;

namespace Application.Mobilizations.Queries.GetAll;

public class GetAllMobilizationsQuery : IRequest<IEnumerable<MobilizationDto>>
{

}
