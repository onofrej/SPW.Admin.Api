using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.User.Create;

[ExcludeFromCodeCoverage]
internal static class CreateErrors
{
    public static Error ReturnInvalidEntriesError(string errorDetails) => new(errorCode: "US001",
        errorMessage: "Invalid entries", errorDetails);
}