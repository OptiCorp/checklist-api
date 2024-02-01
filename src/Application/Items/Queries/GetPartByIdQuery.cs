
using MediatR;
using MobDeMob.Domain.Entities.ItemAggregate;

namespace Checklist.Application.Items.Queries
{
    public class GetItemByIdQuery : IRequest<Item>
    {
        public required string Id { get; init; }
    }
}