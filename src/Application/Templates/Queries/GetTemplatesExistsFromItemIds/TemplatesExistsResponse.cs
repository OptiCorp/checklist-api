
namespace Application.Templates;

public class TemplateExistsReponse
{
    public string ItemId {get; set;}

    public bool HasChecklistTemplate {get; set;}

    public static TemplateExistsReponse New(string itemId, bool hasChecklistTemplate)
    {
        return new TemplateExistsReponse()
        {
            ItemId = itemId,
            HasChecklistTemplate = hasChecklistTemplate,
        };
    }
}