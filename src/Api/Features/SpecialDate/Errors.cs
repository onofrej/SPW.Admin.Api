using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.SpecialDate;

internal static class Errors
{
    internal static Error ReturnInvalidEntriesError(string errorDetails) => new(errorCode: "SPD001",
        errorMessage: "Invalid entries", errorDetails);

    internal static Error ReturnSpecialDateNotFoundError() => new(errorCode: "SPD002",
        errorMessage: "Special date not found");

    internal static Error ReturnCircuitNotFoundError() => new(errorCode: "SPD003",
        errorMessage: "Circuit not found");
}