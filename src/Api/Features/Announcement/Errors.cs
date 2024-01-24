using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Announcement;

internal static class Errors
{
    internal static Error ReturnInvalidEntriesError(string errorDetails) => new(errorCode: "ANN001",
       errorMessage: "Invalid entries", errorDetails);

    internal static Error ReturnAnnouncementNotFoundError() => new(errorCode: "ANN002",
        errorMessage: "Announcement not found");
}