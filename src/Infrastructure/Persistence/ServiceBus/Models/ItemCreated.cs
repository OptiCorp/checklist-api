namespace Infrastructure.Persistence.ServiceBus.Models;

public class ItemCreated : BaseCommunicationEntity
{
    public string itemId {get; init;}

    public string itemTemplateId {get; init;}
}