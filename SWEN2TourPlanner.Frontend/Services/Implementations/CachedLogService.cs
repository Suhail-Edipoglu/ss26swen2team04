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
                "Das ist ein Kommentar", 1.2f, 185, new TimeOnly(1, 30), 
                4.5f, 0),
            new Log(1, new DateTime(2026, 4, 16, 16, 0, 0), 
                "Das ist ein Kommentar", 1.5f, 185, new TimeOnly(1, 30), 
                4.0f, 1),
            new Log(2, new DateTime(2026, 4, 16, 20, 0, 0), 
                "Das ist ein Kommentar", 1.7f, 305, new TimeOnly(3, 0), 
                4.8f, 2)
        };
    }
    
    public List<Log>? GetLogs(int tourId)
    {
        return _logs?.Where(l => l.TourId == tourId).ToList();
    }

    public Log? GetLogById(int logId)
    {
        return _logs?.FirstOrDefault(t => t.Id == logId);
    }

    public int? CreateLog(Log log)
    {
        log.Id = _logs?.Count > 0 ? _logs.Max(l => l.Id) + 1 : 0;
        
        _logs?.Add(log);
        
        return log.Id;
    }

    public void UpdateLog(Log log)
    {
        if (_logs == null) return;
        
        var index = _logs.FindIndex(l => l.Id == log.Id);
        
        if (index >= 0) 
        {
            _logs[index] = log;
        }
    }

    public void DeleteLog(int? logId)
    {
        if (logId == null) return;
        _logs?.RemoveAll(l => l.Id == logId);
    }
}