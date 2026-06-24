using SWEN2TourPlanner.Models;

namespace SWEN2TourPlanner.Dal;

public interface ILogRepository
{
    Task<List<Log>> GetAllLogsForTourAsync(string username, int tourId);
    Task<Log?> GetLogByIdAsync(string username, int logId);
    Task InsertLogAsync(string username, int tourId, Log log);
    Task UpdateLogAsync(string username, Log log);
    Task<bool> DeleteLogAsync(string username, int logId);
}