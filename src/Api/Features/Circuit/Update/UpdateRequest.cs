namespace SPW.Admin.Api.Features.Circuit.Update;

[ExcludeFromCodeCoverage]
public sealed class UpdateRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}