using SPW.Admin.Api.Shared;

namespace SPW.Admin.Api.Features.User.Create;

[ExcludeFromCodeCoverage]
internal static class Errors
{
    public static Error GetInvalidEntriesError(string errorDetails) => new(errorCode: "US001",
        errorMessage: "Invalid entries", errorDetails);
}