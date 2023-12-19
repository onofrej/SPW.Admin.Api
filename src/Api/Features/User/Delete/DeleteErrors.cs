using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.User.Delete
{
    internal static class DeleteErrors
    {
        internal static Error ReturnInvalidEntriesError(string errorDetails) => new(errorCode: "US001",
        errorMessage: "Invalid entries", errorDetails);

        internal static Error ReturnUserNotFoundError() => new(errorCode: "US002",
            errorMessage: "User not found");
    }
}