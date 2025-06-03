using System.Collections.Concurrent;
using BlockedCountryAPI.Entities.Models;
using Contracts;

namespace Infrastructure;

public class AttemptLogRepository : IAttemptLogRepository
{
    private readonly ConcurrentBag<BlockedAttemptLog> _logs = [];

    public void Add(BlockedAttemptLog log) => _logs.Add(log);

    public (int total, IEnumerable<BlockedAttemptLog> blockedAttemptLogs) GetAllWithCount()
        => (_logs.Count, [.. _logs]);
}