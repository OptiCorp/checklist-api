using Application.Mobilizations.Dtos;
using MediatR;

namespace Application.Mobilizations.Queries;

public class GetMobilizationsForItemQuery: IRequest<IEnumerable<MobilizationDto?>>
{
    public string ItemId { get; init; }
}
