using DataBridge.API.Brokers.Loggings;
using DataBridge.API.Brokers.Queues;
using DataBridge.API.Brokers.Sheets;
using DataBridge.API.Brokers.Storages;
using DataBridge.API.Filters;
using DataBridge.API.Middlewares;
using DataBridge.API.Services.Coordinators.Interfaces;
using DataBridge.API.Services.Coordinators.Services;
using DataBridge.API.Services.Foundations.ExternalPersonPets;
using DataBridge.API.Services.Foundations.ExternalPersonPets.Interfaces;
using DataBridge.API.Services.Foundations.ExternalPersonPets.Services;
using DataBridge.API.Services.Foundations.Persons.Interfaces;
using DataBridge.API.Services.Foundations.Persons.Services;
using DataBridge.API.Services.Foundations.Pets.Interfaces;
using DataBridge.API.Services.Foundations.Pets.Services;
using DataBridge.API.Services.Orchestrations.ExternalPersonPets.Interfaces;
using DataBridge.API.Services.Orchestrations.ExternalPersonPets.Services;
using DataBridge.API.Services.Orchestrations.People.Interfaces;
using DataBridge.API.Services.Orchestrations.People.Services;
using DataBridge.API.Services.Orchestrations.PersonPets.Interfaces;
using DataBridge.API.Services.Orchestrations.PersonPets.Services;
using DataBridge.API.Services.Processings.ExternalPersonPets.Interfaces;
using DataBridge.API.Services.Processings.ExternalPersonPets.Services;
using DataBridge.API.Services.Processings.People.Interfaces;
using DataBridge.API.Services.Processings.People.Services;
using DataBridge.API.Services.Processings.Pets.Interfaces;
using DataBridge.API.Services.Processings.Pets.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

namespace DataBridge.API;

public class Startup
{
    public Startup(IConfiguration configuration) =>
        Configuration = configuration;

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        var apiInfo = new OpenApiInfo
        {
            Title = "DataBridge.Api",
            Version = "v1",
        };

        services.AddControllers().AddJsonOptions(options =>
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

        services.AddControllers(options =>
        {
            options.Filters.Add<CustomExceptionFilter>(); 
        });

        services.AddDbContext<StorageBroker>();
        AddBrokers(services);
        AddFoundationServices(services);
        AddProcessingServices(services);
        AddOrchestrationServices(services);
        AddCoordinationServices(services);

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc(
                name: "v1",
                info: apiInfo);
        });
    }

    private static void AddBrokers(IServiceCollection services)
    {
        services.AddTransient<IStorageBroker, StorageBroker>();
        services.AddTransient<ILoggingBroker, LoggingBroker>();
        services.AddTransient<ISheetBroker, SheetBroker>();
        services.AddTransient<IQueueBroker, QueueBroker>();
    }

    private static void AddFoundationServices(IServiceCollection services)
    {
        services.AddTransient<IExternalPersonPetService, ExternalPersonPetService>();
        services.AddTransient<IExternalPersonPetEventService, ExternalPersonPetEventService>();
        services.AddTransient<IExternalPersonPetInputService, ExternalPersonPetInputService>();
        services.AddTransient<IPersonService, PersonService>();
        services.AddTransient<IPersonXMLService, PersonXMLService>();
        services.AddTransient<IPetService, PetService>();
    }

    private static void AddProcessingServices(IServiceCollection services)
    {
        services.AddTransient<IExternalPersonPetProcessingService, ExternalPersonPetProcessingService>();
        services.AddTransient<IExternalPersonPetEventProcessingService, ExternalPersonPetEventProcessingService>();
        services.AddTransient<IExternalPersonPetInputProcessingService, ExternalPersonPetInputProcessingService>();

        services.AddTransient<IPersonProcessingService, PersonProcessingService>();
        services.AddTransient<IPersonXMLProcessingService, PersonXMLProcessingService>();
        services.AddTransient<IPetProcessingService, PetProcessingService>();
    }

    private static void AddOrchestrationServices(IServiceCollection services)
    {
        services.AddTransient<IExternalPersonPetOrchestrationService, ExternalPersonPetOrchestrationService>();
        services.AddTransient<IExternalPersonPetEventOrchestrationService, ExternalPersonPetEventOrchestrationService>();
        services.AddTransient<IPersonPetOrchestrationService, PersonPetOrchestrationService>();
        services.AddTransient<IPersonOrchestrationService, PersonOrchestrationService>();
    }

    private static void AddCoordinationServices(IServiceCollection services) =>
        services.AddTransient<IPersonPetEventCoordinationService, PersonPetEventCoordinationService>();

    public void Configure(IApplicationBuilder app, IWebHostEnvironment environment)
    {
        if (environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint(
                    url: "/swagger/v1/swagger.json",
                    name: "DataBridge.Api v1");
            });
        }

        app.UseMiddleware<ExceptionHandlingMiddleware>();
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
            endpoints.MapControllers());
    }
}