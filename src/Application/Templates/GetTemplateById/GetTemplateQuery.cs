using MediatR;
using MobDeMob.Domain.ItemAggregate;

namespace Application.Templates.GetById;

public class GetTemplateQuery : IRequest<ItemTemplateDto?>
{
    public string ItemId { get; init; }

}
