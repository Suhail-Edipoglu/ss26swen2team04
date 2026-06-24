using SWEN2TourPlanner.Models;

namespace SWEN2TourPlanner.Bll;

public interface ILogService
{
    Task<Log?> GetLogAsync(int logId);
    Task<Log?> GetLogsAsync(int tourId, string username);
    Task<Log> CreateLogAsync(Log log);
    Task<bool> UpdateLogAsync(Log log);
    Task<bool> RemoveLogAsync(int logId);
    Task<IEnumerable<Log>> FindMatchingLogsAsync(string username, int tourId, string? searchText = null);
}