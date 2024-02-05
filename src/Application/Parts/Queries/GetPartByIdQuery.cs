
using MediatR;
using MobDeMob.Domain.ItemAggregate;

namespace MobDeMob.Application.Parts.Queries
{
    public class GetPartByIdQuery : IRequest<PartDto>
    {
        public required string Id { get; init; }
    }
}