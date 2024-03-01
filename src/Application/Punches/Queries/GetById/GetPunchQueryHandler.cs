﻿using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Punches.Dtos;
using Domain.Entities;
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

    private readonly IChecklistRepository _checklistRepository;


    private readonly IFileStorageRepository _fileStorageRepository;

    private readonly ICacheRepository _cacheRepository;

    public GetPunchQueryHandler(IPunchRepository punchRepository, IFileStorageRepository fileStorageRepository, ICacheRepository cachRepository, IChecklistRepository checklistRepository)
    {
        _punchRepository = punchRepository;
        _fileStorageRepository = fileStorageRepository;
        _cacheRepository = cachRepository;
        _checklistRepository = checklistRepository;
    }

    public async Task<PunchDto> Handle(GetPunchQuery request, CancellationToken cancellationToken)
    {
        var punch = await _punchRepository.GetPunchNoTracking(request.PunchId, cancellationToken)
            ?? throw new NotFoundException(nameof(Punch), request.PunchId);

        var checklist = await _checklistRepository.GetSingleChecklist(punch.ChecklistId)
            ?? throw new NotFoundException(nameof(Checklist), punch.ChecklistId);

        var blobUris = punch.PunchFiles;
        if (blobUris.Count == 0)
            return punch.Adapt<PunchDto>();

    
        var checklistCollectionId = checklist.ChecklistCollectionId;
        var containerSasUri = _cacheRepository.GetValue(checklistCollectionId.ToString());

        if (containerSasUri == null)
        {
            var newContainerSAS = await _fileStorageRepository.GenerateContainerSAS(checklistCollectionId.ToString(), cancellationToken);
            _cacheRepository.SetKeyValue(checklistCollectionId.ToString(), newContainerSAS);
            punch.SetSasToken(newContainerSAS.Query.ToString());
        }
        else
        {
            punch.SetSasToken(containerSasUri.Query.ToString());
        }

        return punch.Adapt<PunchDto>();
    }
}
