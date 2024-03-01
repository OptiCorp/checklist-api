using Application.Mobilizations.Dtos;
using MediatR;

namespace Application.Mobilizations.Queries;

public class GetMobilizationByIdQuery : IRequest<MobilizationDto?>
{
    public Guid id { get; init; }
}
