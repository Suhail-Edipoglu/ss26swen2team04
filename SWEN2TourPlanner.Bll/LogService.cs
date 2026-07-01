using SWEN2TourPlanner.Bll.Exceptions;
using SWEN2TourPlanner.Dal;
using SWEN2TourPlanner.Models;

namespace SWEN2TourPlanner.Bll;

public class LogService : ILogService
{
    private readonly ILogRepository _logRepository;
    private readonly ITourRepository _tourRepository;
    
    public LogService(ILogRepository logRepository, ITourRepository tourRepository)
    {
        _logRepository = logRepository;
        _tourRepository = tourRepository;
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
            await CalculateTourAttributesAsync(username, log.TourId);
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
            await CalculateTourAttributesAsync(username, log.TourId);
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
            await CalculateTourAttributesAsync(username, logId);
            return true;
        }
        catch (LogNotFoundException)
        {
            return false;
        }
    }

    public async Task<List<Log>> FindMatchingLogsAsync(string username, int tourId, string? searchText = null)
    {
        var logs = (await _logRepository.GetAllLogsForTourAsync(username, tourId)).OrderBy(l => l.Id).ToList();
        
        if (string.IsNullOrEmpty(searchText))
        {
            return logs;
        }
        
        return logs.Where(l => l.Time.ToString().Contains(searchText, StringComparison.OrdinalIgnoreCase) || 
                               l.Comment.Contains(searchText, StringComparison.OrdinalIgnoreCase) || 
                               l.Difficulty.ToString().Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                               l.TotalDistance.ToString().Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                               l.TotalTime.ToString().Contains(searchText, StringComparison.OrdinalIgnoreCase) || 
                               l.Rating.ToString().Contains(searchText, StringComparison.OrdinalIgnoreCase)).ToList();
    }
    
    public async Task CalculateTourAttributesAsync(string username, int tourId)
    {
        var allLogsCount = await _logRepository.GetLogsCountAsync(username);
        var logs = await _logRepository.GetAllLogsForTourAsync(username, tourId);
        var logsCount = logs.Count() > 0 ? logs.Count() : 0;
        
        var popularity = allLogsCount > 0 ? ((float)logsCount / (float)allLogsCount) * 100 : 0;
        
        var avgDifficulty = logs.Average(l => l.Difficulty);
        var totalTimes = (float)logs.Sum(l => l.TotalTime.TotalHours);
        var totalDistance = (float)logs.Sum(l => l.TotalDistance) / 1000;
        var childFriendliness = (avgDifficulty + totalTimes + totalDistance) / 30;

        await _logRepository.UpdateTourAttributesAsync(username, tourId, popularity, childFriendliness);
    }
}