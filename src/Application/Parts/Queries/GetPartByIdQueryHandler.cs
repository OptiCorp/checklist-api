
using MediatR;
using MobDeMob.Application.Common.Interfaces;
using MobDeMob.Application.Mobilizations;
using MobDeMob.Application.Parts;
using MobDeMob.Application.Parts.Queries;
using MobDeMob.Domain.ItemAggregate;

namespace MobDeMob.Application.Items.Queries;

public class GetPartByIdQueryHandler : IRequestHandler<GetPartByIdQuery, PartDto?>
{
    private readonly IPartsRepository _partsRepository;


    public GetPartByIdQueryHandler(IPartsRepository partsRepository)
    {
        _partsRepository = partsRepository;
    }
    public async Task<PartDto?> Handle(GetPartByIdQuery request, CancellationToken cancellationToken)
    {
        var part =  await _partsRepository.GetById(request.Id, cancellationToken);
        return part?.AsDto();
    }
}