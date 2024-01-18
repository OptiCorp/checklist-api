using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entities;

public class ItemTemplate
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public required string Id { get; set; }

    public required string Name { get; set; }

    public required string Type { get; set; }

    public required string CategoryId { get; set; }

    public required string ProductNumber { get; set; }

    public string? Revision { get; set; }

    public string? Description { get; set; }

    public DateOnly CreatedDate { get; set; }

    public DateOnly? UpdatedDate { get; set; }

    public required string CreatedById { get; set; }

    public ChecklistTemplate? ChecklistTemplate {get; set;}

    public User CreatedBy { get; set; } = null!;

    // public Category? Category { get; set; }

    // public IEnumerable<Document>? Documents { get; set; }

    // public IEnumerable<Size>? Sizes { get; set; }

}

