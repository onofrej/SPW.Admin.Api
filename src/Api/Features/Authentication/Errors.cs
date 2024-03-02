using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Authentication;

internal static class Errors
{
    internal static Error ReturnInvalidEntriesError(string errorDetails) => new(errorCode: "AUTH001",
      errorMessage: "Invalid entries", errorDetails);
}