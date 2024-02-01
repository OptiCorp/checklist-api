
using MediatR;
using MobDeMob.Domain.ItemAggregate;

namespace MobDeMob.Application.Parts.Queries
{
    public class GetItemByIdQuery : IRequest<Part>
    {
        public required string Id { get; init; }
    }
}