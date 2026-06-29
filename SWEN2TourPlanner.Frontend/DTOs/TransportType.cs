using System.Text.Json.Serialization;

namespace SWEN2TourPlanner.Frontend.DTOs;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TransportType
{
    None,
    Bicycle,
    Car,
    Bus,
    Train,
    Airplane
}