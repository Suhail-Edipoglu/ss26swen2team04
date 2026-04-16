using SWEN2TourPlanner.Models;

namespace SWEN2TourPlanner.Frontend.Services;

public interface ILogService
{
    public List<Log>? GetLogs(int tourId);
    public void CreateLog(Log log);
    public void UpdateLog(Log log);
    public void DeleteLog(int logId);
}