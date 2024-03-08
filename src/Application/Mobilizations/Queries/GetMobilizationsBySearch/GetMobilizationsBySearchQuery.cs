using Application.Common.Models;
using Application.Mobilizations.Dtos;
using MediatR;
using MobDeMob.Domain.Entities;

namespace Application.Mobilizations.Queries;

public class GetMobilizationBySearchQuery : IRequest<PaginatedList<MobilizationDto>>
{
    public string? Title { get; init; }

    public int PageNumber { get; init; } = 1;

    public int PageSize { get; init; } = 10;

    public MobilizationStatus? MobilizationStatus { get; init; }
}
