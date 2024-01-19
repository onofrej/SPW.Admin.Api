using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Schedule;

internal static class Errors
{
    internal static Error ReturnInvalidEntriesError(string errorDetails) => new(errorCode: "SCDL001",
        errorMessage: "Invalid entries", errorDetails);

    internal static Error ReturnScheduleNotFoundError() => new(errorCode: "SCDL002",
        errorMessage: "Schedule not found");
}