using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Entities.TemplateAggregate;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using MobDeMob.Application.Common.Interfaces;
using MobDeMob.Domain.ItemAggregate;

namespace Application.Templates.Commands;

public class AddChecklistTemplateQuestionCommandHandler : IRequestHandler<AddChecklistTemplateQuestionCommand, Guid>
{
    private readonly IChecklistTemplateRepository _checklistTemplateRepository;

    private readonly IChecklistQuestionRepository _checklistQuestionRepository;

    public AddChecklistTemplateQuestionCommandHandler(IChecklistTemplateRepository checklistTemplateRepository, IChecklistQuestionRepository checklistQuestionRepository)
    {
        _checklistTemplateRepository = checklistTemplateRepository;
        _checklistQuestionRepository = checklistQuestionRepository;
    }

    public async Task<Guid> Handle(AddChecklistTemplateQuestionCommand request, CancellationToken cancellationToken)
    {
        var checklistTemplate = await _checklistTemplateRepository.GetChecklistTemplateById(request.checklistTemplateId, cancellationToken)
            ?? throw new NotFoundException(nameof(ChecklistTemplate), request.checklistTemplateId); 
        
        var checklistQuestions = await _checklistQuestionRepository.GetQuestionsByQuestionTemplateId(checklistTemplate.Id, cancellationToken);

        if (checklistQuestions.Any() && request.conflictOption == null) //TODO:
            throw new Exception($"Found conflict for questionTemplate {request.checklistTemplateId} because is belongs to one or checklistQuestions but no action was specified");

        else if (checklistQuestions.Any() && request.conflictOption != null) 
        {
            //TODO: 
            var conflictAction = request.conflictOption;
            switch (conflictAction)
            {
                case QuestionTemplateAddConflictOptions.DoNothing:
                    break;
                case QuestionTemplateAddConflictOptions.AddToAllChecklistQuestionsAndSetStatusInProgress:
                    break;
            }
        }
        var newQuestionTemplate = QuestionTemplate.New(request.question);

        checklistTemplate.AddQuestionTemplate(newQuestionTemplate);

        await _checklistTemplateRepository.SaveChanges(cancellationToken);

        return newQuestionTemplate.Id;        
    }

}
