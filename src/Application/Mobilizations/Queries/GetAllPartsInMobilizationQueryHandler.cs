


using Application.Common.Interfaces;
using MediatR;
using MobDeMob.Application.Parts;
using MobDeMob.Domain.Entities;

namespace MobDeMob.Application.Mobilizations.Queries;

public class GetAllPartsInMobilizationQueryHandler : IRequestHandler<GetAllPartsInMobilizationQuery, IEnumerable<PartDto>>

{
    private readonly IMobilizationRepository _mobilizationRepository;

    public GetAllPartsInMobilizationQueryHandler(IMobilizationRepository mobilizationRepository)
    {
        _mobilizationRepository = mobilizationRepository;
    }

    public async Task<IEnumerable<PartDto>> Handle(GetAllPartsInMobilizationQuery request, CancellationToken cancellationToken)
    {
        var parts = await _mobilizationRepository.GetAllPartsInMobilization(request.id, request.includeChildren,cancellationToken);
        return parts.Select(p => p.AsDto());
    }
}