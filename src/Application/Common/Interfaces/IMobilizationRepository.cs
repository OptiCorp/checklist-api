﻿using Application.Common.Models;
using Application.Mobilizations.Dtos;
using MobDeMob.Domain.Entities;

namespace Application.Common.Interfaces;

public interface IMobilizationRepository
{
    Task<Guid> AddMobilization(Mobilization mobilization, CancellationToken cancellationToken = default);

    Task<Mobilization?> GetMobilizationById(Guid id, CancellationToken cancellationToken = default);

    Task<Mobilization?> GetMobilizationByIdWithChecklists(Guid id, CancellationToken cancellationToken = default);

    Task<IEnumerable<Mobilization>> GetAllMobilizations(CancellationToken cancellationToken = default);

    Task<PaginatedList<Mobilization>> GetAllMobilizationsWithPagination(int pageNumber, int pageSize, CancellationToken cancellationToken = default);

    Task<PaginatedList<Mobilization>> GetMobilizationsBySearch(int pageNumber, int pageSize, string? title, DateOnly? date,  MobilizationStatus? status, CancellationToken cancellationToken);


    Task DeleteMobilization(Guid id, CancellationToken cancellationToken = default);

    Task<Mobilization?> GetMobilizationIdByChecklistCollectionId(Guid id, CancellationToken cancellationToken = default);



    //Task<PaginatedList<Mobilization>> GetMobilizationsForItem(string ItemId, int pageNumber, int pageSize, CancellationToken cancellationToken = default);

    //Task RemovePartFromMobilization(string id, string partId, CancellationToken cancellationToken = default);

    //Task<IEnumerable<Part>> GetAllPartsInMobilization(string mobId, bool includeChildren, CancellationToken cancellationToken = default);

    Task SaveChanges(CancellationToken cancellationToken = default);
}
