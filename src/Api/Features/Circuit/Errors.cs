using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Circuit;

internal static class Errors
{
    internal static Error ReturnInvalidEntriesError(string errorDetails) => new(errorCode: "CIRC001",
        errorMessage: "Invalid entries", errorDetails);

    internal static Error ReturnCircuitNotFoundError() => new(errorCode: "CIRC002",
        errorMessage: "Circuit not found");
}