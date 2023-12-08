namespace SPW.Admin.Api.Features.User.Get
{
    [ExcludeFromCodeCoverage]
    public sealed class EndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/users", GetAllUsersAsync);
            app.MapGet("users{id:int}", GetUserByIdAsync);
            app.MapGet("/users/{name}", GetUserByNameAsync);
        }

        public static async Task<string> GetAllUsersAsync()
        {
            return await Task.FromResult("Ok");
        }

        public static async Task<string> GetUserByIdAsync(int id)
        {
            return await Task.FromResult("Ok");
        }

        public static async Task<string> GetUserByNameAsync(string name)
        {
            return await Task.FromResult("Ok");
        }
    }
}