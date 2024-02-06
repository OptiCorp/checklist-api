
using MediatR;
using MobDeMob.Application.Common.Interfaces;
using MobDeMob.Application.Mobilizations;
using MobDeMob.Application.Parts;
using MobDeMob.Application.Parts.Queries;
using MobDeMob.Domain.ItemAggregate;

namespace MobDeMob.Application.Items.Queries;

public class GetAllPartsQueryHandler : IRequestHandler<GetAllPartsQuery, IEnumerable<PartDto>>
{
    private readonly IPartsRepository _partsRepository;

    public GetAllPartsQueryHandler(IPartsRepository partsRepository)
    {
        _partsRepository = partsRepository;
    }

    public async Task<IEnumerable<PartDto>> Handle(GetAllPartsQuery request, CancellationToken cancellationToken)
    {
        var parts = await _partsRepository.GetAll(request.includeChildren, cancellationToken);
        return parts.Select(p => p.AsDto());
    }   
}