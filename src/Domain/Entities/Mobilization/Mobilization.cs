using Domain.Entities.Mobilization.Events;
using MobDeMob.Domain.Common;
using MobDeMob.Domain.Entities.ChecklistAggregate;

namespace MobDeMob.Domain.Entities;

public class Mobilization : AuditableEntity
{
    public required string Title { get; set; }

    public string? Description { get; set; }

    public MobilizationType Type { get; set; }

    public MobilizationStatus Status { get; set; }

    public string ChecklistId { get; set; }

    public Checklist Checklist { get; set; } = null!;

    public IList<string> PartIds { get; set; } = new List<string>();

    //[NotMapped]
    //public IEnumerable<Punch> Punches => Checklist?.Punches ?? Enumerable.Empty<Punch>();

    public void AddPartToMobilization(object part)//TODO
    {
        //if (Parts.Any(part => part.ChecklistId == ChecklistId))
        //{
        //    throw new Exception($"PartId: '{part.Id}' already belongs to mobilization with id: {Id} ");
        //}
        // I'm thinking we probably only need of this 2 if statements, the first one is similar to what you implemented
        // but I think one part can only have 1 mob, we can discuss this later

        //if (part.ChecklistId != null)
        //{
        //    throw new Exception($"Part is already assigned to mobilization with Id: {part.ChecklistId}");
        //}

        //if (!part.hasChecklistTemplate)
        //{
        //    throw new Exception($"PartId: '{part.Id}' with partTemplateId: {part.PartTemplate.Id} does not have any checklistTemplate ()");
        //}

        //// add more validations if needed


        //part.AssignToCheckList(ChecklistId);
        //Parts.Add(part);
    }

    public void DeleteParts()
    {
        AddDomainEvent(new MobilizationDeleted(Id));
    }

    public void RemovePartToMobilization(object part) // TODO
    {
        //if (part.ChecklistId == null)
        //{
        //    throw new Exception($"Part is not assigned to a checklist");
        //}

        //if (Parts.Remove(part))
        //{
        //    throw new Exception($"Could not remove part");
        //}
    }
}
