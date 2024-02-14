


using Application.Common.Interfaces;
using Application.Mobilizations.Dtos;
using AutoMapper;
using MediatR;
using MobDeMob.Application.Mobilizations;

namespace Application.Mobilizations.Queries.GetMobilizationById;

public class GetMobilizationByIdQueryHandler : IRequestHandler<GetMobilizationByIdQuery, MobilizationDto?>

{
    private readonly IMobilizationRepository _mobilizationRepository;

    private readonly IMapper _mapper;

    public GetMobilizationByIdQueryHandler(IMobilizationRepository mobilizationRepository, IMapper mapper)
    {
        _mobilizationRepository = mobilizationRepository;
        _mapper = mapper;
    }

    public async Task<MobilizationDto?> Handle(GetMobilizationByIdQuery request, CancellationToken cancellationToken)
    {
        var mob = await _mobilizationRepository.GetMobilizationById(request.id, cancellationToken);
        return  _mapper.Map<MobilizationDto>(mob);
    }

}
