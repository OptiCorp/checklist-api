using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MobDeMob.Domain.Common;
using MobDeMob.Domain.Entities.ChecklistAggregate;
using MobDeMob.Domain.Entities.ItemAggregate;

namespace MobDeMob.Domain.Entities.Mobilization;

public class Mobilization : Entity
{
        public required string Title { get; set; }

        public string? Description { get; set; }

        public MobilizationType Type { get; set; }

        public required Checklist Checklist { get; set; }

        [NotMapped]
        public IEnumerable<Item> Items => Checklist?.Items ?? Enumerable.Empty<Item>();

        [NotMapped]
        public IEnumerable<Punch> Punches => Checklist?.Punches ?? Enumerable.Empty<Punch>();
    
}