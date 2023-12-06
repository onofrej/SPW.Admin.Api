using SPW.Admin.Api.DependencyInjection;
using SPW.Admin.Api.Shared.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAWSLambdaHosting(LambdaEventSource.HttpApi);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.InitializeApplicationDependencies();
builder.Services.AddCarter();
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingPipelineBehavior<,>));
builder.Services.AddScoped<IDynamoDBContext, DynamoDBContext>();
builder.Services.AddAWSService<IAmazonDynamoDB>();
builder.Services.AddMediatR(configuration =>
    configuration.RegisterServicesFromAssembly(typeof(Program).Assembly));

builder.Logging.ClearProviders();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Host.UseSerilog();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapCarter();
app.UseMiddleware<GlobalExceptionHandler>();
app.Run();

[ExcludeFromCodeCoverage]
public partial class Program
{
    protected Program()
    { }
}