


using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Mobilizations.Dtos;
using Mapster;
using MediatR;
using MobDeMob.Application.Mobilizations;

namespace Application.Mobilizations.Queries;

public class GetMobilizationsBySearchQueryHandler : IRequestHandler<GetMobilizationBySearchQuery, PaginatedList<MobilizationDto>>

{
    private readonly IMobilizationRepository _mobilizationRepository;
    public GetMobilizationsBySearchQueryHandler(IMobilizationRepository mobilizationRepository)
    {
        _mobilizationRepository = mobilizationRepository;
    }

    public async Task<PaginatedList<MobilizationDto>> Handle(GetMobilizationBySearchQuery request, CancellationToken cancellationToken)
    {
        var mobilizationsPaginated = await _mobilizationRepository
            .GetMobilizationsBySearch(request.PageNumber, request.PageSize, request.Title, request.MobilizationStatus, cancellationToken);

        var mobilizationsPaginatedDtos = new PaginatedList<MobilizationDto>(
                mobilizationsPaginated.Items.AsQueryable().ProjectToType<MobilizationDto>(),
                mobilizationsPaginated.TotalCount,
                mobilizationsPaginated.PageNumber,
                mobilizationsPaginated.TotalPages
        );

        return mobilizationsPaginatedDtos;
    }


}
