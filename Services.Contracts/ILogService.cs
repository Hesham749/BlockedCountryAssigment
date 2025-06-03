using BlockedCountryAPI.Entities.Models;
using Shared;
using Shared.DTOs;

namespace Services.Contracts;

public interface ILogService
{
    PagedResult<BlockedAttemptLog> GetBlockedAttempts(PaginatedQueryParameters query);

    void LogAttempt(BlockedAttemptLog attempt);
}