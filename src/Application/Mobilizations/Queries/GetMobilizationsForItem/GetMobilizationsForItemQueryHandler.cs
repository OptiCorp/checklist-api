
using Application.Common.Interfaces;
using Application.Mobilizations.Dtos;
using Mapster;
using MediatR;
using MobDeMob.Domain.Entities;

namespace Application.Mobilizations.Queries;

public class GetMobilizationForItemQueryHandler : IRequestHandler<GetMobilizationsForItemQuery, IEnumerable<MobilizationDto>>
{
    private readonly IMobilizationRepository _mobilizationRepository;

    //private readonly IMapper _mapper;

    public GetMobilizationForItemQueryHandler(IMobilizationRepository mobilizationRepository)
    {
        _mobilizationRepository = mobilizationRepository;
        //_mapper = mapper;
    }
    public async Task<IEnumerable<MobilizationDto>> Handle(GetMobilizationsForItemQuery request, CancellationToken cancellationToken)
    {
        var mobs = (await _mobilizationRepository.GetMobilizationsForItem(request.ItemId, cancellationToken)).AsQueryable();
        var mobsDto = mobs.ProjectToType<MobilizationDto>();
        return mobsDto;
    }
}