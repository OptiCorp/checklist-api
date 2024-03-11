using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities.ChecklistAggregate;
using Domain.Entities.TemplateAggregate;
using Mapster;
using MediatR;
using MobDeMob.Domain.Enums;
using MobDeMob.Domain.ItemAggregate;

namespace Application.Templates.Queries;

public class CheckConflictsOnUpdateItemTemplateQuestionsQueryHandler : IRequestHandler<CheckConflictsOnUpdateItemTemplateQuestionsQuery, IEnumerable<Guid>>
{
    private readonly IChecklistRepository _checklistRepository;

    public CheckConflictsOnUpdateItemTemplateQuestionsQueryHandler(IChecklistRepository checklistRepository)
    {
        _checklistRepository = checklistRepository;
    }

    public async Task<IEnumerable<Guid>> Handle(CheckConflictsOnUpdateItemTemplateQuestionsQuery request, CancellationToken cancellationToken)
    {
        // var itemHasItemTemplate = await _templateRepository.ItemTemplateExistsForItemIds(request.ItemIds, cancellationToken);
        // return itemHasItemTemplate
        //     .Select(templateDict => TemplateExistsReponse.New(templateDict.Key, templateDict.Value)).ToList();
        var checklists = await _checklistRepository.GetChecklistByItemTemplateId(request.ItemTemplateId, cancellationToken);
        ICollection<Guid> conflictChecklists = [];
        foreach(var Id in CheckIfChecklistStatusConflict(checklists))
        {
            conflictChecklists.Add(Id);
        }
        return conflictChecklists;

    }

    IEnumerable<Guid> CheckIfChecklistStatusConflict(IEnumerable<Checklist> checklists)
    {
        foreach (var checklist in checklists)
        {
            if (checklist.Status != ChecklistStatus.NotStarted)
            {
                yield return checklist.Id;
            }
        }
    }
}
