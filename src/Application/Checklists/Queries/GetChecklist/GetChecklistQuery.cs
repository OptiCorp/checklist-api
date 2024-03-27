
using Application.Checklists.Dtos;
using MediatR;

namespace Application.Checklists.Queries;

public class GetChecklistQuery : IRequest<ChecklistDto?>
{
    public Guid ChecklistId {get; init;} 
}
