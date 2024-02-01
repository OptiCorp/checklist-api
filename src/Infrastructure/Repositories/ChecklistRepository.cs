using Microsoft.EntityFrameworkCore;
using MobDeMob.Application.Common.Interfaces;
using MobDeMob.Domain.Entities.ChecklistAggregate;
using MobDeMob.Domain.ItemAggregate;


namespace MobDeMob.Infrastructure.Repositories;

public class CheklistRepository : IChecklistRepository
{
  
        private readonly ModelContextBase _modelContextBase;

        public CheklistRepository(ModelContextBase modelContextBase)
        {
            _modelContextBase = modelContextBase;
        }

    public Task<string> AddChecklist()
    {
        throw new NotImplementedException();
    }
}