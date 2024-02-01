using Microsoft.EntityFrameworkCore;
using MobDeMob.Application.Common.Interfaces;
using MobDeMob.Domain.Entities.ItemAggregate;


namespace MobDeMob.Infrastructure.Repositories;

public class ItemsRepository : IItemsRepository
{
  
        private readonly ModelContextBase _modelContextBase;

        public ItemsRepository(ModelContextBase modelContextBase)
        {
            _modelContextBase = modelContextBase;
        }

        public async Task AddItem(Item item, CancellationToken cancellationToken = default)
        {
            await _modelContextBase.Items.AddAsync(item, cancellationToken);

            await _modelContextBase.SaveChangesAsync(cancellationToken);
        }

        public async Task<Item?> GetById(string id)
        {
            return await _modelContextBase.Items.FirstOrDefaultAsync(x => x.Id == id);
        }
}