


using Application.Common.Interfaces;
using MediatR;
using MobDeMob.Domain.Entities.Mobilization;

namespace MobDeMob.Application.Mobilizations.Queries;

public class GetAllMobilizationsQueryHandler : IRequestHandler<GetAllMobilizationsQuery, IEnumerable<MobilizationDto>>

{
    private readonly IMobilizationRepository _mobilizationRepository;

    public GetAllMobilizationsQueryHandler(IMobilizationRepository mobilizationRepository)
    {
        _mobilizationRepository = mobilizationRepository;
    }
    public async Task<IEnumerable<MobilizationDto>> Handle(GetAllMobilizationsQuery request, CancellationToken cancellationToken)
    {
       var mobilizations = await _mobilizationRepository.GetAll(cancellationToken);
       var mobilizationsDto = mobilizations.Select(mob => mob.AsDto());
       return mobilizationsDto;
        
    }
}