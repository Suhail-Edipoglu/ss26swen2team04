using SWEN2TourPlanner.Frontend.DTOs;
using SWEN2TourPlanner.Frontend.Services.Interfaces;
using System.Globalization;

namespace SWEN2TourPlanner.Frontend.Services;

public class CachedApiService : IApiService {
    private readonly List<UserData> _loginDataList = [
        new UserData { Username = "user1", Password = "password1" },
        new UserData { Username = "user2", Password = "password2" }
    ];

    private readonly List<Tour> _tours = [
        new Tour {
            Id = 1,
            Name = "Vienna City Ride",
            Description = "Bike tour from Prater to Stephansplatz.",
            From = "Prater",
            To = "Stephansplatz",
            TransportType = TransportType.Bicycle,
            Distance = 8,
            EstimatedTime = new TimeSpan(0, 35, 0),
            RouteInformation = "Praterstern -> Ringstrasse -> Stephansplatz",
            UserId = 1
        },
        new Tour {
            Id = 2,
            Name = "Airport Transfer",
            Description = "Fast route to Vienna International Airport.",
            From = "Vienna Central Station",
            To = "Vienna Airport",
            TransportType = TransportType.Train,
            Distance = 20,
            EstimatedTime = new TimeSpan(0, 22, 0),
            RouteInformation = "S7/Railjet route",
            UserId = 1
        }
    ];

    private readonly List<TourLog> _tourLogs = [
        new TourLog {
            Id = 1,
            Time = DateTime.UtcNow.AddDays(-2),
            Comment = "Nice weather and smooth ride.",
            Difficulty = 2.5f,
            TotalDistance = 8,
            TotalTime = new TimeSpan(2, 37, 0),
            Rating = 4.5f,
            TourId = 1
        },
        new TourLog {
            Id = 2,
            Time = DateTime.UtcNow.AddDays(-1),
            Comment = "Train was on time.",
            Difficulty = 1.0f,
            TotalDistance = 20,
            TotalTime = new TimeSpan(0, 22, 0),
            Rating = 4.0f,
            TourId = 2
        }
    ];

    public Task<bool> RegisterAsync(UserData userData) {
        if (_loginDataList.Any(ld => ld.Username == userData.Username)) {
            return Task.FromResult(false);
        }

        _loginDataList.Add(new UserData {
            Username = userData.Username,
            Password = userData.Password
        });

        return Task.FromResult(true);
    }

    public Task<string?> LoginAsync(UserData userData) {
        if (_loginDataList.Any(ld =>
            ld.Username == userData.Username && ld.Password == userData.Password)) {
            return Task.FromResult(userData.Username);
        }

        return Task.FromResult<string?>(null);
    }

    public Task<List<Tour>> GetToursAsync() {
        return Task.FromResult(_tours.Select(CloneTour).ToList());
    }

    public async Task<List<Tour>> SearchToursAsync(string searchTerm) {
        var normalizedSearchTerm = searchTerm.Trim();
        if (string.IsNullOrWhiteSpace(normalizedSearchTerm)) {
            return await GetToursAsync();
        }

        return _tours
            .Where(t =>
                t.Name.Contains(normalizedSearchTerm, StringComparison.OrdinalIgnoreCase) ||
                t.Description.Contains(normalizedSearchTerm, StringComparison.OrdinalIgnoreCase) ||
                t.From.Contains(normalizedSearchTerm, StringComparison.OrdinalIgnoreCase) ||
                t.To.Contains(normalizedSearchTerm, StringComparison.OrdinalIgnoreCase) ||
                t.TransportType.ToString().Contains(normalizedSearchTerm, StringComparison.OrdinalIgnoreCase) ||
                (t.Distance?.ToString(CultureInfo.InvariantCulture).Contains(normalizedSearchTerm, StringComparison.OrdinalIgnoreCase) ?? false) ||
                (t.EstimatedTime?.ToString("HH:mm").Contains(normalizedSearchTerm, StringComparison.OrdinalIgnoreCase) ?? false) ||
                (t.Popularity?.ToString(CultureInfo.InvariantCulture).Contains(normalizedSearchTerm, StringComparison.OrdinalIgnoreCase) ?? false) ||
                (t.ChildFriendliness?.ToString(CultureInfo.InvariantCulture).Contains(normalizedSearchTerm, StringComparison.OrdinalIgnoreCase) ?? false))
            .Select(CloneTour)
            .ToList();
    }

    public Task<Tour> GetTourByIdAsync(int tourId) {
        var tour = _tours.FirstOrDefault(t => t.Id == tourId)
            ?? throw new KeyNotFoundException($"Tour with id {tourId} was not found.");

        return Task.FromResult(CloneTour(tour));
    }

    public Task<int> CreateTourAsync(Tour tourData) {
        var newId = _tours.Count == 0 ? 1 : _tours.Max(t => t.Id ?? 0) + 1;
        var newTour = CloneTour(tourData);
        newTour.Id = newId;
        _tours.Add(newTour);

        return Task.FromResult(newId);
    }

