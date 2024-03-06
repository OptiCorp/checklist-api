using Application.Checklists.Dtos;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities.ChecklistAggregate;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Application.Checklists.Commands.SetChecklistCheckedValue;

public class SetChecklistNotApplicableValueCommandHandler : IRequestHandler<SetChecklistQuestionNotApplicableCommand>
{

    private readonly IChecklistQuestionRepository _checklistQuestionRepository;


    public SetChecklistNotApplicableValueCommandHandler(
        IChecklistQuestionRepository checklistQuestionRepository
        )
    {
        _checklistQuestionRepository = checklistQuestionRepository;
    }
    public async Task Handle(SetChecklistQuestionNotApplicableCommand request, CancellationToken cancellationToken)
    {
        var checklistQuestion = await _checklistQuestionRepository.GetQuestion(request.ChecklistQuestionId, cancellationToken)
            ?? throw new NotFoundException(nameof(ChecklistQuestion), request.ChecklistQuestionId);

        checklistQuestion.MarkQuestionAsNotApplicable(request.Value);
        await _checklistQuestionRepository.SaveChanges(cancellationToken);
    }

}