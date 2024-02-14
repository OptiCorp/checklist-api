using Application.Common.Interfaces;
using Application.Punches.Dtos;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities.ChecklistAggregate;
using Infrastructure.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using MobDeMob.Domain.Entities.ChecklistAggregate;
using MobDeMob.Infrastructure;

namespace Infrastructure.Repositories
{
    public class ChecklistItemRepository : RepositoryBase<ChecklistItem>, IChecklistItemRepository
    {
        private readonly IMapper _mapper;
        public ChecklistItemRepository(ModelContextBase modelContextBase, IMapper mapper) : base(modelContextBase)
        {
            _mapper = mapper;
        }

        public async Task<Guid> AddChecklistItem(ChecklistItem checklistItem, CancellationToken cancellationToken = default)
        {
            await Add(checklistItem, cancellationToken);

            return checklistItem.Id;
        }

        public async Task<ChecklistItem?> GetChecklistItemByItemId(string itemId, Guid checklistId, CancellationToken cancellationToken = default)
        {
            return await _modelContextBase.ChecklistItems
                .Include(ci => ci.Punches)
                .Where(ci => ci.ChecklistId == checklistId)
                .SingleOrDefaultAsync(ci => ci.ItemId == itemId, cancellationToken);
        }

        public async Task<IEnumerable<Punch>> GetChecklistItemsWithPunches(Guid checklistId, CancellationToken cancellationToken = default)
        {
            var punches = await _modelContextBase.ChecklistItems
                .Include(ci => ci.Punches)
                .Where(ci => ci.ChecklistId == checklistId && ci.Punches.Count > 0)
                .SelectMany(ci => ci.Punches)
                .Include(p => p.ChecklistItem)
                //.Select(p => _mapper.Map<Pun)
                //.ProjectTo<PunchDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);


            return punches;
            //.ToListAsync(cancellationToken)
        }
    }
}
