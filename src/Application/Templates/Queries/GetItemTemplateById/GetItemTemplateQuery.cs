using Application.Templates.Models;
using MediatR;
using MobDeMob.Domain.ItemAggregate;

namespace Application.Templates.GetById;

public class GetItemTemplateQuery : IRequest<ItemTemplateDto?>
{
    public string ItemTemplateId { get; init; }
}
