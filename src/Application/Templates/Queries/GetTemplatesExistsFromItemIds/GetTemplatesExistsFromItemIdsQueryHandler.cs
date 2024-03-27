using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities.TemplateAggregate;
using Mapster;
using MediatR;
using MobDeMob.Domain.ItemAggregate;

namespace Application.Templates.Queries;

public class GetItemTemplatesExistsFromItemIdsQueryHandler : IRequestHandler<GetTemplatesExistsFromItemIdsQuery, IEnumerable<TemplateExistsReponse>>
{
    private readonly IItemReposiory _itemReposiory;

    public GetItemTemplatesExistsFromItemIdsQueryHandler(IItemReposiory itemReposiory)
    {
        _itemReposiory = itemReposiory;
    }

    public async Task<IEnumerable<TemplateExistsReponse>> Handle(GetTemplatesExistsFromItemIdsQuery request, CancellationToken cancellationToken)
    {
        var itemsHasCheckistTemplate = await _itemReposiory.ChecklistTemplateExistsForItemIds(request.ItemIds, cancellationToken);

        if (request.ItemIds.Count() != itemsHasCheckistTemplate.Count)
        {
            foreach(var id in request.ItemIds)
            {
                if(!itemsHasCheckistTemplate.ContainsKey(id))
                {
                    throw new Exception($"id: {id} does not exist");
                }
            }
        } 

        return itemsHasCheckistTemplate
            .Select(templateDict => TemplateExistsReponse.New(templateDict.Key, templateDict.Value)).ToList();
    }
}
