using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Circuit;

internal static class Errors
{
    internal static Error ReturnInvalidEntriesError(string errorDetails) => new(errorCode: "US001",
        errorMessage: "Invalid entries", errorDetails);

    internal static Error ReturnCircuitNotFoundError() => new(errorCode: "US002",
        errorMessage: "Circuit not found");
}