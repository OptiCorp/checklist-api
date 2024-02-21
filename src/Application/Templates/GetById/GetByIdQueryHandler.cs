using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities.TemplateAggregate;
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
            ?? throw new NotFoundException(nameof(ItemTemplate), request.TemplateId);;
        return MapToTemplateDto(itemTemplate, itemTemplate.Questions);
    }

    private static ItemTemplateDto MapToTemplateDto(ItemTemplate itemTemplate, IEnumerable<QuestionTemplate> questions)
    {
        return new ItemTemplateDto(questions)
        {
            Id = itemTemplate.Id,
            ItemId = itemTemplate.ItemId,
            Name = itemTemplate.Name,
            Type = itemTemplate.Type,
            Created = itemTemplate.Created,
            CreatedBy = itemTemplate.CreatedBy,
            Description = itemTemplate.Description,
            LastModified = itemTemplate.LastModified,
            LastModifiedBy = itemTemplate.LastModifiedBy,
            Revision = itemTemplate.Revision,
        };
    }
}
