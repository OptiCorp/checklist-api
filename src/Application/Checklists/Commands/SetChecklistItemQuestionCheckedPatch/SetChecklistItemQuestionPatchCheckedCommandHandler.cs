using Application.Checklists.Dtos;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities.ChecklistAggregate;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Application.Checklists.Commands.SetChecklistCheckedValue;

public class SetChecklistCheckedValueCommandHandler : IRequestHandler<SetChecklistItemQuestionPatchCheckedCommand>
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
    public async Task Handle(SetChecklistItemQuestionPatchCheckedCommand request, CancellationToken cancellationToken)
    {
        var checklistItemQuestion = await _checklistItemQuestionRepository.GetQuestion(request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(ChecklistItemQuestion), request.Id);

        request.Patches.ApplyTo(checklistItemQuestion, request.ModelState);
        if (!request.ModelState.IsValid) return;
        ValidateChecklistItemCheckedQuestion(checklistItemQuestion, request.Patches, request.ModelState);
        //request.Patches.ApplyTo(checklistItemQuestion);
        await _checklistItemQuestionRepository.SaveChanges(cancellationToken);
    }

    public void ValidateChecklistItemCheckedQuestion(ChecklistItemQuestion q, JsonPatchDocument<ChecklistItemQuestion> patches, ModelStateDictionary modelState)
    {
       
        // if (q.IsQuestionCheckable())
        // {
            
        //     q.MarkQuestionAsChecked();
            
        // }

        if (q.Checked && q.NotApplicable) throw new Exception("Both Checked and NotApplicable cannot be true");

        // else
        // {
        //     throw new Exception("Cannot check the question");
        // }
    }
}