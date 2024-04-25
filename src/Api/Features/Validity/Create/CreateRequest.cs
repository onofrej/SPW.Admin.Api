namespace SPW.Admin.Api.Features.Validity.Create;

[ExcludeFromCodeCoverage]
public sealed class CreateRequest
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool Status { get; set; }
    public Guid DomainId { get; set; }
}