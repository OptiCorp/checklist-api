


using Application.Common.Interfaces;
using MediatR;
using MobDeMob.Domain.Entities.Mobilization;

namespace MobDeMob.Application.Mobilizations.Queries;

public class GetMobilizationByIdQueryHandler : IRequestHandler<GetMobilizationByIdQuery, Mobilization?>

{
    private readonly IMobilizationRepository _mobilizationRepository;

    public GetMobilizationByIdQueryHandler(IMobilizationRepository mobilizationRepository)
    {
        _mobilizationRepository = mobilizationRepository;
    }


    public async Task<Mobilization?> Handle(GetMobilizationByIdQuery request, CancellationToken cancellationToken)
    {
        return await _mobilizationRepository.GetById(request.id, cancellationToken);
    }
}