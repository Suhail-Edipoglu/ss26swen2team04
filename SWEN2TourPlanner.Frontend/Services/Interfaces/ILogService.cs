using SWEN2TourPlanner.Models;

namespace SWEN2TourPlanner.Frontend.Services.Interfaces;

public interface ILogService
{
    public List<Log>? GetLogs(int tourId);
    public Log? GetLogById(int logId);
    public int? CreateLog(Log log);
    public void UpdateLog(Log log);
    public void DeleteLog(int? logId);
}