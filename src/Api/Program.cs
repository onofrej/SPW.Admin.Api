using SPW.Admin.Api.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddAWSLambdaHosting(LambdaEventSource.HttpApi);
builder.Services.InitializeApplicationServices();
builder.Services.AddCarter();

builder.Logging.ClearProviders();
builder.Services.AddMediatR(configuration =>
    configuration.RegisterServicesFromAssembly(typeof(Program).Assembly));

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Host.UseSerilog();

var app = builder.Build();

app.MapCarter();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.MapEndpoints();

app.Run();

[ExcludeFromCodeCoverage]
public partial class Program
{
    protected Program()
    { }
}