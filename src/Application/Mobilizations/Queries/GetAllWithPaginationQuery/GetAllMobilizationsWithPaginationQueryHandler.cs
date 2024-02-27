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

    //private readonly IMapper _mapper;

    public GetAllMobilizationsQueryHandler(IMobilizationRepository mobilizationRepository)
    {
        _mobilizationRepository = mobilizationRepository;
    }
    public async Task<PaginatedList<MobilizationDto>> Handle(GetAllMobilizationsQuery request, CancellationToken cancellationToken)
    {
        var mobilizationsDtos = await _mobilizationRepository
            .GetAllMobilizationsWithPagination(request.PageNumber, request.pageSize, cancellationToken);
        //var mobilizationsDto = mobilizations.Select(mob => mob.AsDto());
        // var mobilizationDtos = mobilizations.Select(m => _mapper.Map<MobilizationDto>(m));
        //var mobilizationDtos = mobilizations;
            // .ProjectToType<MobilizationDto>()
            // .PaginatedListAsync(request.PageNumber, request.pageSize);

        return mobilizationsDtos;

    }
}