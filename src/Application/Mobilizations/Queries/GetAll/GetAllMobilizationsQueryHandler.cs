using Application.Common.Interfaces;
using Application.Mobilizations.Dtos;
using Mapster;
using MediatR;
using MobDeMob.Application.Mobilizations;
using MobDeMob.Domain.Entities;

namespace Application.Mobilizations.Queries.GetAll;

public class GetAllMobilizationsQueryHandler : IRequestHandler<GetAllMobilizationsQuery, IEnumerable<MobilizationDto>>

{
    private readonly IMobilizationRepository _mobilizationRepository;

    //private readonly IMapper _mapper;

    public GetAllMobilizationsQueryHandler(IMobilizationRepository mobilizationRepository)
    {
        _mobilizationRepository = mobilizationRepository;

    }
    public async Task<IEnumerable<MobilizationDto>> Handle(GetAllMobilizationsQuery request, CancellationToken cancellationToken)
    {
        var mobilizations = await _mobilizationRepository.GetAllMobilizations(cancellationToken);
        //var mobilizationsDto = mobilizations.Select(mob => mob.AsDto());
        // var mobilizationDtos = mobilizations.Select(m => _mapper.Map<MobilizationDto>(m));
        var mobilizationDtos = mobilizations.AsQueryable().ProjectToType<MobilizationDto>();

        return mobilizationDtos;

    }
}