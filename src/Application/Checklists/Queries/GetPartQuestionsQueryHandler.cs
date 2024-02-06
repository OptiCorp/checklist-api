
using MediatR;
using MobDeMob.Application.Common.Interfaces;
using MobDeMob.Application.Mobilizations;
using MobDeMob.Application.Parts.Queries;
using MobDeMob.Application.Templates;
using MobDeMob.Domain.ItemAggregate;

namespace MobDeMob.Application.Items.Queries;

public class GetPartQuestionsQueryHandler : IRequestHandler<GetPartQuestionsQuery, IEnumerable<ChecklistSectionTemplateDto>?>
{
    private readonly IChecklistRepository _checkListRepository;


    public GetPartQuestionsQueryHandler(IChecklistRepository checkListRepository)
    {
        _checkListRepository = checkListRepository;
    }

    public async Task<IEnumerable<ChecklistSectionTemplateDto>?> Handle(GetPartQuestionsQuery request, CancellationToken cancellationToken)
    {
        var sections =  await _checkListRepository.GetQuestions(request.Id, cancellationToken);
        if (sections == null) return null;
        return sections.Select(s => s.AsDto());
    }
}