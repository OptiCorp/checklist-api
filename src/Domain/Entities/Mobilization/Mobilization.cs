using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MobDeMob.Domain.Common;
using MobDeMob.Domain.Entities.ChecklistAggregate;
using MobDeMob.Domain.ItemAggregate;

namespace MobDeMob.Domain.Entities.Mobilization;

public class Mobilization : AuditableEntity
{
        public required string Title { get; set; }

        public string? Description { get; set; }

        public MobilizationType Type { get; set; }

        public string? ChecklistId {get; set;}

        public Checklist? Checklist { get; set; }

        [NotMapped]
        public IEnumerable<Part> Parts => Checklist?.Parts ?? Enumerable.Empty<Part>();

        [NotMapped]
        public IEnumerable<Punch> Punches => Checklist?.Punches ?? Enumerable.Empty<Punch>();
    
}