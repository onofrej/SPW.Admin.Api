using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Point;

internal static class Errors
{
    internal static Error ReturnInvalidEntriesError(string errorDetails) => new(errorCode: "POI001",
        errorMessage: "Invalid entries", errorDetails);

    internal static Error ReturnPointNotFoundError(string errorDetails) => new(errorCode: "POI002",
    errorMessage: "Point not found", errorDetails);
}