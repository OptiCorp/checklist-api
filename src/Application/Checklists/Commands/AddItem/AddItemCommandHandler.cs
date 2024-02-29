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
    private readonly IChecklistRepository _checklistRepository;
    private readonly IChecklistQuestionRepository _checklistQuestionRepository;

    public AddItemCommandHandler(
        IMobilizationRepository mobilizationRepository,
        ITemplateRepository templateRepository,
        IChecklistRepository checklistRepository,
        IChecklistQuestionRepository checklistQuestionRepository)
    {
        _mobilizationRepository = mobilizationRepository;
        _templateRepository = templateRepository;
        _checklistRepository = checklistRepository;
        _checklistQuestionRepository = checklistQuestionRepository;
    }

    public async Task<Guid> Handle(AddItemCommand request, CancellationToken cancellationToken)
    {
        var mobilization = await _mobilizationRepository.GetMobilizationById(request.MobilizationId)
            ?? throw new NotFoundException(nameof(Mobilization), request.MobilizationId);

        var itemTemplate = await _templateRepository.GetTemplateByItemId(request.ItemId, cancellationToken)
            ?? throw new NotFoundException(nameof(ItemTemplate), request.ItemId);

        var checklist = await AddCheckList(mobilization, itemTemplate, cancellationToken);

        //await AddPartToChecklist(request, mobilization, cancellationToken);

        return checklist.Id;
    }

    private async Task<Checklist> AddCheckList(Mobilization mobilization, ItemTemplate itemTemplate, CancellationToken cancellationToken)
    {
        var checklist = new Checklist(itemTemplate, mobilization.ChecklistCollectionId);

        await _checklistRepository.AddChecklist(checklist, cancellationToken);

        await AddQuestions(itemTemplate, checklist, cancellationToken);

        return checklist;
    }

    private async Task AddQuestions(ItemTemplate itemTemplate, Checklist checklist, CancellationToken cancellationToken)
    {
        var checklistQuestions = itemTemplate.Questions
            .Select(q => new ChecklistQuestion(q, checklist.Id))
            .ToList();

        
        foreach (var question in checklistQuestions)
        {
            await _checklistQuestionRepository.AddQuestion(question, cancellationToken);
        }
    }

    // //TODO: validate ItemId string
    // private async Task AddPartToChecklist(AddItemCommand request, Mobilization mobilization, CancellationToken cancellationToken)
    // {
    //     mobilization.Checklist.Parts.Add(request.ItemId);

    //     await _mobilizationRepository.SaveChanges(cancellationToken);
    // }
}
