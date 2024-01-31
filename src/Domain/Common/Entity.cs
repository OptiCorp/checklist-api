
using System.ComponentModel.DataAnnotations.Schema;

namespace MobDeMob.Domain.Common;

public abstract class Entity
{
    public required string Id {get; set;}

    private readonly List<Event> _domainEvents = new();

    [NotMapped]
    public IReadOnlyCollection<Event> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(Event domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}