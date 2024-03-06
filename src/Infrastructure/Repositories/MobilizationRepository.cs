using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Common.Models;
using Application.Mobilizations.Dtos;
using Domain.Entities.ChecklistAggregate;
using Infrastructure.Repositories.Common;
using Mapster;
using Microsoft.EntityFrameworkCore;
using MobDeMob.Domain.Entities;

namespace MobDeMob.Infrastructure.Repositories;

public class MobilizationRepository : RepositoryBase<Mobilization>, IMobilizationRepository   
{



    public MobilizationRepository(ModelContextBase modelContextBase) : base(modelContextBase)
    {
    }

    public async Task<Guid> AddMobilization(Mobilization mobilization, CancellationToken cancellationToken)
    {
        await Add(mobilization, cancellationToken);
        return mobilization.Id;
    }

    public async Task<Mobilization?> GetMobilizationById(Guid mobilizationId, CancellationToken cancellationToken)
        => await GetSet().Include(m => m.ChecklistCollection).FirstOrDefaultAsync(x => x.Id == mobilizationId, cancellationToken);

    // public async Task<IEnumerable<Mobilization>> GetAllMobilizations(CancellationToken cancellationToken)
    //     => await GetAll(cancellationToken);

    public async Task<IEnumerable<Mobilization>> GetAllMobilizations(CancellationToken cancellationToken)
    {
        return await GetSet()
            .AsNoTracking()
            .Include(m => m.ChecklistCollection)
            .ThenInclude(c => c.Checklists)
            .ToListAsync(cancellationToken);
    }

    public async Task DeleteMobilization(Guid id, CancellationToken cancellationToken)
        => await DeleteById(id, cancellationToken);

    //TODO: this was created for getting completion percent, may be a better way of doing it without including everything
    public async Task<Mobilization?> GetMobilizationByIdWithChecklists(Guid mobilizationId, CancellationToken cancellationToken = default)

        => await GetSet().Include(m => m.ChecklistCollection)
                            .ThenInclude(c => c.Checklists)
                                //.ThenInclude(ci => ci.Questions)
                                .SingleOrDefaultAsync(x => x.Id == mobilizationId, cancellationToken);

    public async Task<PaginatedList<Mobilization>> GetAllMobilizationsWithPagination(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        return await GetSet()
            .OrderBy(x => x.Title)
            .Include(m => m.ChecklistCollection)
            .ThenInclude(c => c.Checklists)
            //.ProjectToType<MobilizationDto>()
            .PaginatedListAsync(pageNumber, pageSize);

        //.PaginatedListAsync(pageNumber, pageSize);
    }

    public async Task<PaginatedList<Mobilization>> GetMobilizationsBySearch(int pageNumber, int pageSize, string title, MobilizationStatus? status, CancellationToken cancellationToken)
    {
        return await GetSet()
            .Where(m => m.Title.Contains(title) && (status == null || m.Status == status))
            .OrderBy(x => x.Title)
            .Include(m => m.ChecklistCollection)
            .ThenInclude(c => c.Checklists)
            // .ProjectToType<MobilizationDto>()
            .PaginatedListAsync(pageNumber, pageSize); 
    }

    public async Task<PaginatedList<Mobilization>> GetMobilizationsForItem(string ItemId, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        return await GetSet()
            .Include(m => m.ChecklistCollection)
            .ThenInclude(cc => cc.Checklists)
            .ThenInclude(c => c.ItemTemplate)
            .Where(m => m.ChecklistCollection.Checklists.Any(c => c.ItemTemplate.ItemId == ItemId))
            .PaginatedListAsync(pageNumber, pageSize);
    }

}
