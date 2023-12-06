using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.User.Create;

[ExcludeFromCodeCoverage]
internal static class Errors
{
    public static Error CreateInvalidEntriesError(string errorDetails) => new(errorCode: "US001",
        errorMessage: "Invalid entries", errorDetails);
}