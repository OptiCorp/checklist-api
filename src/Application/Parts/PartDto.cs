
using MobDeMob.Domain.ItemAggregate;

namespace MobDeMob.Application.Parts;

public record PartChildDto
{
    public required string partChildId {get; set;}

    public string Type {get; set;}

    public PartChildDto(PartType partType)
    {
        Type = partType.ToString();
    }
}
public record PartDto
{
    public required string PartId {get; set;}
    public string Type { get; set; }
    public required string WpId { get; set; }

    public required string SerialNumber { get; set; }

    public required string Name { get; set; }
    public string? PartTemplateId { get; set; }

    public required bool hasChecklistTemplate {get; set;}

    public IEnumerable<PartChildDto>? Children { get; set; }

    public PartDto(PartType partType)
    {
        Type = partType.ToString();
    }
}