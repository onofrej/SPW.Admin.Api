using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Validity;

internal static class Errors
{
    internal static Error ReturnInvalidEntriesError(string errorDetails) => new(errorCode: "VAL001",
        errorMessage: "Invalid entries", errorDetails);

    internal static Error ReturnValidityNotFoundError() => new(errorCode: "VAL002",
        errorMessage: "Validity not found");
}