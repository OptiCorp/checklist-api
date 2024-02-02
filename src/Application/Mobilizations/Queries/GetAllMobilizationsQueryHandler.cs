


using Application.Common.Interfaces;
using MediatR;
using MobDeMob.Domain.Entities.Mobilization;

namespace MobDeMob.Application.Mobilizations.Queries;

public class GetAllMobilizationsQueryHandler : IRequestHandler<GetAllMobilizationsQuery, IEnumerable<Mobilization>>

{
    private readonly IMobilizationRepository _mobilizationRepository;

    public GetAllMobilizationsQueryHandler(IMobilizationRepository mobilizationRepository)
    {
        _mobilizationRepository = mobilizationRepository;
    }
    public async Task<IEnumerable<Mobilization>> Handle(GetAllMobilizationsQuery request, CancellationToken cancellationToken)
    {
        return await _mobilizationRepository.GetAll(cancellationToken);
    }
}