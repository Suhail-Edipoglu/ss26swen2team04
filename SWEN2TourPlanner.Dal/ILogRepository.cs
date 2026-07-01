using SWEN2TourPlanner.Models;

namespace SWEN2TourPlanner.Dal;

public interface ILogRepository
{
    Task<List<Log>> GetAllLogsForTourAsync(string username, int tourId);
    Task<Log?> GetLogByIdAsync(string username, int logId);
    Task InsertLogAsync(string username, Log log);
    Task UpdateLogAsync(string username, Log log);
    Task<bool> DeleteLogAsync(string username, int logId);
    Task<int> GetLogsCountAsync(string username);
    Task UpdateTourAttributesAsync(string username, int tourId, float popularity, float childFriendliness);
}