    public Task<bool> UpdateTourAsync(Tour tourData) {
        if (tourData.Id is null) {
            return Task.FromResult(false);
        }

        var index = _tours.FindIndex(t => t.Id == tourData.Id.Value);
        if (index < 0) {
            return Task.FromResult(false);
        }

        _tours[index] = CloneTour(tourData);
        return Task.FromResult(true);
    }

    public Task<bool> DeleteTourAsync(int tourId) {
        var removed = _tours.RemoveAll(t => t.Id == tourId) > 0;
        if (!removed) {
            return Task.FromResult(false);
        }

        _tourLogs.RemoveAll(l => l.TourId == tourId);
        return Task.FromResult(true);
    }

    public Task<List<TourLog>> GetTourLogsAsync(int tourId) {
        return Task.FromResult(_tourLogs
            .Where(l => l.TourId == tourId)
            .Select(CloneTourLog)
            .ToList());
    }

    public async Task<List<TourLog>> SearchTourLogsAsync(int tourId, string searchTerm) {
        var normalizedSearchTerm = searchTerm.Trim();
        if (string.IsNullOrWhiteSpace(normalizedSearchTerm)) {
            return await GetTourLogsAsync(tourId);
        }

        return _tourLogs
            .Where(l => l.TourId == tourId)
            .Where(l =>
                l.Comment.Contains(normalizedSearchTerm, StringComparison.OrdinalIgnoreCase) ||
                l.Time.ToString("g").Contains(normalizedSearchTerm, StringComparison.OrdinalIgnoreCase) ||
                l.Difficulty.ToString(CultureInfo.InvariantCulture).Contains(normalizedSearchTerm, StringComparison.OrdinalIgnoreCase) ||
                l.TotalDistance.ToString(CultureInfo.InvariantCulture).Contains(normalizedSearchTerm, StringComparison.OrdinalIgnoreCase) ||
                l.TotalTime.ToString().Contains(normalizedSearchTerm, StringComparison.OrdinalIgnoreCase) ||
                l.Rating.ToString(CultureInfo.InvariantCulture).Contains(normalizedSearchTerm, StringComparison.OrdinalIgnoreCase))
            .Select(CloneTourLog)
            .ToList();
    }

    public Task<TourLog> GetTourLogByIdAsync(int tourLogId) {
        var log = _tourLogs.FirstOrDefault(l => l.Id == tourLogId)
            ?? throw new KeyNotFoundException($"Tour log with id {tourLogId} was not found.");

        return Task.FromResult(CloneTourLog(log));
    }

    public Task<int> CreateTourLogAsync(TourLog tourLogData) {
        if (_tours.All(t => t.Id != tourLogData.TourId)) {
            throw new KeyNotFoundException($"Tour with id {tourLogData.TourId} was not found.");
        }

        var newId = _tourLogs.Count == 0 ? 1 : _tourLogs.Max(l => l.Id ?? 0) + 1;
        var newLog = CloneTourLog(tourLogData);
        newLog.Id = newId;
        _tourLogs.Add(newLog);

        return Task.FromResult(newId);
    }

    public Task<bool> UpdateTourLogAsync(TourLog tourLogData) {
        if (tourLogData.Id is null) {
            return Task.FromResult(false);
        }

        var index = _tourLogs.FindIndex(l => l.Id == tourLogData.Id.Value);
        if (index < 0) {
            return Task.FromResult(false);
        }

        if (_tours.All(t => t.Id != tourLogData.TourId)) {
            return Task.FromResult(false);
        }

        _tourLogs[index] = CloneTourLog(tourLogData);
        return Task.FromResult(true);
    }

    public Task<bool> DeleteTourLogAsync(int tourLogId) {
        var removed = _tourLogs.RemoveAll(l => l.Id == tourLogId) > 0;
        return Task.FromResult(removed);
    }

    // Return copies so tests/components cannot mutate the cache by reference.
    private static Tour CloneTour(Tour source) {
        return new Tour {
            Id = source.Id,
            Name = source.Name,
            Description = source.Description,
            From = source.From,
            To = source.To,
            TransportType = source.TransportType,
            Distance = source.Distance,
            EstimatedTime = source.EstimatedTime,
            RouteInformation = source.RouteInformation,
            UserId = source.UserId,
            Popularity = source.Popularity,
            ChildFriendliness = source.ChildFriendliness,
        };
    }

    private static TourLog CloneTourLog(TourLog source) {
        return new TourLog {
            Id = source.Id,
            Time = source.Time,
            Comment = source.Comment,
            Difficulty = source.Difficulty,
            TotalDistance = source.TotalDistance,
            TotalTime = source.TotalTime,
            Rating = source.Rating,
            TourId = source.TourId
        };
    }
}