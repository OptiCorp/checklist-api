


using Application.Common.Interfaces;
using Application.Mobilizations.Dtos;
using Mapster;
using MediatR;
using MobDeMob.Application.Mobilizations;

namespace Application.Mobilizations.Queries.GetMobilizationById;

public class GetMobilizationByIdQueryHandler : IRequestHandler<GetMobilizationByIdQuery, MobilizationDto?>

{
    private readonly IMobilizationRepository _mobilizationRepository;

    //private readonly IMapper _mapper;

    public GetMobilizationByIdQueryHandler(IMobilizationRepository mobilizationRepository)
    {
        _mobilizationRepository = mobilizationRepository;
        //_mapper = mapper;
    }

    public async Task<MobilizationDto?> Handle(GetMobilizationByIdQuery request, CancellationToken cancellationToken)
    {
        //TODO: look at the Mobilization Entity (ChecklistCountDone, ChecklistCount, PartsCount) to see why im not using GetMobilizationById instead of GetMobilizationByIdWithChecklistItems
        var mob = await _mobilizationRepository.GetMobilizationByIdWithChecklists(request.Id, cancellationToken);
        // return _mapper.Map<MobilizationDto>(mob);
        return mob.Adapt<MobilizationDto>();
    }

}
