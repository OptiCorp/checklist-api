
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entities;

public class ChecklistTemplate
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public required string Id {get; set;}

    public required string ItemTemplateId {get; set;}

    public ItemTemplate ItemTemplate {get; set;} = null!;
}
