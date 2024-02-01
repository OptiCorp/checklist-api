

using Application.Common.Interfaces;
using MobDeMob.Domain.Entities.Mobilization;

namespace MobDeMob.Infrastructure.Repositories;

public class MobilizationRepository : IMobilizationRepository
{

    private readonly ModelContextBase _modelContextBase;

    public MobilizationRepository(ModelContextBase modelContextBase)
    {
        _modelContextBase = modelContextBase;
    }
    public Task AddMobilization(Mobilization mobilization)
    {
        throw new NotImplementedException();
    }
}