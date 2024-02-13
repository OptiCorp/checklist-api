using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities.ChecklistAggregate;
using MediatR;
using MobDeMob.Domain.Entities;
using MobDeMob.Domain.ItemAggregate;

namespace Application.Checklists.Commands.AddItem;

public class AddItemCommandHandler : IRequestHandler<AddItemCommand, Guid>
{
    private readonly IMobilizationRepository _mobilizationRepository;
    private readonly ITemplateRepository _templateRepository;
    private readonly IChecklistItemRepository _checklistItemRepository;
    private readonly IChecklistItemQuestionRepository _checklistItemQuestionRepository;

    public AddItemCommandHandler(
        IMobilizationRepository mobilizationRepository,
        ITemplateRepository templateRepository,
        IChecklistItemRepository checklistItemRepository,
        IChecklistItemQuestionRepository checklistItemQuestionRepository)
    {
        _mobilizationRepository = mobilizationRepository;
        _templateRepository = templateRepository;
        _checklistItemRepository = checklistItemRepository;
        _checklistItemQuestionRepository = checklistItemQuestionRepository;
    }

    public async Task<Guid> Handle(AddItemCommand request, CancellationToken cancellationToken)
    {
        var mobilization = await _mobilizationRepository.GetMobilizationById(request.MobilizationId)
            ?? throw new NotFoundException(nameof(Mobilization), request.MobilizationId);

        var itemTemplate = await _templateRepository.GetTemplateByItemId(request.ItemId, cancellationToken)
            ?? throw new NotFoundException("Item", request.ItemId);

        var checklistItem = await AddCheckListItem(mobilization, itemTemplate, cancellationToken);

        await AddPartToChecklist(request, mobilization, cancellationToken);

        return checklistItem.Id;
    }

    private async Task<ChecklistItem> AddCheckListItem(Mobilization mobilization, ItemTemplate itemTemplate, CancellationToken cancellationToken)
    {
        var checklistItem = new ChecklistItem(itemTemplate, mobilization.Checklist.Id);

        await _checklistItemRepository.AddChecklistItem(checklistItem, cancellationToken);

        await AddQuestions(itemTemplate, checklistItem, cancellationToken);

        return checklistItem;
    }

    private async Task AddQuestions(ItemTemplate itemTemplate, ChecklistItem checklistItem, CancellationToken cancellationToken)
    {
        var checklistItemQuestions = itemTemplate.Questions
            .Select(q => new ChecklistItemQuestion(q, checklistItem.Id))
            .ToList();

        foreach (var question in checklistItemQuestions)
        {
            await _checklistItemQuestionRepository.AddQuestion(question, cancellationToken);
        }
    }

    private async Task AddPartToChecklist(AddItemCommand request, Mobilization mobilization, CancellationToken cancellationToken)
    {
        mobilization.Checklist.Parts.Add(request.ItemId);

        await _mobilizationRepository.SaveChanges(cancellationToken);
    }
}
