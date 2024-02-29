using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities.TemplateAggregate;
using Mapster;
using MediatR;
using MobDeMob.Domain.ItemAggregate;

namespace Application.Templates.GetById;

public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, ItemTemplateDto?>
{
    private readonly ITemplateRepository _templateRepository;

    public GetByIdQueryHandler(ITemplateRepository templateRepository)
    {
        _templateRepository = templateRepository;
    }

    public async Task<ItemTemplateDto?> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        var itemTemplate =  await _templateRepository.GetTemplateById(request.TemplateId, cancellationToken) 
            ?? throw new NotFoundException(nameof(ItemTemplate), request.TemplateId);
            
        return itemTemplate.Adapt<ItemTemplateDto>();
    }
}
