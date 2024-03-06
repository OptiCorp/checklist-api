
using Application.Checklists.Dtos;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Punches.Dtos;
using Domain.Entities.ChecklistAggregate;
using Mapster;
using MediatR;
using MobDeMob.Domain.Entities;
using MobDeMob.Domain.ItemAggregate;

namespace Application.Checklists.Queries;

public class GetChecklistsQueryHandler : IRequestHandler<GetChecklistsQuery, PaginatedList<ChecklistBriefDto>> 
{
    private readonly IMobilizationRepository _mobilizationRepository;
    private readonly IChecklistRepository _checklistRepository;

    public GetChecklistsQueryHandler(IMobilizationRepository mobilizationRepository, IChecklistRepository checklistRepository)
    {
        _mobilizationRepository = mobilizationRepository;
        _checklistRepository = checklistRepository; 
    }
    public async Task<PaginatedList<ChecklistBriefDto>> Handle(GetChecklistsQuery request, CancellationToken cancellationToken)
    {
        var mobilization = await _mobilizationRepository.GetMobilizationById(request.MobilizationId, cancellationToken)
            ?? throw new NotFoundException(nameof(Mobilization), request.MobilizationId);

        var checklistsPaginated = await _checklistRepository.GetChecklistsWithPaginationFromChecklistCollection(mobilization.ChecklistCollectionId, request.PageNumber, request.PageSize, cancellationToken);

        var mobilizationsPaginatedDtos = new PaginatedList<ChecklistBriefDto>(
                checklistsPaginated.Items.AsQueryable().ProjectToType<ChecklistBriefDto>(),
                checklistsPaginated.TotalCount,
                checklistsPaginated.PageNumber,
                checklistsPaginated.TotalPages
        );
        return mobilizationsPaginatedDtos;
    }

}
