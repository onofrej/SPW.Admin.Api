namespace SPW.Admin.Api.Features.User.Get
{
    [ExcludeFromCodeCoverage]
    public sealed class Query
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; } = default;
        public DateTime BaptismDate { get; set; } = default;
        public string Privilege { get; set; } = string.Empty;
    }
}