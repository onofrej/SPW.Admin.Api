using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Domain
{
    internal static class Errors
    {
        internal static Error ReturnInvalidEntriesError(string errorDetails) => new(errorCode: "DM001",
        errorMessage: "Invalid entries", errorDetails);

        internal static Error ReturnDomainNotFoundError() => new(errorCode: "DM002",
            errorMessage: "Domain not found");
    }
}