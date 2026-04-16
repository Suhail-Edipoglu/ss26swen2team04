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
                new DateTime(0,0, 0, 1, 20, 0), 
                "Hier stehen die Routeninformationen", 
                "Path", 0, _logService.GetLogs(0)),
            new Tour(1, "TestTour2", "Das ist die zweite Test Tour", 
                "Linz", "Wien", TransportType.Train, 184, 
                new DateTime(0,0, 0, 1, 20, 0), 
                "Hier stehen die Routeninformationen", 
                "Path", 0, _logService.GetLogs(1)),
            new Tour(2, "TestTour3", "Das ist die dritte Test Tour", 
                "Wien", "Salzburg", TransportType.Train, 298, 
                new DateTime(0,0, 0, 2, 30, 0), 
                "Hier stehen die Routeninformationen", 
                "Path", 0, _logService.GetLogs(2)),
        };
    }
    
    public List<Tour>? GetTours(int userId)
    {
        return _tours;
    }

    public void CreateTour(Tour tour)
    {
        _tours?.Add(tour);
    }

    public void UpdateTour(Tour tour)
    {
        _tours?.Where(t => t.Id == tour.Id).ToList().ForEach(t => t = tour);
    }

    public void DeleteTour(int tourId)
    {
        _tours?.RemoveAll(t => t.Id == tourId);
    }
}