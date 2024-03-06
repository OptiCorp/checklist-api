


using Application.Common.Interfaces;
using Application.Mobilizations.Dtos;
using Mapster;
using MediatR;
using MobDeMob.Application.Mobilizations;

namespace Application.Mobilizations.Queries.GetMobilizationById;

public class GetMobilizationByIdQueryHandler : IRequestHandler<GetMobilizationByIdQuery, MobilizationDto?>

{
    private readonly IMobilizationRepository _mobilizationRepository;

    public GetMobilizationByIdQueryHandler(IMobilizationRepository mobilizationRepository)
    {
        _mobilizationRepository = mobilizationRepository;
    }

    public async Task<MobilizationDto?> Handle(GetMobilizationByIdQuery request, CancellationToken cancellationToken)
    {
        var mob = await _mobilizationRepository.GetMobilizationByIdWithChecklists(request.Id, cancellationToken);

        return mob.Adapt<MobilizationDto>();
    }

}
