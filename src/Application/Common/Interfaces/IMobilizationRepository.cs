using MobDeMob.Domain.Entities.Mobilization;

namespace Application.Common.Interfaces;

public interface IMobilizationRepository
{
    Task AddMobilization(Mobilization mobilization);
}
