
using Application.Checklists.Dtos;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Punches;
using Domain.Entities.ChecklistAggregate;
using Mapster;
using MediatR;

namespace Application.Checklists.Queries;

public class GetChecklistQueryHandler : IRequestHandler<GetChecklistQuery, ChecklistDto>
{
    private readonly IChecklistRepository _checklistRepository;

    private readonly IChecklistQuestionRepository _checklistQuestionRepository;

    private readonly IPunchRepository _punchRepository;


    public GetChecklistQueryHandler(IChecklistRepository checklistRepository, IPunchRepository punchRepository, IChecklistQuestionRepository checklistQuestionRepository)
    {
        _checklistRepository = checklistRepository;
        _punchRepository = punchRepository;
        _checklistQuestionRepository = checklistQuestionRepository;
    }

    public async Task<ChecklistDto> Handle(GetChecklistQuery request, CancellationToken cancellationToken)
    {
        var checklist = await _checklistRepository.GetSingleChecklist(request.ChecklistId, cancellationToken) ??
             throw new NotFoundException(nameof(Checklist), request.ChecklistId);

        var checklistItemQuestions = await _checklistQuestionRepository.GetQuestionsByChecklistId(checklist.Id, cancellationToken);

        checklist.SetQuestions(checklistItemQuestions);

        checklist.SetPunchesCount(await _punchRepository.GetPunchesCount(checklist.Id, cancellationToken));

        var checklistDto = checklist.Adapt<ChecklistDto>();

        return checklistDto;
    }
}
