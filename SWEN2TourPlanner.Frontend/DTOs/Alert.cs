using MudBlazor;

namespace SWEN2TourPlanner.Frontend.DTOs;

public record Alert {
    public Alert(string message, Severity severity) {
        Message = message;
        Severity = severity;
    }
    internal string Message { get; }
    internal Severity Severity { get; }
}