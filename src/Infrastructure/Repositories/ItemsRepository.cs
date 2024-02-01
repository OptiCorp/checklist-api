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

        public async Task AddItem(Part part, CancellationToken cancellationToken = default)
        {
            await _modelContextBase.Parts.AddAsync(part, cancellationToken);

            await _modelContextBase.SaveChangesAsync(cancellationToken);
        }

        public async Task<Part?> GetById(string id)
        {
            return await _modelContextBase.Parts.FirstOrDefaultAsync(x => x.Id == id);
        }
}