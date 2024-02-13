using MediatR;
using MobDeMob.Domain.ItemAggregate;

namespace Application.Templates.GetById;

public class GetByIdCommand : IRequest<ItemTemplate?>
{
    public Guid TemplateId { get; set; }
}
