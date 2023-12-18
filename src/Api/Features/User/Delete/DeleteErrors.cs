using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.User.Delete
{
    internal static class DeleteErrors
    {
        public static Error ReturnInvalidEntriesError(string errorDetails) => new(errorCode: "US001",
        errorMessage: "Invalid entries", errorDetails);
    }
}