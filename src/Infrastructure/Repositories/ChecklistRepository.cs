using Microsoft.EntityFrameworkCore;
using MobDeMob.Application.Common.Interfaces;
using MobDeMob.Domain.Entities.ChecklistAggregate;
using MobDeMob.Domain.ItemAggregate;


namespace MobDeMob.Infrastructure.Repositories;

public class CheklistRepository : IChecklistRepository
{

    private readonly ModelContextBase _modelContextBase;

    public CheklistRepository(ModelContextBase modelContextBase)
    {
        _modelContextBase = modelContextBase;
    }

    public async Task<string> AddChecklist(CancellationToken cancellationToken)
    {
        var newChecklist = new Checklist() { };
        await _modelContextBase.Checklists.AddAsync(newChecklist, cancellationToken);
        return newChecklist.Id;
    }

    public async Task<IEnumerable<ChecklistSectionTemplate>?> GetQuestions(string id, CancellationToken cancellationToken)
    {
        //var part = await _modelContextBase.Parts.FirstAsync(p => p.Id  == id);
        var sections = await _modelContextBase.Parts
            .Where(p => p.Id == id)
            .Include(p => p.PartTemplate)
            .ThenInclude(pt => pt.PartCheckListTemplate)
            .ThenInclude(ct => ct.SubSections)
            .Select(p => p.PartTemplate.PartCheckListTemplate)
            .ToListAsync(cancellationToken);
        //.ToListAsync(cancellationToken);

        if (sections == null) return null;

        var allQuestions = new List<ChecklistSectionTemplate>();

        foreach (var section in sections)
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

    public async Task CreatePartChecklistQuestions(string id, List<string> questions, CancellationToken cancellationToken)
    {
        var partTemp = await _modelContextBase.Parts
            .Where(p => p.Id == id)
            .Include(p => p.PartTemplate)
            .ThenInclude(pt => pt.PartCheckListTemplate)
            .ThenInclude(ct => ct.SubSections)
            .Select(p => p.PartTemplate)
            .SingleAsync(cancellationToken);

        var firstQuestion = questions.First();
        var restQuestions = questions.Skip(1);

        if (partTemp.PartCheckListTemplate == null)
        {
            partTemp.PartCheckListTemplate = new ChecklistSectionTemplate
            {
                ChecklistQuestion = firstQuestion,
            };
            foreach (var q in restQuestions)
            {
                partTemp.PartCheckListTemplate.SubSections.Add(new ChecklistSectionTemplate { ChecklistQuestion = q });
            }
        }

        else
        {
            //TODO: dont delete, update the existing
            _modelContextBase.ChecklistSectionTemplate.RemoveRange(partTemp.PartCheckListTemplate.SubSections);

            partTemp.PartCheckListTemplate.ChecklistQuestion = firstQuestion;

            foreach (var q in restQuestions)
            {
                partTemp.PartCheckListTemplate.SubSections.Add(new ChecklistSectionTemplate { ChecklistQuestion = q });
            }
        }
        await _modelContextBase.SaveChangesAsync(cancellationToken);
    }
}