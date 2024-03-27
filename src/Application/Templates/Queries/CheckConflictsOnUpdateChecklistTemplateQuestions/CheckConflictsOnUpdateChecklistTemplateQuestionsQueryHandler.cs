using Application.Checklists.Dtos;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities.ChecklistAggregate;
using Domain.Entities.TemplateAggregate;
using Mapster;
using MediatR;
using MobDeMob.Application.Common.Interfaces;
using MobDeMob.Domain.Entities;
using MobDeMob.Domain.Enums;
using MobDeMob.Domain.ItemAggregate;

namespace Application.Templates.Queries;

public class CheckConflictsOnUpdateChecklistTemplateQuestionsQueryHandler : IRequestHandler<CheckConflictsOnUpdateChecklistTemplateQuestionsQuery, IEnumerable<ChecklistBriefDto>>
{
    private readonly IChecklistRepository _checklistRepository;

    private readonly IMobilizationRepository _mobilizationRepository;

    private readonly IChecklistTemplateRepository _checklistTemplateRepository;

    public CheckConflictsOnUpdateChecklistTemplateQuestionsQueryHandler(IChecklistRepository checklistRepository, IChecklistTemplateRepository checklistTemplateRepository, IMobilizationRepository mobilizationRepository)
    {
        _checklistRepository = checklistRepository;
        _checklistTemplateRepository = checklistTemplateRepository;
        _mobilizationRepository = mobilizationRepository;
    }

    public async Task<IEnumerable<ChecklistBriefDto>> Handle(CheckConflictsOnUpdateChecklistTemplateQuestionsQuery request, CancellationToken cancellationToken)
    {
        // var itemHasItemTemplate = await _templateRepository.ItemTemplateExistsForItemIds(request.ItemIds, cancellationToken);
        // return itemHasItemTemplate
        //     .Select(templateDict => TemplateExistsReponse.New(templateDict.Key, templateDict.Value)).ToList();
        var checklists = await _checklistRepository.GetChecklistsByChecklistTemplateId(request.checklistTemplateId, cancellationToken); 

        ICollection<ChecklistBriefDto> conflictChecklists = [];
        foreach(var checklist in CheckIfChecklistStatusConflict(checklists)) 
        {
            var belongingMob = await _mobilizationRepository.GetMobilizationIdByChecklistCollectionId(checklist.ChecklistCollectionId, cancellationToken)
                ?? throw new NotFoundException(nameof(Mobilization), $"Could not find mobilization based on checklistcollectionId: '{checklist.ChecklistCollectionId}'");
            checklist.SetMobilizationId(belongingMob.Id);
            var checklistBrief = checklist.Adapt<ChecklistBriefDto>();
            conflictChecklists.Add(checklistBrief);
        }
        return conflictChecklists;
    }

    IEnumerable<Checklist> CheckIfChecklistStatusConflict(IEnumerable<Checklist> checklists)
    {
        foreach (var checklist in checklists)
        {
            if (checklist.Status != ChecklistStatus.NotStarted)
            {
                yield return checklist;
            }
        }
    }
}
