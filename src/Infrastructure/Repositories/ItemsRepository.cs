using Microsoft.EntityFrameworkCore;
using MobDeMob.Application.Common.Interfaces;
using MobDeMob.Domain.ItemAggregate;


namespace MobDeMob.Infrastructure.Repositories;

public class ItemsRepository : IItemsRepository
{
  
        private readonly ModelContextBase _modelContextBase;

        public ItemsRepository(ModelContextBase modelContextBase)
        {
            _modelContextBase = modelContextBase;
        }

        public async Task AddItem(Part part, CancellationToken cancellationToken)
        {
            await _modelContextBase.Parts.AddAsync(part, cancellationToken);

            await _modelContextBase.SaveChangesAsync(cancellationToken);
        }

        public async Task<Part?> GetById(string id, CancellationToken cancellationToken)
        {
            return await _modelContextBase.Parts
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }
}