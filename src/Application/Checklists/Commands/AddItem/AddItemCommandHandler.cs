using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Entities.ChecklistAggregate;
using MediatR;
using MobDeMob.Application.Common.Interfaces;
using MobDeMob.Domain.Entities;
using MobDeMob.Domain.ItemAggregate;

namespace Application.Checklists.Commands.AddItem;

public class AddItemCommandHandler : IRequestHandler<AddItemCommand, Guid>
{
    private readonly IMobilizationRepository _mobilizationRepository;

    private readonly IItemReposiory _itemReposiory;

    private readonly IChecklistRepository _checklistRepository;
    private readonly IChecklistQuestionRepository _checklistQuestionRepository;

    private readonly IChecklistTemplateRepository _checklistTemplateRepository;


    public AddItemCommandHandler(
        IMobilizationRepository mobilizationRepository,
        IChecklistRepository checklistRepository,
        IChecklistQuestionRepository checklistQuestionRepository,
        IItemReposiory itemReposiory,
        IChecklistTemplateRepository checklistTemplateRepository
        
        )
    {
        _mobilizationRepository = mobilizationRepository;
        _checklistRepository = checklistRepository;
        _checklistQuestionRepository = checklistQuestionRepository;
        _itemReposiory = itemReposiory;
        _checklistTemplateRepository = checklistTemplateRepository;
    }

    public async Task<Guid> Handle(AddItemCommand request, CancellationToken cancellationToken)
    {
        var mobilization = await _mobilizationRepository.GetMobilizationById(request.MobilizationId)
            ?? throw new NotFoundException(nameof(Mobilization), request.MobilizationId);

        var item = await _itemReposiory.GetItemById(request.ItemId, cancellationToken)
            ?? throw new NotFoundException(nameof(Item), request.ItemId);
        
        //TODO: fix exception
        var checklistTemplate = await _checklistTemplateRepository.GetChecklistTemplateByItemTemplateId(item.ItemTemplate.Id) 
            ?? throw new Exception($"Missing ChecklistTemplate for itemTemplate with id: {item.ItemTemplate.Id}");
       

        var checklist = await AddCheckList(mobilization, item.Id, checklistTemplate, cancellationToken);

        return checklist.Id;
    }

    private async Task<Checklist> AddCheckList(Mobilization mobilization, string itemId, ChecklistTemplate checklistTemplate,CancellationToken cancellationToken = default)
    {
        var checklist = Checklist.New(itemId, mobilization.ChecklistCollectionId, checklistTemplate.Id);

        await _checklistRepository.AddChecklist(checklist, cancellationToken);

        await AddQuestions(checklist.Id, checklistTemplate, cancellationToken);

        return checklist;
    }

    private async Task AddQuestions(Guid checklistId, ChecklistTemplate checklistTemplate, CancellationToken cancellationToken = default)
    {
        var checklistQuestions = checklistTemplate.Questions
            .Select(q => new ChecklistQuestion(q, checklistId))
            .ToList();

        
        foreach (var question in checklistQuestions)
        {
            await _checklistQuestionRepository.AddQuestion(question, cancellationToken);
        }
    }
}
