using MediatR;
using MobDeMob.Domain.ItemAggregate;

namespace Application.Templates.GetById;

public class GetByIdQuery : IRequest<ItemTemplateDto?>
{
    public Guid TemplateId { get; set; }
}
