namespace SPW.Admin.Api.Shared;

[ExcludeFromCodeCoverage]
public record Response<T>(T? Data = default, object? Errors = default);