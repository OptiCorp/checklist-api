namespace Application.Common;
public abstract class DtoExtension
{
    public Guid Id {get; set;}
    public DateOnly Created { get; set; }

    // public string? CreatedBy { get; set; }

    // public DateTime? LastModified { get; set; }

    // public string? LastModifiedBy { get; set; }
}