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

    public async Task<PaginatedList<Mobilization>> GetMobilizationsBySearch(int pageNumber, int pageSize, string? title, DateOnly? date, MobilizationStatus? status, CancellationToken cancellationToken)
    {
        if (title == null && status == null && date == null) throw new Exception("Cannot search for mobilizations with no inputs");
        var query = GetSet()
            .OrderBy(x => x.Title)
            .Include(m => m.ChecklistCollection)
            .ThenInclude(c => c.Checklists)
            .AsQueryable();
        // .OrderBy(x => x.Title)
        // .Include(m => m.ChecklistCollection)
        // .ThenInclude(c => c.Checklists);

        if (!string.IsNullOrEmpty(title))
            query = query.Where(m => m.Title.Contains(title));

        if (status != null)
            query = query.Where(m => m.Status == status);

        if (date != null)
            query = query.Where(m => m.Created == date);



        return await query.PaginatedListAsync(pageNumber, pageSize);
    }

    public async Task<Mobilization?> GetMobilizationIdByChecklistCollectionId(Guid id, CancellationToken cancellationToken = default)
    {
        return await GetSet() 
            .AsNoTracking()
            .Where(m => m.ChecklistCollectionId == id)
            .SingleOrDefaultAsync(cancellationToken);
    }


    // public async Task<PaginatedList<Mobilization>> GetMobilizationsForItem(string ItemId, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    // {
    //     return await GetSet()
    //         .Include(m => m.ChecklistCollection)
    //         .ThenInclude(cc => cc.Checklists)
    //         .ThenInclude(c => c.ItemTemplate)
    //         .Where(m => m.ChecklistCollection.Checklists.Any(c => c.ItemTemplate.ItemId == ItemId))
    //         .PaginatedListAsync(pageNumber, pageSize);
    // }

}
