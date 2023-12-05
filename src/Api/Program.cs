using SPW.Admin.Api.DependencyInjection;
using SPW.Admin.Api.Shared.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddAWSLambdaHosting(LambdaEventSource.HttpApi);

builder.Services.InitializeApplicationDependencies();

builder.Services.AddCarter();

builder.Services.AddMediatR(configuration =>
    configuration.RegisterServicesFromAssembly(typeof(Program).Assembly));

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingPipelineBehavior<,>));

builder.Services.AddScoped<IDynamoDBContext, DynamoDBContext>();

builder.Services.AddAWSService<IAmazonDynamoDB>();

builder.Logging.ClearProviders();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Host.UseSerilog();

var app = builder.Build();

app.MapCarter();

app.UseHttpsRedirection();

app.UseMiddleware<GlobalExceptionHandler>();

app.UseAuthorization();

app.MapControllers();

app.Run();

[ExcludeFromCodeCoverage]
public partial class Program
{
    protected Program()
    { }
}