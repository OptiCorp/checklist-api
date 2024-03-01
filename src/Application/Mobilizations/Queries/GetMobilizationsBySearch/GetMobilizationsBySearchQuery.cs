using Application.Common.Models;
using Application.Mobilizations.Dtos;
using MediatR;
using MobDeMob.Domain.Entities;

namespace Application.Mobilizations.Queries;

public class GetMobilizationBySearchQuery : IRequest<PaginatedList<MobilizationDto?>>
{
    public string title { get; init; }

    public int PageNumber { get; init; } = 1;

    public int pageSize { get; init; } = 10;

    public MobilizationStatus? mobilizationStatus { get; init; }
}
