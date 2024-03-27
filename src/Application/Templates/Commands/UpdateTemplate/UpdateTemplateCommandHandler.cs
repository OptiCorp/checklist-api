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
    private readonly IQuestionTemplateRepository _questionTemplateRepository;

    public UpdateTemplateCommandHandler(IQuestionTemplateRepository questionTemplateRepository)
    {
        _questionTemplateRepository = questionTemplateRepository;
    }

    public async Task Handle(UpdateTemplateCommand request, CancellationToken cancellationToken)
    {
        // var template = await _templateRepository.GetTemplateByItemId(request.ItemId, cancellationToken)
        //     ?? throw new NotFoundException(nameof(ItemTemplate), request.ItemId);
        var questionTemplate = await _questionTemplateRepository.GetSingleQuestion(request.questionTemplateId, cancellationToken)
            ?? throw new NotFoundException(nameof(QuestionTemplate), request.questionTemplateId); ;

        await UpdateQuestionIfChange(questionTemplate, request.question, cancellationToken);
    }

    public async Task UpdateQuestionIfChange(QuestionTemplate questionTemplate, string newQuestion, CancellationToken cancellationToken)
    {
        if (questionTemplate.Question == newQuestion) return;

        questionTemplate.UpdateQuestion(newQuestion);
        await _questionTemplateRepository.SaveChanges(cancellationToken);
    }


    // private async Task AddQuestions(ItemTemplate itemTemplate, Checklist checklist, CancellationToken cancellationToken)
    // {
    //     var checklistQuestions = itemTemplate.Questions
    //         .Select(q => new ChecklistQuestion(q, checklist.Id))
    //         .ToList();


    //     foreach (var question in checklistQuestions)
    //     {
    //         await _checklistQuestionRepository.AddQuestion(question, cancellationToken);
    //     }
    // }
}
