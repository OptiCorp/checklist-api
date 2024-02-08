

using MobDeMob.Domain.Entities.ChecklistAggregate;

namespace MobDeMob.Application.Common.Interfaces;

public interface ICacheRepository
{
    Uri? GetValue (string key);

    void SetKeyValue (string key, Uri uri, TimeSpan? absoluteExpirationRelativeToNow = null);
}