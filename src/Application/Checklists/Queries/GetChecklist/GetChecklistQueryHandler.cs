
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
    private readonly IMobilizationRepository _mobilizationRepository;

    private readonly IChecklistRepository _checklistRepository;

    private readonly IChecklistQuestionRepository _checklistQuestionRepository;

    private readonly IPunchRepository _punchRepository;


    public GetChecklistQueryHandler(IMobilizationRepository mobilizationRepository, IChecklistRepository checklistRepository, IPunchRepository punchRepository, IChecklistQuestionRepository checklistQuestionRepository)
    {
        _mobilizationRepository = mobilizationRepository;
        _checklistRepository = checklistRepository;
        _punchRepository = punchRepository;
        _checklistQuestionRepository = checklistQuestionRepository;
    } 

    public async Task<ChecklistDto> Handle(GetChecklistQuery request, CancellationToken cancellationToken)
    {
        // var mobilization = await _mobilizationRepository.GetMobilizationById(request.MobilizationId, cancellationToken)
        //     ?? throw new NotFoundException(nameof(Mobilization), request.MobilizationId);
        
        var checklist = await _checklistRepository.GetSingleChecklist(request.ChecklistId, cancellationToken) ??
             throw new NotFoundException(nameof(Checklist), request.ChecklistId);

        var checklistItemQuestions = await _checklistQuestionRepository.GetQuestionsByChecklistId(checklist.Id, cancellationToken);

        checklist.SetQuestions(checklistItemQuestions);

        // foreach(var q in checklistItem.Questions) //TODO:
        // {
        //     q.SetQuestion(q.QuestionTemplate.Question);
        // }
        checklist.SetPunchesCount(await _punchRepository.GetPunchesCount(checklist.Id, cancellationToken));
        
        var checklistDto = checklist.Adapt<ChecklistDto>();
        //var checklist = mobilization.Checklist;
        
        //await _checklistItemRepository.LoadChecklistItems(checklist, cancellationToken);
        return checklistDto; 
    } 
}
