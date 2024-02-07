

using Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using MobDeMob.Domain.Entities.ChecklistAggregate;
using MobDeMob.Domain.Entities;
using MobDeMob.Domain.ItemAggregate;

namespace MobDeMob.Infrastructure.Repositories;

public class MobilizationRepository : IMobilizationRepository
{

    private readonly ModelContextBase _modelContextBase;

    public MobilizationRepository(ModelContextBase modelContextBase)
    {
        _modelContextBase = modelContextBase;
    }
    public async Task AddMobilization(Mobilization mobilization, CancellationToken cancellationToken)
    {
        await _modelContextBase.Mobilizations.AddAsync(mobilization, cancellationToken);
        await _modelContextBase.SaveChangesAsync(cancellationToken);
    }

    public async Task<Mobilization?> GetById(string id, CancellationToken cancellationToken)
    {
        return await _modelContextBase.Mobilizations
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<Mobilization>> GetAll(CancellationToken cancellationToken)
    {
        return await _modelContextBase.Mobilizations
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task UpdateMobilization(string id, string? title, string? desription, CancellationToken cancellationToken)
    {
        var mob = await _modelContextBase.Mobilizations
            .SingleAsync(m => m.Id == id, cancellationToken);
        if (title != null)
        {
            mob.Title = title;
        }
        if (desription != null)
        {
            mob.Description = desription;
        }
        await _modelContextBase.SaveChangesAsync(cancellationToken);
        //TODO: should the items also be provided or should a different endpoint do this?
    }

    public async Task DeleteMobilization(string id, CancellationToken cancellationToken)
    {
        await _modelContextBase.Mobilizations
            .Where(m => m.Id == id)
            .ExecuteDeleteAsync(cancellationToken);
    }

    public async Task AddPartToMobilization(string id, string partId, CancellationToken cancellationToken)
    {
        var mob = await _modelContextBase.Mobilizations
            .AsNoTracking()
            .Include(m => m.Checklist)
            .FirstAsync(m => m.Id == id, cancellationToken);

        if (mob.ChecklistId == null)
        {
            throw new Exception($"Mobilizaiton with id: {id} does not have any Checklist associated");
        }

        var part = await _modelContextBase.Parts
            .Include(p => p.PartTemplate)
            .ThenInclude(pt => pt.PartCheckListTemplate)
            .FirstAsync(p => p.Id == partId, cancellationToken);

        if (part.ChecklistId != null)
        {
            var partBelongsToMobId = await _modelContextBase.Mobilizations
                .Where(m => m.ChecklistId == part.ChecklistId)
                .Select(m => m.Id)
                .SingleAsync(cancellationToken);
            if (mob.Id == partBelongsToMobId) throw new Exception ($"PartId: '{partId}' already belongs to this mobilization");
            throw new Exception($"PartId: '{partId}' already belongs to mobilization with id: {partBelongsToMobId} ");
        }

        if (part.PartTemplate.PartCheckListTemplate == null)
        {
            throw new Exception($"PartId: '{partId}' with partTemplateId: {part.PartTemplate.Id} does not have any checklistTemplate ()");
        }

        var checklistSection = await _modelContextBase.ChecklistSections
            .Where(cs => cs.ChecklistId == mob.ChecklistId && cs.ChecklistSectionId == null)
            .FirstOrDefaultAsync(cancellationToken);

        if (checklistSection == null) //is first item in the section
        {
            await _modelContextBase.ChecklistSections.AddAsync(new ChecklistSection
            {
                ChecklistSectionTemplate = part.PartTemplate.PartCheckListTemplate,
                Checklist = mob.Checklist,
                Part = part
            }, cancellationToken);

            // if (part.PartTemplate.PartCheckListTemplate == null)
            // {
            //     await _modelContextBase.ChecklistSections.AddAsync(new ChecklistSection
            //     {
            //         ChecklistSectionTemplate = new ChecklistSectionTemplate
            //         {
            //             ChecklistQuestion = ""
            //         },
            //         Checklist = mob.Checklist,
            //         Part = part
            //     }, cancellationToken);
            // } 
        }
        else //is not the first item in the section
        {
            checklistSection.SubSections.Add(new ChecklistSection
            {
                Part = part,
                ChecklistSectionTemplate = part.PartTemplate.PartCheckListTemplate,
                Checklist = mob.Checklist
            });
        }

        part.ChecklistId = mob.ChecklistId;
        await _modelContextBase.SaveChangesAsync(cancellationToken);
    }

    public async Task RemovePartFromMobilization(string id, string partId, CancellationToken cancellationToken)
    {
        var mob = await _modelContextBase.Mobilizations
            .Include(m => m.Checklist)
                .ThenInclude(c => c.Parts)
            // .Include(m => m.Checklist)
            //     .ThenInclude(c => c.ChecklistSections
            //         .Where(cs => cs.PartId == partId
            //     ))
            .SingleAsync(m => m.Id == id, cancellationToken);
            

        var part = mob.Checklist.Parts.Single(p => p.Id == partId);
        mob.Checklist.Parts.Remove(part);

        var cs = await _modelContextBase.ChecklistSections
            .Where(cs => cs.ChecklistId == mob.ChecklistId && cs.PartId == partId)
            .SingleAsync(cancellationToken);

        _modelContextBase.ChecklistSections.Remove(cs);

        await _modelContextBase.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<Part>> GetAllPartsInMobilization(string mobId, bool includeChildren, CancellationToken cancellationToken)
    {
        var query = _modelContextBase.Mobilizations
            .AsNoTracking()
            .Where(m => m.Id == mobId);

        if (includeChildren)
        {
            query = query
                .Include(m => m.Checklist)
                    .ThenInclude(c => c.Parts)
                        .ThenInclude(p => p.Children);
        }
        else
        {
            query = query
                .Include(m => m.Checklist)
                    .ThenInclude(c => c.Parts);
        }
        var checklist = await query
            .Select(m => m.Checklist)
            .SingleAsync(cancellationToken);

        return checklist.Parts;
    }
}