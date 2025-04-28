using NameSorter.Application.Interfaces;
using NameSorter.Application.Services;
using NLog;
using SorterAPI.Common;

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
var isprod = environment.Contains(Environments.Production, StringComparison.InvariantCultureIgnoreCase);
LogManager.Setup().LoadConfigurationFromFile(isprod ? "nlog.config" : "nlog.debug.config");
var logger = LogManager.GetCurrentClassLogger();
try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
    builder.Configuration.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASNETCORE_ENVIRONMENT")}.json", optional: true);
    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options =>
    {
        options.SupportNonNullableReferenceTypes();
        options.MapType<IFormFile>(() => new Microsoft.OpenApi.Models.OpenApiSchema
        {
            Type = "string",
            Format = "binary"
        });
    });
    builder.Services.AddScoped<ISortService, SortService>();
    builder.Services.AddScoped<IFileService, FileService>();
    builder.Services.AddExceptionHandler<CustomExceptionHandler>();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    app.UseExceptionHandler(_ => { });
    app.UseStatusCodePages();

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();

}
catch (Exception ex)
{
    logger.Error(ex, "Stopped program beacuse of exception");
    throw;
}
finally
{
    LogManager.Shutdown();
}

