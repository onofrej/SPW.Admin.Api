using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.User.Update;

[ExcludeFromCodeCoverage]
internal static class UpdateErrors
{
    public static Error ReturnInvalidEntriesError(string errorDetails) => new(errorCode: "US001",
        errorMessage: "Invalid entries", errorDetails);
}