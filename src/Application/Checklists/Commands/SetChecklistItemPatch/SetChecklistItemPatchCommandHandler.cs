using Application.Checklists.Dtos;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities.ChecklistAggregate;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Application.Checklists.Commands.SetChecklistCheckedValue;

public class SetChecklistItemPatchCommandHandler : IRequestHandler<SetChecklistItemPatchCommand>
{

    private readonly IMobilizationRepository _mobilizationRepository;
    private readonly ITemplateRepository _templateRepository;
    private readonly IChecklistItemRepository _checklistItemRepository;
    private readonly IChecklistItemQuestionRepository _checklistItemQuestionRepository;

    private readonly IMapper _mapper;

    public SetChecklistItemPatchCommandHandler(
        IMobilizationRepository mobilizationRepository,
        ITemplateRepository templateRepository,
        IChecklistItemRepository checklistItemRepository,
        IChecklistItemQuestionRepository checklistItemQuestionRepository,
        IMapper mapper
        )
    {
        _mobilizationRepository = mobilizationRepository;
        _templateRepository = templateRepository;
        _checklistItemRepository = checklistItemRepository;
        _checklistItemQuestionRepository = checklistItemQuestionRepository;
        _mapper = mapper;
    }
    public async Task Handle(SetChecklistItemPatchCommand request, CancellationToken cancellationToken)
    {
        var checklistItem = await _checklistItemRepository.GetChecklistItem(request.Id, cancellationToken) ?? throw new NotFoundException(nameof(ChecklistItemQuestion), request.Id);
        ChangeChecklistItem(checklistItem, request.Patches);
        request.Patches.ApplyTo(checklistItem);
        await _checklistItemQuestionRepository.SaveChanges(cancellationToken);
    }

    public void ChangeChecklistItem(ChecklistItem q, JsonPatchDocument<ChecklistItem> patches)
    {
        patches.ApplyTo(q);
    }
}