using MediatR;
using MobDeMob.Domain.ItemAggregate;

namespace Application.Templates.GetById;

public class GetByIdCommand : IRequest<ItemTemplateDto?>
{
    public Guid TemplateId { get; set; }
}
