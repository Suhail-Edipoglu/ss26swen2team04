using SWEN2TourPlanner.Bll.Exceptions;
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
    
    public async Task<Log> GetLogAsync(int logId, string username)
    {
        return await _logRepository.GetLogByIdAsync(username, logId) ?? throw new LogNotFoundException();
    }

    public async Task<List<Log>> GetLogsAsync(int tourId, string username)
    {
        return await _logRepository.GetAllLogsForTourAsync(username, tourId) ?? throw new LogNotFoundException();
    }

    public async Task<Log> CreateLogAsync(Log log, string username)
    {
        try
        {
            await _logRepository.InsertLogAsync(username, log);
            return log; // maybe return log from db?
        }
        catch (DuplicateKeyException)
        {
            throw new DuplicateKeyException($"Log with id '{log.Id}' already exists for user '{username}'.");
        }
    }

    public async Task<bool> UpdateLogAsync(Log log, string username)
    {
        try
        {
            await _logRepository.UpdateLogAsync(username, log);
            return true;
        }
        catch (LogNotFoundException)
        {
            return false;
        }
    }

    public async Task<bool> RemoveLogAsync(int logId, string username)
    {
        try
        {
            await _logRepository.DeleteLogAsync(username, logId);
            return true;
        }
        catch (LogNotFoundException)
        {
            return false;
        }
    }

    public async Task<List<Log>> FindMatchingLogsAsync(string username, int tourId, string? searchText = null)
    {
        var logs = await _logRepository.GetAllLogsForTourAsync(username, tourId);
        
        if (string.IsNullOrEmpty(searchText))
        {
            return logs;
        }
        
        return logs.Where(l => l.Comment.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                       l.Difficulty.ToString().Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                       l.Rating.ToString().Contains(searchText, StringComparison.OrdinalIgnoreCase))
                        .ToList();
    }
}