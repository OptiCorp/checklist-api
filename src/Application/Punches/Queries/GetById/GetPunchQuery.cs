using Application.Punches.Dtos;
using MediatR;

namespace Application.Punches.Queries.GetById;

public class GetPunchQuery : IRequest<PunchDto?>
{
   public Guid punchId { get; set; }
}
