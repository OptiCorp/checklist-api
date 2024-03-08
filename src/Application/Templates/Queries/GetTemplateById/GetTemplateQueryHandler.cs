using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Entities.TemplateAggregate;
using Mapster;
using MediatR;
using MobDeMob.Domain.ItemAggregate;

namespace Application.Templates.GetById;

public class GetTemplateQueryHandler : IRequestHandler<GetTemplateQuery, ItemTemplateDto?>
{
    private readonly ITemplateRepository _templateRepository;

    private readonly IItemReposiory _itemRepository;

    public GetTemplateQueryHandler(ITemplateRepository templateRepository, IItemReposiory itemReposiory)
    {
        _templateRepository = templateRepository;
        _itemRepository = itemReposiory;
    }

    public async Task<ItemTemplateDto?> Handle(GetTemplateQuery request, CancellationToken cancellationToken)
    {
        var item = await _itemRepository.GetItemById(request.ItemId, cancellationToken)
            ?? throw new NotFoundException(nameof(Item), request.ItemId);

         var itemTemplate =  await _templateRepository.GetTemplateByItemId(request.ItemId, cancellationToken);

         if (itemTemplate == null) return null; //this means item has not ItemTemplate (checklistTemplate)
            
        return itemTemplate.Adapt<ItemTemplateDto>();
    }
}
