using Application.Common.Interfaces;
using MediatR;
using MobDeMob.Application.Common.Interfaces;
using MobDeMob.Application.Punches;

namespace MobDeMob.Application.Punches.Commands;

public class CreatePunchCommandHandler : IRequestHandler<CreatePunchCommand, string>
{
    private readonly IFileStorageRepository _fileStorageRepository;
    private readonly ICacheRepository _cacheRepository;
    private readonly IPunchRepository _punchRepository;


    public CreatePunchCommandHandler(IFileStorageRepository fileStorageRepository, ICacheRepository cachRepository, IPunchRepository punchRepository)
    {
        _fileStorageRepository = fileStorageRepository;
        _cacheRepository = cachRepository;
        _punchRepository = punchRepository;
    }

    public async Task<string> Handle(CreatePunchCommand request, CancellationToken cancellationToken)
    {
        return await _punchRepository.CreatePunch(request.Title, request.Description, request.ChecklistSectionId, cancellationToken);
    }
}
