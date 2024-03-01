


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

    //private readonly IMapper _mapper;

    public GetMobilizationsBySearchQueryHandler(IMobilizationRepository mobilizationRepository)
    {
        _mobilizationRepository = mobilizationRepository;
        //_mapper = mapper;
    }

    public Task<PaginatedList<MobilizationDto>> Handle(GetMobilizationBySearchQuery request, CancellationToken cancellationToken)
    {
        return _mobilizationRepository.GetMobilizationsBySearch(request.PageNumber, request.pageSize, request.title, request.mobilizationStatus, cancellationToken);
    }

    // public async Task<IEnumerable<MobilizationDto?>> Handle(GetMobilizationBySearchQuery request, CancellationToken cancellationToken)
    // {
    //     //TODO: look at the Mobilization Entity (ChecklistCountDone, ChecklistCount, PartsCount) to see why im not using GetMobilizationById instead of GetMobilizationByIdWithChecklistItems
    //     var mob = await _mobilizationRepository.GetMobilizationByIdWithChecklists(request.id, cancellationToken);
    //     // return _mapper.Map<MobilizationDto>(mob);
    //     return mob.Adapt<MobilizationDto>();
    // }

}
