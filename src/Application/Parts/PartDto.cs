
using MobDeMob.Domain.ItemAggregate;

namespace MobDeMob.Application.Parts;
public record PartDto
{
    public string Type { get; set; }
    public required string WpId { get; set; }

    public required string SerialNumber { get; set; }

    public required string Name { get; set; }
    public string? PartTemplateId { get; set; }

    public ICollection<Part>? Children { get; set; }

    public PartDto(PartType partType)
    {
        Type = partType.ToString();
    }
}