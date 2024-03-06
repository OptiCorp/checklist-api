using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities.TemplateAggregate;
using Mapster;
using MediatR;
using MobDeMob.Domain.ItemAggregate;

namespace Application.Templates.GetById;

public class GetTemplateQueryHandler : IRequestHandler<GetTemplateQuery, ItemTemplateDto?>
{
    private readonly ITemplateRepository _templateRepository;

    public GetTemplateQueryHandler(ITemplateRepository templateRepository)
    {
        _templateRepository = templateRepository;
    }

    public async Task<ItemTemplateDto?> Handle(GetTemplateQuery request, CancellationToken cancellationToken)
    {
        // var itemTemplate =  await _templateRepository.GetTemplateByItemId(request.ItemId, cancellationToken) 
        //     ?? throw new NotFoundException(nameof(ItemTemplate), request.ItemId);

         var itemTemplate =  await _templateRepository.GetTemplateByItemId(request.ItemId, cancellationToken);

         if (itemTemplate == null) return null;
            
        return itemTemplate.Adapt<ItemTemplateDto>();
    }
}
