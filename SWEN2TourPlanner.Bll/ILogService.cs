using SWEN2TourPlanner.Models;

namespace SWEN2TourPlanner.Bll;

public interface ILogService
{
    Task<Log> GetLogAsync(int logId, string username);
    Task<List<Log>> GetLogsAsync(int tourId, string username);
    Task<Log> CreateLogAsync(Log log, string username);
    Task<bool> UpdateLogAsync(Log log, string username);
    Task<bool> RemoveLogAsync(int logId, string username);
    Task<IEnumerable<Log>> FindMatchingLogsAsync(string username, int tourId, string? searchText = null);
}