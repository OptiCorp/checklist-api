using Microsoft.EntityFrameworkCore;
using MobDeMob.Application.Common.Interfaces;
using MobDeMob.Application.Mobilizations;
using MobDeMob.Domain.Entities.ChecklistAggregate;
using MobDeMob.Domain.ItemAggregate;


namespace MobDeMob.Infrastructure.Repositories;

public class PartsRepository : IPartsRepository
{

    private readonly ModelContextBase _modelContextBase;

    public PartsRepository(ModelContextBase modelContextBase)
    {
        _modelContextBase = modelContextBase;
    }

    public async Task AddPart(Part part, CancellationToken cancellationToken)
    {
        await _modelContextBase.Parts.AddAsync(part, cancellationToken);

        await _modelContextBase.SaveChangesAsync(cancellationToken);
    }

    public async Task<Part?> GetById(string id, CancellationToken cancellationToken)
    {
        return await _modelContextBase.Parts
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    }

    public async Task<IEnumerable<Part>> GetAll(CancellationToken cancellationToken)
    {
        return await _modelContextBase.Parts
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<ChecklistSectionTemplate>> GetQuestions(string id, CancellationToken cancellationToken)
    {
        //var part = await _modelContextBase.Parts.FirstAsync(p => p.Id  == id);
        var sections =  await _modelContextBase.Parts
            .Include(p => p.PartTemplate)
            .ThenInclude(p => p.PartCheckListTemplate)
            .ThenInclude(p => p.SubSections)
            .Where(p => p.Id == id)
            .Select(p => p.PartTemplate.PartCheckListTemplate)
            .ToListAsync(cancellationToken);
        
        var allQuestions = new List<ChecklistSectionTemplate>();
        if (sections == null) return allQuestions;

        foreach(var section in sections)
        {
            if (section == null) break;
            allQuestions.AddRange(GetAllQuestions(section));
        }
        return allQuestions;
    }

    private static List<ChecklistSectionTemplate> GetAllQuestions(ChecklistSectionTemplate section)
    {
        var questions = new List<ChecklistSectionTemplate>();

        // Add the question from the current section
        questions.Add(section);

        // Recursively add the questions from the subsections
        foreach (var subSection in section.SubSections)
        {
            questions.AddRange(GetAllQuestions(subSection));
        }

        return questions;
    }
}