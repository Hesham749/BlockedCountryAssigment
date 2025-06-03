using BlockedCountryAPI.Entities.Models;
using Contracts;
using Services.Contracts;
using Shared;
using Shared.DTOs;

namespace Services;

public class LogService(IAttemptLogRepository repo) : ILogService
{
    public void LogAttempt(BlockedAttemptLog attempt) => repo.Add(attempt);

    public PagedResult<BlockedAttemptLog> GetBlockedAttempts(PaginatedQueryParameters query)
    {
        var (total, blockedAttemptLogs) = repo.GetAllWithCount();

        var all = blockedAttemptLogs.OrderByDescending(a => a.Timestamp);

        var items = all
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToList();

        return new PagedResult<BlockedAttemptLog>
        {
            Items = items,
            TotalCount = total,
            Page = query.Page,
            PageSize = query.PageSize
        };
    }
}