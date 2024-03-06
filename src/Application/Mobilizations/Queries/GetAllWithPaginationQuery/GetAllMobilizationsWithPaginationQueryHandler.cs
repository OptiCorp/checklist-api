using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Mobilizations.Dtos;
using Mapster;
using MediatR;
using Application.Common.Mappings;
using MobDeMob.Application.Mobilizations;
using MobDeMob.Domain.Entities;
using MapsterMapper;

namespace Application.Mobilizations.Queries.GetAll;

public class GetAllMobilizationsQueryHandler : IRequestHandler<GetAllMobilizationsQuery, PaginatedList<MobilizationDto>>

{
    private readonly IMobilizationRepository _mobilizationRepository;

    public GetAllMobilizationsQueryHandler(IMobilizationRepository mobilizationRepository)
    {
        _mobilizationRepository = mobilizationRepository;
    }
    public async Task<PaginatedList<MobilizationDto>> Handle(GetAllMobilizationsQuery request, CancellationToken cancellationToken)
    {
        var mobilizationsPaginated = await _mobilizationRepository
            .GetAllMobilizationsWithPagination(request.PageNumber, request.PageSize, cancellationToken);

        var mobilizationsPaginatedDtos = new PaginatedList<MobilizationDto>(
                mobilizationsPaginated.Items.AsQueryable().ProjectToType<MobilizationDto>(),
                mobilizationsPaginated.TotalCount,
                mobilizationsPaginated.PageNumber,
                mobilizationsPaginated.TotalPages
        );

        return mobilizationsPaginatedDtos;

    }
}