namespace SPW.Admin.Api.Users.UseCases.Create;

internal interface IUseCase
{
    Task ExecuteAsync(Input input, CancellationToken cancellationToken);
}