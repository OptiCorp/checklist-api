
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entities;

public class ChecklistItem
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id {get; set;}

    [MaxLength(150)]
    public required string Question {get; set;} 

    public required string ChecklistTemplateId {get; set;}



    //TODO: add additional props
}