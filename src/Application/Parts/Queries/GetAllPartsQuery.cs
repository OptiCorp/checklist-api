
using MediatR;
using MobDeMob.Domain.ItemAggregate;

namespace MobDeMob.Application.Parts.Queries
{
    public class GetAllPartsQuery : IRequest<IEnumerable<PartDto>>
    {
        public bool includeChildren {get; set;}
    }
}