using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Punches.Dtos;
using Domain.Entities.ChecklistAggregate;
using Mapster;
using MediatR;
using MobDeMob.Application.Common.Interfaces;
using MobDeMob.Application.Mobilizations;
using MobDeMob.Domain.Entities.ChecklistAggregate;

namespace Application.Punches.Queries.GetById;

public class GetPunchQueryHandler : IRequestHandler<GetPunchQuery, PunchDto>

{
    private readonly IPunchRepository _punchRepository;

    private readonly IChecklistItemRepository _checklistItemRepository;


    private readonly IFileStorageRepository _fileStorageRepository;

    private readonly ICacheRepository _cacheRepository;

    public GetPunchQueryHandler(IPunchRepository punchRepository, IFileStorageRepository fileStorageRepository, ICacheRepository cachRepository, IChecklistItemRepository checklistItemRepository)
    {
        _punchRepository = punchRepository;
        _fileStorageRepository = fileStorageRepository;
        _cacheRepository = cachRepository;
        _checklistItemRepository = checklistItemRepository;
    }

    public async Task<PunchDto> Handle(GetPunchQuery request, CancellationToken cancellationToken)
    {
        var punch = await _punchRepository.GetPunchNoTracking(request.punchId, cancellationToken)
            ?? throw new NotFoundException(nameof(Punch), request.punchId);

        var checklistItem = await _checklistItemRepository.GetChecklistItem(punch.ChecklistItemId)
            ?? throw new NotFoundException(nameof(ChecklistItem), punch.ChecklistItemId);

        var blobUris = punch.ImageBlobUris;
        if (blobUris.Count == 0)
            return punch.Adapt<PunchDto>();

        var checklistId = checklistItem.ChecklistId; //TODO:
        var containerSasUri = _cacheRepository.GetValue(checklistId.ToString());

        if (containerSasUri == null)
        {
            var newContainerSAS = await _fileStorageRepository.GenerateContainerSAS(checklistId.ToString(), cancellationToken);
            _cacheRepository.SetKeyValue(checklistId.ToString(), newContainerSAS);
            punch.SasToken = newContainerSAS.Query.ToString();
        }
        else
        {
            punch.SasToken = containerSasUri.Query.ToString();
        }

        return punch.Adapt<PunchDto>();
    }
}
