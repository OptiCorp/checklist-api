using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MobDeMob.Domain.Common;

namespace MobDeMob.Domain.Entities;

public class Item : AuditableEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public required string Id { get; set; }

    public required string WpId { get; set; }

    public required string SerialNumber { get; set; }

    public required string CreatedById { get; set; }

    [MaxLength(300)]
    public string? Comment { get; set; }

    public DateOnly? LastModified { get; set; }

    public ICollection<Item>? Children { get; set; }

    // public string VendorId { get; set; }
    // public Vendor Vendor { get; set; }

    // public string LocationId { get; set; }
    // public Location? Location { get; set; }

    //public IEnumerable<LogEntry>? LogEntries { get; set; }

    //public User? CreatedBy { get; set; } //Should be requiered?
    // public string? PreCheckId { get; set; }

    // public PreCheck? PreCheck { get; set; } 
    
    public required string ItemTemplateId { get; set; }
    public ItemTemplate ItemTemplate { get; set; } = null!;

    //public ICollection<Document>? Documents { get; set; }
    public required string ParentId { get; set; }
    public Item? Parent { get; set; }

    public List<Mobilization> Mobilizations {get; set;} = [];

    public List<ItemMobilization> ItemMobilizations {get; set;} = [];

    public List<Punch> Punches {get; set;} = [];

}

