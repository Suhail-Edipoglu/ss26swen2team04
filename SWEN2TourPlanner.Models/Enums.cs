using System.Text.Json.Serialization;

namespace SWEN2TourPlanner.Models;

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