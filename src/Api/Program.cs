using SPW.Admin.Api.DependencyInjection;
using SPW.Admin.Api.Users.UseCases.Create;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddAWSLambdaHosting(LambdaEventSource.HttpApi);
builder.Services.InitializeAppliactionServices();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.MapEndpoints();

app.Run();