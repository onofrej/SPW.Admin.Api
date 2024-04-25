using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Congregation;

internal static class Errors
{
    internal static Error ReturnInvalidEntriesError(string errorDetails) => new(errorCode: "CONG001",
        errorMessage: "Invalid entries", errorDetails);

    internal static Error ReturnCongregationNotFoundError() => new(errorCode: "CONG002",
        errorMessage: "Congregation not found");
}