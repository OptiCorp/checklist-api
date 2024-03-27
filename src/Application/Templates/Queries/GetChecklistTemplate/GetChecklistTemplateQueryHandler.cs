using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Templates.Models;
using Domain.Entities;
using Domain.Entities.TemplateAggregate;
using Mapster;
using MediatR;
using MobDeMob.Application.Common.Interfaces;
using MobDeMob.Domain.ItemAggregate;

namespace Application.Templates.Queries;

public class GetChecklistTemplateQueryHandler : IRequestHandler<GetChecklistTemplateQuery, ChecklistTemplateDto?>
{
    private readonly IChecklistTemplateRepository _checklistTemplateRepository;

    public GetChecklistTemplateQueryHandler(IChecklistTemplateRepository checklistTemplateRepository)
    {
        _checklistTemplateRepository = checklistTemplateRepository;
    }

    public async Task<ChecklistTemplateDto?> Handle(GetChecklistTemplateQuery request, CancellationToken cancellationToken)
    {
        var checklistTemplate = await _checklistTemplateRepository.GetChecklistTemplateById(request.checklistTemplateId, cancellationToken);


        if (checklistTemplate == null) return null; //this means item has not ItemTemplate (checklistTemplate)

        return checklistTemplate.Adapt<ChecklistTemplateDto>();
    }
}
