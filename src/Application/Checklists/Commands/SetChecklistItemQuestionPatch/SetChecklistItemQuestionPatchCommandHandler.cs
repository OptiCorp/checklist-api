using Application.Checklists.Dtos;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities.ChecklistAggregate;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Application.Checklists.Commands.SetChecklistCheckedValue;

public class SetChecklistCheckedValueCommandHandler : IRequestHandler<SetChecklistItemQuestionPatchCommand>
{

    private readonly IMobilizationRepository _mobilizationRepository;
    private readonly ITemplateRepository _templateRepository;
    private readonly IChecklistItemRepository _checklistItemRepository;
    private readonly IChecklistItemQuestionRepository _checklistItemQuestionRepository;


    public SetChecklistCheckedValueCommandHandler(
        IMobilizationRepository mobilizationRepository,
        ITemplateRepository templateRepository,
        IChecklistItemRepository checklistItemRepository,
        IChecklistItemQuestionRepository checklistItemQuestionRepository
        )
    {
        _mobilizationRepository = mobilizationRepository;
        _templateRepository = templateRepository;
        _checklistItemRepository = checklistItemRepository;
        _checklistItemQuestionRepository = checklistItemQuestionRepository;
    }
    public async Task Handle(SetChecklistItemQuestionPatchCommand request, CancellationToken cancellationToken)
    {
        var checklistItemQuestion = await _checklistItemQuestionRepository.GetQuestion(request.Id, cancellationToken) 
            ?? throw new NotFoundException(nameof(ChecklistItemQuestion), request.Id);
        ChangeChecklistItem(checklistItemQuestion, request.Patches);
        request.Patches.ApplyTo(checklistItemQuestion);
        await _checklistItemQuestionRepository.SaveChanges(cancellationToken);
    }

    //TODO: should not be able to change "checked" to true if NotApplicable is also true, not sure how to validate this with Patch
    public void ChangeChecklistItem(ChecklistItemQuestion q, JsonPatchDocument<ChecklistItemQuestion> patches)
    {
        patches.ApplyTo(q);
    }
}