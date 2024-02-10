using MediatR;
using MobDeMob.Domain.ItemAggregate;

namespace Application.Templates.GetById;

public class GetByIdCommand : IRequest<ItemTemplate>
{
    public string TemplateId { get; set; }
}
