
using MediatR;
using MobDeMob.Application.Common.Interfaces;
using MobDeMob.Application.Mobilizations;
using MobDeMob.Domain.Entities.ChecklistAggregate;

namespace MobDeMob.Application.Punches.Queries;

public class GetAllPunchesQueryHandler : IRequestHandler<GetAllPartPunchesQuery, IEnumerable<PunchDto>>
{
    private readonly IPunchRepository _punchRepository;

    private readonly IFileStorageRepository _fileStorageRepository;

    private readonly ICacheRepository _cacheRepository;

    public GetAllPunchesQueryHandler(IPunchRepository punchRepository, IFileStorageRepository fileStorageRepository, ICacheRepository cachRepository)
    {
        _punchRepository = punchRepository;
        _fileStorageRepository = fileStorageRepository;
        _cacheRepository = cachRepository;
    }
    public async Task<IEnumerable<PunchDto>> Handle(GetAllPartPunchesQuery request, CancellationToken cancellationToken)
    {
        var punches = await _punchRepository.GetAllPartPunches(request.partId, cancellationToken);
        return punches.Select(p => p.AsDto(null));
    }
}