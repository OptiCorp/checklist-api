using Application.Checklists.Dtos;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities.ChecklistAggregate;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.Checklists.Commands.SetChecklistCheckedValue;

public class SetChecklistCheckedValueCommandHandler : IRequestHandler<SetChecklistCheckedValueCommand>
{

    private readonly IMobilizationRepository _mobilizationRepository;
    private readonly ITemplateRepository _templateRepository;
    private readonly IChecklistItemRepository _checklistItemRepository;
    private readonly IChecklistItemQuestionRepository _checklistItemQuestionRepository;

    private readonly IMapper _mapper;

    public SetChecklistCheckedValueCommandHandler(
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
    public async Task Handle(SetChecklistCheckedValueCommand request, CancellationToken cancellationToken)
    {
        var checklistItemQuestion = await _checklistItemQuestionRepository.GetQuestion(request.Id, cancellationToken) ?? throw new NotFoundException(nameof(ChecklistItemQuestion), request.Id);
        ChangeChecklistItem(_mapper.Map<ChecklistItemQuestionDto>(checklistItemQuestion), checklistItemQuestion, request);
        await _checklistItemQuestionRepository.SaveChanges(cancellationToken);
    }

    public void ChangeChecklistItem(ChecklistItemQuestionDto qDto, ChecklistItemQuestion q, SetChecklistCheckedValueCommand request)
    {
        //q.Checked = request.CheckedValue;
        request.Patch.ApplyTo(qDto);
        // q.Id = qDto.Id;
        // q.ChecklistItemId = qDto.ChecklistItemId;
        // q.QuestionTemplateId = qDto.QuestionTemplateId;
        // q.Checked = qDto.Checked;
    }
}