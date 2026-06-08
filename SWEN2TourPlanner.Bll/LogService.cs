using SWEN2TourPlanner.Dal;
using SWEN2TourPlanner.Models;

namespace SWEN2TourPlanner.Bll;

public class LogService : ILogService
{
    private readonly ILogRepository _logRepository;
    
    public LogService(ILogRepository logRepository)
    {
        _logRepository = logRepository;
    }
    
    public Task<Log?> GetLogAsync(int logId)
    {
        throw new NotImplementedException();
    }

    public Task<Log?> GetLogsAsync(int tourId, string username)
    {
        throw new NotImplementedException();
    }

    public Task<Log> CreateLogAsync(Log log)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateLogAsync(Log log)
    {
        throw new NotImplementedException();
    }

    public Task<bool> RemoveLogAsync(int logId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Log>> FindMatchingLogsAsync(int tourId, string? searchText = null)
    {
        throw new NotImplementedException();
    }
}