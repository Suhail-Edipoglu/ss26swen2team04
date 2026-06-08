using SWEN2TourPlanner.Frontend.DTOs;
using SWEN2TourPlanner.Frontend.Services;
using NUnit.Framework;

namespace SWEN2TourPlanner.Frontend.Tests;

public class CachedApiServiceTests {
    [Test]
    public async Task RegisterAsync_ReturnsFalse_WhenUsernameAlreadyExists() {
        var service = CreateService();

        var result = await service.RegisterAsync(new LoginData {
            Username = "user1",
            Password = "anything"
        });

        Assert.That(result, Is.False);
    }

    [Test]
    public async Task LoginAsync_ReturnsTrue_ForRegisteredUser() {
        var service = CreateService();

        await service.RegisterAsync(new LoginData {
            Username = "new-user",
            Password = "pw"
        });

        var loginResult = await service.LoginAsync(new LoginData {
            Username = "new-user",
            Password = "pw"
        });

        Assert.That(loginResult, Is.True);
    }

    [Test]
    public async Task GetToursAsync_ReturnsClonedData() {
        var service = CreateService();

        var firstRead = await service.GetToursAsync();
        firstRead[0].Name = "Mutated Name";

        var secondRead = await service.GetToursAsync();

        Assert.That(secondRead[0].Name, Is.Not.EqualTo("Mutated Name"));
    }

    [Test]
    public void GetTourByIdAsync_Throws_WhenTourDoesNotExist() {
        var service = CreateService();

        Assert.ThrowsAsync<KeyNotFoundException>(async () => await service.GetTourByIdAsync(9999));
    }

    [Test]
    public async Task CreateTourAsync_AssignsNewId_AndPersistsTour() {
        var service = CreateService();
        var newTour = CreateTour("Danube Route", TransportType.Bicycle);

        var newId = await service.CreateTourAsync(newTour);
        var loadedTour = await service.GetTourByIdAsync(newId);

        Assert.That(newId, Is.GreaterThan(0));
        Assert.That(loadedTour.Id, Is.EqualTo(newId));
        Assert.That(loadedTour.Name, Is.EqualTo("Danube Route"));
    }

    [Test]
    public async Task UpdateTourAsync_ReturnsFalse_WhenIdIsMissing() {
        var service = CreateService();
        var tourWithoutId = CreateTour("No Id Tour", TransportType.Car);

        var updated = await service.UpdateTourAsync(tourWithoutId);

        Assert.That(updated, Is.False);
    }

    [Test]
    public async Task DeleteTourAsync_RemovesTourAndItsLogs() {
        var service = CreateService();

        var deleted = await service.DeleteTourAsync(1);
        var logsAfterDelete = await service.GetTourLogsAsync(1);

        Assert.That(deleted, Is.True);
        Assert.That(logsAfterDelete, Is.Empty);
        Assert.ThrowsAsync<KeyNotFoundException>(async () => await service.GetTourByIdAsync(1));
    }

    [Test]
    public async Task GetTourLogsAsync_ReturnsOnlyMatchingTourLogs() {
        var service = CreateService();

        var logs = await service.GetTourLogsAsync(2);

        Assert.That(logs, Has.Count.EqualTo(1));
        Assert.That(logs.All(log => log.TourId == 2), Is.True);
    }

    [Test]
    public void GetTourLogByIdAsync_Throws_WhenLogDoesNotExist() {
        var service = CreateService();

        Assert.ThrowsAsync<KeyNotFoundException>(async () => await service.GetTourLogByIdAsync(9999));
    }

    [Test]
    public void CreateTourLogAsync_Throws_WhenReferencedTourDoesNotExist() {
        var service = CreateService();
        var invalidLog = CreateTourLog(tourId: 555);

        Assert.ThrowsAsync<KeyNotFoundException>(async () => await service.CreateTourLogAsync(invalidLog));
    }

    [Test]
    public async Task UpdateTourLogAsync_ReturnsFalse_WhenTourDoesNotExist() {
        var service = CreateService();

        var existingLog = await service.GetTourLogByIdAsync(1);
        existingLog.TourId = 8888;

        var updated = await service.UpdateTourLogAsync(existingLog);

        Assert.That(updated, Is.False);
    }

    [Test]
    public async Task DeleteTourLogAsync_RemovesLog() {
        var service = CreateService();

        var deleted = await service.DeleteTourLogAsync(2);

        Assert.That(deleted, Is.True);
        Assert.ThrowsAsync<KeyNotFoundException>(async () => await service.GetTourLogByIdAsync(2));
    }

    private static CachedApiService CreateService() => new();

    private static Tour CreateTour(string name, TransportType transportType) {
        return new Tour {
            Name = name,
            Description = "Test description",
            From = "Start",
            To = "Destination",
            TransportType = transportType,
            Distance = 12,
            EstimatedTime = new TimeOnly(1, 15),
            RouteInformation = "Turn left then right",
            ImgPath = "images/test.jpg",
            UserId = 1
        };
    }

    private static TourLog CreateTourLog(int tourId) {
        return new TourLog {
            Time = DateTime.UtcNow,
            Comment = "Test log",
            Difficulty = 2.0f,
            TotalDistance = 10,
            TotalTime = new TimeOnly(0, 50),
            Rating = 4.0f,
            TourId = tourId
        };
    }
}
