using BlockedCountryAPI.Entities.Models;

namespace Contracts;

public interface IAttemptLogRepository
{
    void Add(BlockedAttemptLog log);

    (int total, IEnumerable<BlockedAttemptLog> blockedAttemptLogs) GetAllWithCount();
}