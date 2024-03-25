using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.SpecialDay;

internal static class Errors
{
    internal static Error ReturnInvalidEntriesError(string errorDetails) => new(errorCode: "SPD001",
        errorMessage: "Invalid entries", errorDetails);

    internal static Error ReturnSpecialDayNotFoundError() => new(errorCode: "SPD002",
        errorMessage: "Special day not found");
}