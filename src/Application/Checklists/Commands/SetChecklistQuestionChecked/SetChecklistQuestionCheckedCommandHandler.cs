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

public class SetChecklistCheckedValueCommandHandler : IRequestHandler<SetChecklistQuestionCheckedCommand>
{
    private readonly IChecklistQuestionRepository _checklistQuestionRepository;

    private readonly IChecklistRepository _checklistRepository;


    public SetChecklistCheckedValueCommandHandler(
        IChecklistQuestionRepository checklistQuestionRepository,
        IChecklistRepository checklistRepository
        )
    {
        _checklistQuestionRepository = checklistQuestionRepository;
        _checklistRepository = checklistRepository; 
    }
    public async Task Handle(SetChecklistQuestionCheckedCommand request, CancellationToken cancellationToken)
    {
        var checklist = await _checklistRepository.GetSingleChecklist(request.ChecklistId, cancellationToken)
            ?? throw new NotFoundException(nameof(ChecklistQuestion), request.ChecklistId);

        var checklistQuestion = await _checklistQuestionRepository.GetQuestion(request.ChecklistQuestionId, cancellationToken)
            ?? throw new NotFoundException(nameof(ChecklistQuestion), request.ChecklistQuestionId);

        checklist.SetChecklistStatus(ChecklistStatus.InProgress);


        checklistQuestion.MarkQuestionAsCheckedOrUnChecked(request.Value);
        await _checklistQuestionRepository.SaveChanges(cancellationToken);
    }

}