using SWEN2TourPlanner.Models;

namespace SWEN2TourPlanner.Frontend.Services.Implementations;

public class CachedLogService : ILogService
{
    private List<Log>? _logs;
    
    public CachedLogService()
    {
        _logs = new List<Log>()
        {
            new Log(0, new DateTime(2026, 4, 16, 14, 0, 0), 
                "Das ist ein Kommentar", 1.2f, 185, 90, 
                4.5f, 0),
            new Log(1, new DateTime(2026, 4, 16, 16, 0, 0), 
                "Das ist ein Kommentar", 1.5f, 185, 90, 
                4.0f, 1),
            new Log(2, new DateTime(2026, 4, 16, 20, 0, 0), 
                "Das ist ein Kommentar", 1.7f, 305, 180, 
                4.8f, 2)
        };
    }
    
    public List<Log>? GetLogs(int tourId)
    {
        return _logs?.Where(l => l.TourId == tourId).ToList();
    }

    public void CreateLog(Log log)
    {
        throw new NotImplementedException();
    }

    public void UpdateLog(Log log)
    {
        throw new NotImplementedException();
    }

    public void DeleteLog(int logId)
    {
        _logs?.RemoveAll(l => l.Id == logId);
    }
}