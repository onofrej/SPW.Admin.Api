namespace SPW.Admin.Api.Shared.Models;

[ExcludeFromCodeCoverage]
public record Response<T>(T? Data = default, object? Errors = default);