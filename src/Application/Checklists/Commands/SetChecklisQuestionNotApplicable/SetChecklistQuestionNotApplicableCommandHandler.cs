using Application.Checklists.Dtos;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities.ChecklistAggregate;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MobDeMob.Domain.Enums;

namespace Application.Checklists.Commands.SetChecklistCheckedValue;

public class SetChecklistNotApplicableValueCommandHandler : IRequestHandler<SetChecklistQuestionNotApplicableCommand>
{

    private readonly IChecklistQuestionRepository _checklistQuestionRepository;

    private readonly IChecklistRepository _checklistRepository;


    public SetChecklistNotApplicableValueCommandHandler(
        IChecklistQuestionRepository checklistQuestionRepository,
        IChecklistRepository checklistRepository
        )
    {
        _checklistQuestionRepository = checklistQuestionRepository;
        _checklistRepository = checklistRepository; 
    }
    public async Task Handle(SetChecklistQuestionNotApplicableCommand request, CancellationToken cancellationToken)
    {
        var checklist = await _checklistRepository.GetSingleChecklist(request.checklistId, cancellationToken)
            ?? throw new NotFoundException(nameof(Checklist), request.checklistId);

        var checklistQuestion = await _checklistQuestionRepository.GetQuestion(request.checklistQuestionId, cancellationToken)
            ?? throw new NotFoundException(nameof(ChecklistQuestion), request.checklistQuestionId);

        checklist.SetChecklistStatus(ChecklistStatus.InProgress);

        checklistQuestion.MarkQuestionAsNotApplicable(request.value);
        await _checklistQuestionRepository.SaveChanges(cancellationToken);
    }

}