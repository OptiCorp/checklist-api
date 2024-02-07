


using Application.Common.Interfaces;
using MediatR;
using MobDeMob.Domain.Entities;

namespace MobDeMob.Application.Mobilizations.Queries;

public class GetMobilizationByIdQueryHandler : IRequestHandler<GetMobilizationByIdQuery, MobilizationDto?>

{
    private readonly IMobilizationRepository _mobilizationRepository;

    public GetMobilizationByIdQueryHandler(IMobilizationRepository mobilizationRepository)
    {
        _mobilizationRepository = mobilizationRepository;
    }


    public async Task<MobilizationDto?> Handle(GetMobilizationByIdQuery request, CancellationToken cancellationToken)
    {

        var mob = await _mobilizationRepository.GetById(request.id, cancellationToken);
        return mob?.AsDto();
    }
    
}