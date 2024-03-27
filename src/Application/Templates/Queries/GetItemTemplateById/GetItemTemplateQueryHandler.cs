using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Templates.Models;
using Domain.Entities;
using Domain.Entities.TemplateAggregate;
using Mapster;
using MediatR;
using MobDeMob.Domain.ItemAggregate;

namespace Application.Templates.GetById;

public class GetChecklistTemplateQueryHandler : IRequestHandler<GetItemTemplateQuery, ItemTemplateDto?>
{
    private readonly IItemTemplateRepository _itemtTemplateRepository;

    private readonly IItemReposiory _itemRepository;

    public GetChecklistTemplateQueryHandler(IItemTemplateRepository templateRepository, IItemReposiory itemReposiory)
    {
        _itemtTemplateRepository = templateRepository;
        _itemRepository = itemReposiory;
    }

    public async Task<ItemTemplateDto?> Handle(GetItemTemplateQuery request, CancellationToken cancellationToken)
    {
        var itemTemplate = await _itemtTemplateRepository.GetTemplateById(request.ItemTemplateId, cancellationToken)
            ?? throw new NotFoundException(nameof(ItemTemplate), request.ItemTemplateId);
    

         if (itemTemplate == null) return null; //this means item has not ItemTemplate (checklistTemplate)
            
        return itemTemplate.Adapt<ItemTemplateDto>();
    }
}
