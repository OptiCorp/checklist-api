
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Mobilizations.Dtos;
using Mapster;
using MediatR;
using MobDeMob.Domain.Entities;

namespace Application.Mobilizations.Queries;

public class GetMobilizationForItemQueryHandler : IRequestHandler<GetMobilizationsForItemQuery, PaginatedList<MobilizationDto>>
{
    private readonly IMobilizationRepository _mobilizationRepository;
    public GetMobilizationForItemQueryHandler(IMobilizationRepository mobilizationRepository)
    {
        _mobilizationRepository = mobilizationRepository;
    }
    public async Task<PaginatedList<MobilizationDto>> Handle(GetMobilizationsForItemQuery request, CancellationToken cancellationToken)
    {
        var mobsPaginated = await _mobilizationRepository.GetMobilizationsForItem(request.ItemId, request.PageNumber, request.PageSize, cancellationToken);

        var mobilizationsPaginatedDtos = new PaginatedList<MobilizationDto>(
            mobsPaginated.Items.AsQueryable().ProjectToType<MobilizationDto>(),
            mobsPaginated.TotalCount,
            mobsPaginated.PageNumber,
            mobsPaginated.TotalPages
            );
        return mobilizationsPaginatedDtos;
    }
}