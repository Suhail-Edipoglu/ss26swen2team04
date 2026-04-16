using SWEN2TourPlanner.Models;

namespace SWEN2TourPlanner.Frontend.Services.Implementations;

public class CachedTourService : ITourService
{
    private readonly ILogService _logService;
    
    private List<Tour>? _tours;

    public CachedTourService(ILogService logService)
    {
        _logService = logService;
        _tours = new List<Tour>()
        {
            new Tour(0, "TestTour1", "Das ist die erste Test Tour", 
                "Wien", "Linz", TransportType.Train, 184, 
                new TimeOnly(1, 20), 
                "Hier stehen die Routeninformationen", 
                "Path", 0, _logService.GetLogs(0)),
            new Tour(1, "TestTour2", "Das ist die zweite Test Tour", 
                "Linz", "Wien", TransportType.Train, 184, 
                new TimeOnly(1, 20), 
                "Hier stehen die Routeninformationen", 
                "Path", 0, _logService.GetLogs(1)),
            new Tour(2, "TestTour3", "Das ist die dritte Test Tour", 
                "Wien", "Salzburg", TransportType.Train, 298, 
                new TimeOnly(2, 30), 
                "Hier stehen die Routeninformationen", 
                "Path", 0, _logService.GetLogs(2)),
        };
    }
    
    public List<Tour>? GetTours(int userId)
    {
        return _tours?.Where(t => t.UserId == userId).ToList();
    }
    
    public Tour? GetTourById(int tourId)
    {
        var tour = _tours?.FirstOrDefault(t => t.Id == tourId);
        tour?.Logs = _logService?.GetLogs(tourId);
        return tour;
    }

    public int? CreateTour(Tour tour)
    {
        tour.Id = _tours?.Count > 0 ? _tours.Max(t => t.Id) + 1 : 0;
        
        _tours?.Add(tour);
        
        return tour.Id;
    }

    public void UpdateTour(Tour tour)
    {
        if (_tours == null) return;
        
        var index = _tours.FindIndex(t => t.Id == tour.Id);

        if (index >= 0)
        { 
            _tours[index] = tour;
        }
    }

    public void DeleteTour(int tourId)
    {
        _logService?.GetLogs(tourId)?.ForEach(l => _logService.DeleteLog(l.Id));
        _tours?.RemoveAll(t => t.Id == tourId);
    }
}