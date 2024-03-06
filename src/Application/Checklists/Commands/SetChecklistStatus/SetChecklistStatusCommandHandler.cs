using Application.Checklists.Dtos;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities.ChecklistAggregate;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Application.Checklists.Commands.SetChecklistCheckedValue;

public class SetChecklistStatusCommandHandler : IRequestHandler<SetChecklistStatusCommand>
{

    private readonly IMobilizationRepository _mobilizationRepository;
    private readonly ITemplateRepository _templateRepository;
    private readonly IChecklistRepository _checklistRepository;
    private readonly IChecklistQuestionRepository _checklistQuestionRepository;


    public SetChecklistStatusCommandHandler(
        IMobilizationRepository mobilizationRepository,
        ITemplateRepository templateRepository,
        IChecklistRepository checklistRepository,
        IChecklistQuestionRepository checklistQuestionRepository
        )
    {
        _mobilizationRepository = mobilizationRepository;
        _templateRepository = templateRepository;
        _checklistRepository = checklistRepository;
        _checklistQuestionRepository = checklistQuestionRepository;
    }

    public async Task Handle(SetChecklistStatusCommand request, CancellationToken cancellationToken)
    {
        var checklist = await _checklistRepository.GetSingleChecklist(request.ChecklistId, cancellationToken)
         ?? throw new NotFoundException(nameof(ChecklistQuestion), request.ChecklistId);

        checklist.SetChecklistStatus(request.Status);
        await _checklistRepository.SaveChanges(cancellationToken);
    }
}