using Application.Common.Models;
using Application.Mobilizations.Dtos;
using MediatR;

namespace Application.Mobilizations.Queries.GetAll;

public class GetAllMobilizationsQuery : IRequest<PaginatedList<MobilizationDto>>
{
    public int PageNumber {get; init;} = 1;

    public int pageSize {get; init;} = 10;
}
