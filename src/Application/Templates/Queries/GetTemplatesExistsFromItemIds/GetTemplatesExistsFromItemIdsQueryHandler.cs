using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities.TemplateAggregate;
using Mapster;
using MediatR;
using MobDeMob.Domain.ItemAggregate;

namespace Application.Templates.Queries;

public class GetItemTemplatesExistsFromItemIdsQueryHandler : IRequestHandler<GetTemplatesExistsFromItemIdsQuery, IEnumerable<TemplateExistsReponse>>
{
    private readonly IItemTemplateRepository _templateRepository;

    public GetItemTemplatesExistsFromItemIdsQueryHandler(IItemTemplateRepository templateRepository)
    {
        _templateRepository = templateRepository;
    }

    public async Task<IEnumerable<TemplateExistsReponse>> Handle(GetTemplatesExistsFromItemIdsQuery request, CancellationToken cancellationToken)
    {
        var itemHasItemTemplate = await _templateRepository.ItemTemplateExistsForItemIds(request.ItemIds, cancellationToken);
        return itemHasItemTemplate
            .Select(templateDict => TemplateExistsReponse.New(templateDict.Key, templateDict.Value)).ToList();


    }
}
