using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Holiday;

internal static class Errors
{
    internal static Error ReturnInvalidEntriesError(string errorDetails) => new(errorCode: "HOL001",
        errorMessage: "Invalid entries", errorDetails);

    internal static Error ReturnHolidayNotFoundError() => new(errorCode: "HOL002",
        errorMessage: "Holiday not found");
}