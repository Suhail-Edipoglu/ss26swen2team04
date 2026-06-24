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
    
    public async Task<Log?> GetLogAsync(int logId)
    {
        throw new NotImplementedException();
    }

    public async Task<Log?> GetLogsAsync(int tourId, string username)
    {
        throw new NotImplementedException();
    }

    public async Task<Log> CreateLogAsync(Log log)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> UpdateLogAsync(Log log)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> RemoveLogAsync(int logId)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Log>> FindMatchingLogsAsync(string username, int tourId, string? searchText = null)
    {
        var logs = await _logRepository.GetAllLogsForTourAsync(username, tourId);
        
        if (string.IsNullOrEmpty(searchText))
        {
            return logs;
        }
        
        return logs.Where(l => l.Comment.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                       l.Difficulty.ToString().Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                       l.Rating.ToString().Contains(searchText, StringComparison.OrdinalIgnoreCase));
    }
}