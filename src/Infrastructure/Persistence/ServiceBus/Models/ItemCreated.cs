namespace Infrastructure.Persistence.ServiceBus.Models;

public class ItemCreated : BaseCommunicationEntity
{
    public string ItemId {get; init;}

    public string ItemTemplateId {get; init;}
}