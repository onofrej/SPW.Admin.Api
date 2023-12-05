namespace SPW.Admin.Api.Shared.Models;

[ExcludeFromCodeCoverage]
public sealed class Result<T>
{
    public Result(T? data = default, Error error = default)
    {
        Data = data;
        Error = error;
        HasFailed = !string.IsNullOrWhiteSpace(Error.ErrorMessage);
    }

    public T? Data { get; private set; }

    public Error Error { get; private set; }

    public bool HasFailed { get; private set; }
}