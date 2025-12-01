using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;

namespace Kepler.Core.Pagination;

/// <summary>
/// Pagination metadata for API responses
/// </summary>
public class PaginationMetadata
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
    public bool HasPreviousPage => PageNumber > 1;
    public bool HasNextPage => PageNumber < TotalPages;
}

/// <summary>
/// Paginated result wrapper
/// </summary>
public class PagedResult<T>
{
    public List<T> Items { get; set; } = new();
    public PaginationMetadata Metadata { get; set; } = null!;
}

/// <summary>
/// Pagination extension for IQueryable<T>
/// </summary>
public static class PaginationExtensions
{
    /// <summary>
    /// Apply pagination to a query (simple version)
    /// </summary>
    /// <param name="query">The IQueryable to paginate</param>
    /// <param name="page">Page number (1-based, default = 1)</param>
    /// <param name="pageSize">Items per page (default = 10)</param>
    /// <returns>Paginated query</returns>
    public static IQueryable<T> ApplyKeplerPagination<T>(this IQueryable<T> query, int page = 1, int pageSize = 10) where T : class
    {
        Guard.Against.NegativeOrZero(page, nameof(page), "Page must be greater than 0");
        Guard.Against.NegativeOrZero(pageSize, nameof(pageSize), "Page size must be greater than 0");

        var skip = (page - 1) * pageSize;

        return query.Skip(skip).Take(pageSize);
    }

    /// <summary>
    /// Apply pagination and get total count (async)
    /// Returns paginated results with metadata
    /// </summary>
    /// <param name="query">The IQueryable to paginate</param>
    /// <param name="page">Page number (1-based, default = 1)</param>
    /// <param name="pageSize">Items per page (default = 10)</param>
    /// <returns>PagedResult with items and metadata</returns>
    public static async Task<PagedResult<T>> ToPagedResultAsync<T>(this IQueryable<T> query, int page = 1, int pageSize = 10) where T : class
    {
        Guard.Against.NegativeOrZero(page, nameof(page), "Page must be greater than 0");
        Guard.Against.NegativeOrZero(pageSize, nameof(pageSize), "Page size must be greater than 0");

        var totalCount = await query.CountAsync();

        var skip = (page - 1) * pageSize;
        var items = await query
            .Skip(skip)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<T>
        {
            Items = items,
            Metadata = new PaginationMetadata
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalCount = totalCount
            }
        };
    }

    /// <summary>
    /// Apply pagination and get total count (sync version)
    /// </summary>
    public static PagedResult<T> ToPagedResult<T>(this IQueryable<T> query, int page = 1, int pageSize = 10) where T : class
    {
        Guard.Against.NegativeOrZero(page, nameof(page), "Page must be greater than 0");
        Guard.Against.NegativeOrZero(pageSize, nameof(pageSize), "Page size must be greater than 0");

        var totalCount = query.Count();

        var skip = (page - 1) * pageSize;
        var items = query
            .Skip(skip)
            .Take(pageSize)
            .ToList();

        return new PagedResult<T>
        {
            Items = items,
            Metadata = new PaginationMetadata
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalCount = totalCount
            }
        };
    }
}