# SWEN2TourPlanner.Frontend.Tests

Unit tests for `CachedApiService` in-memory behavior.

## Run

```sh
dotnet test "/home/janybanny/RiderProjects/SWEN2TourPlanner/SWEN2TourPlanner.Frontend.Tests/SWEN2TourPlanner.Frontend.Tests.csproj"
```

## What is covered

- Authentication checks (`RegisterAsync`, `LoginAsync`)
- Tour CRUD operations
- Tour log CRUD operations
- Error/not-found paths and referential checks
- Defensive copy behavior for returned collections

