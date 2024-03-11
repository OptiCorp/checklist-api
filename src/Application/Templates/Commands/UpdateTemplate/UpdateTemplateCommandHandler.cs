using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities.ChecklistAggregate;
using Domain.Entities.TemplateAggregate;
using MediatR;
using MobDeMob.Domain.Enums;
using MobDeMob.Domain.ItemAggregate;

namespace Application.Templates.UpdateTemplate;


public class UpdateTemplateCommandHandler : IRequestHandler<UpdateTemplateCommand>
{
    private readonly ITemplateRepository _templateRepository;

    private readonly IChecklistRepository _checklistRepository;

    private readonly IChecklistQuestionRepository _checklistQuestionRepository;



    public UpdateTemplateCommandHandler(ITemplateRepository templateRepository, IChecklistRepository checklistRepository, IChecklistQuestionRepository checklistQuestionRepository)
    {
        _templateRepository = templateRepository;
        _checklistRepository = checklistRepository;
        _checklistQuestionRepository = checklistQuestionRepository;
    }

    public async Task Handle(UpdateTemplateCommand request, CancellationToken cancellationToken)
    {
        var template = await _templateRepository.GetTemplateByItemId(request.ItemId, cancellationToken)
            ?? throw new NotFoundException(nameof(ItemTemplate), request.ItemId);

        var checklists = await _checklistRepository.GetChecklistByItemTemplateId(template.Id, cancellationToken);

        if (!checklists.Any()) throw new Exception("No checklists were found based on the itemTemplateId");

        UpdateTemplate(template, request); //this will delete the previous questionTemplate which deletes all associated ChecklistQuestions

        await _templateRepository.SaveChanges(cancellationToken);

       

        foreach (var checklist in checklists)
        {
            checklist.SetChecklistStatus(ChecklistStatus.NotStarted);
            await AddQuestions(template, checklist, cancellationToken);
        }
    }

    private static ItemTemplate UpdateTemplate(ItemTemplate template, UpdateTemplateCommand request)
    {
        template.UpdateQuestions(request.Questions);

        return template;
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
}
