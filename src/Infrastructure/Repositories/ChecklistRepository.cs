using Microsoft.EntityFrameworkCore;
using MobDeMob.Application.Common.Interfaces;
using MobDeMob.Domain.Entities.ChecklistAggregate;


namespace MobDeMob.Infrastructure.Repositories;

public class CheklistRepository : IChecklistRepository
{

    private readonly ModelContextBase _modelContextBase;

    public CheklistRepository(ModelContextBase modelContextBase)
    {
        _modelContextBase = modelContextBase;
    }

    public async Task<string> AddChecklist(Checklist checklist, CancellationToken cancellationToken = default)
    {
        await _modelContextBase.Checklists.AddAsync(checklist, cancellationToken);
        return checklist.Id;
    }

    public async Task<IEnumerable<ChecklistSectionTemplate>?> GetQuestions(string id, CancellationToken cancellationToken = default)
    {
        //var part = await _modelContextBase.Parts.FirstAsync(p => p.Id  == id);
        var sections = await _modelContextBase.Parts
            .Where(p => p.Id == id)
            .Include(p => p.PartTemplate)
            .ThenInclude(pt => pt.PartCheckListTemplate)
            .ThenInclude(ct => ct.SubSections)
            .Select(p => p.PartTemplate.PartCheckListTemplate)
            .ToListAsync(cancellationToken);

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
        var questions = new List<ChecklistSectionTemplate>
        {
            // Add the question from the current section
            section
        };

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
            //TODO: dont delete, update the existing (Robin from the future: I dont think that is possible due to determenism)
            partTemp.PartCheckListTemplate.ChecklistQuestion = firstQuestion;

            // var diffQuestionsCount = partTemp.PartCheckListTemplate.SubSections.Count - restQuestions.Count();
            // if (diffQuestionsCount > 0)//diff need to be deleted
            // {
            //     _modelContextBase.ChecklistSectionTemplate.RemoveRange(partTemp.PartCheckListTemplate.SubSections.Take(diffQuestionsCount));
            // }else if (diffQuestionsCount <= 0) //non need to be deleted
            // {

            // }
            _modelContextBase.ChecklistSectionTemplate.RemoveRange(partTemp.PartCheckListTemplate.SubSections);

            foreach (var q in restQuestions)
            {
                partTemp.PartCheckListTemplate.SubSections.Add(new ChecklistSectionTemplate { ChecklistQuestion = q });
            }
        }
        await _modelContextBase.SaveChangesAsync(cancellationToken);
    }
}
