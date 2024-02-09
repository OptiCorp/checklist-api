using MediatR;
using MobDeMob.Application.Common.Interfaces;
using MobDeMob.Domain.Entities.ChecklistAggregate;

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
        dynamic checklistrepo = null;// inject IChecklistRepository, I'm just 
        dynamic request2 = null;// I think the request should have the checklistId and not ChecklistSectionId, but double check

        var checklistSection = await checklistrepo.GetById(request2.checkListId);
        var newPunch = new Punch
        {
            Title = "",//Title,
            Description = "",//Description,
            Section = checklistSection
        };

        return await _punchRepository.CreatePunch(newPunch, cancellationToken);
    }
}
