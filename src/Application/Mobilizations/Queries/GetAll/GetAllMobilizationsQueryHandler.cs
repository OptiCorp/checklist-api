using Application.Common.Interfaces;
using Application.Mobilizations.Dtos;
using AutoMapper;
using MediatR;
using MobDeMob.Application.Mobilizations;
using MobDeMob.Domain.Entities;

namespace Application.Mobilizations.Queries.GetAll;

public class GetAllMobilizationsQueryHandler : IRequestHandler<GetAllMobilizationsQuery, IEnumerable<MobilizationDto>>

{
    private readonly IMobilizationRepository _mobilizationRepository;

     private readonly IMapper _mapper;

    public GetAllMobilizationsQueryHandler(IMobilizationRepository mobilizationRepository, IMapper mapper)
    {
        _mobilizationRepository = mobilizationRepository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<MobilizationDto>> Handle(GetAllMobilizationsQuery request, CancellationToken cancellationToken)
    {
        var mobilizations = await _mobilizationRepository.GetAllMobilizations(cancellationToken);
        //var mobilizationsDto = mobilizations.Select(mob => mob.AsDto());
        var mobilizationDtos = mobilizations.Select(m => _mapper.Map<MobilizationDto>(m));
        return mobilizationDtos;

    }
}