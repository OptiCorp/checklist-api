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

public class SetChecklistStatusCommandHandler : IRequestHandler<SetChecklistStatusCommand>
{
    private readonly IChecklistRepository _checklistRepository;

    private readonly IChecklistQuestionRepository _checklistQuestionRepository;


    public SetChecklistStatusCommandHandler(

        IChecklistRepository checklistRepository,
        IChecklistQuestionRepository checklistQuestionRepository
        )
    {
        _checklistRepository = checklistRepository;
        _checklistQuestionRepository = checklistQuestionRepository;
    }
    public async Task Handle(SetChecklistStatusCommand request, CancellationToken cancellationToken)
    {
        var checklist = await _checklistRepository.GetSingleChecklist(request.ChecklistId, cancellationToken)
         ?? throw new NotFoundException(nameof(ChecklistQuestion), request.ChecklistId);

        if (request.Status == ChecklistStatus.Completed) {
            var questions = await _checklistQuestionRepository.GetQuestionsByChecklistId(request.ChecklistId, cancellationToken);
            if (questions.Any(q => !q.Checked && !q.NotApplicable)) throw new Exception("This cannot be set as completed");
        }

        checklist.SetChecklistStatus(request.Status);
        await _checklistRepository.SaveChanges(cancellationToken);
    }
}