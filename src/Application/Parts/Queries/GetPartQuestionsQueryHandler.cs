
using MediatR;
using MobDeMob.Application.Common.Interfaces;
using MobDeMob.Application.Mobilizations;
using MobDeMob.Application.Parts.Queries;
using MobDeMob.Application.Templates;
using MobDeMob.Domain.ItemAggregate;

namespace MobDeMob.Application.Items.Queries;

public class GetPartQuestionsQueryHandler : IRequestHandler<GetPartQuestionsQuery, IEnumerable<ChecklistSectionTemplateDto>>
{
    private readonly IPartsRepository _partsRepository;


    public GetPartQuestionsQueryHandler(IPartsRepository partsRepository)
    {
        _partsRepository = partsRepository;
    }

    public async Task<IEnumerable<ChecklistSectionTemplateDto>> Handle(GetPartQuestionsQuery request, CancellationToken cancellationToken)
    {
        var sections =  await _partsRepository.GetQuestions(request.Id, cancellationToken);
        return sections.Select(s => s.AsDto());
    }
}