using EventOrganizer.Core.Commands.EventCommands;
using EventOrganizer.Core.Commands;
using EventOrganizer.Core.DTO;
using EventOrganizer.Core.Queries.EventQueries;
using EventOrganizer.Core.Queries.UserQueries;
using EventOrganizer.Core.Queries;
using EventOrganizer.Core.Repositories;
using EventOrganizer.Core.Services;
using EventOrganizer.EF.MySql.Repositories;
using EventOrganizer.EF.MySql.Triggers;
using EventOrganizer.EF.MySql;
using EventOrganizer.EF;
using EventOrganizer.WebApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Diagnostics.CodeAnalysis;
using EventOrganizer.Core.Queries.CalendarQueries;
using EventOrganizer.Core.Queries.TagQueries;
using EventOrganizer.WebApi.Infrastructure;
using EventOrganizer.Core.Commands.SubscriptionCommands;
using System.Security.Cryptography.X509Certificates;
using EventOrganizer.WebApi;
using IdentityServer4.AccessTokenValidation;
using Microsoft.EntityFrameworkCore.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

var webOptions = builder.Configuration.GetSection(nameof(WebOptions)).Get<WebOptions>();

if (webOptions.UseCustomSslCertificates)
{
    var certs = new Dictionary<string, X509Certificate2>(StringComparer.OrdinalIgnoreCase)
    {
        ["localhost"] = new X509Certificate2("/app/certificates/localhost.pfx", "password"),
        ["host.docker.internal"] = new X509Certificate2("/app/certificates/host.docker.internal.pfx", "password")
    };

    using (var store = new X509Store(StoreName.Root, StoreLocation.CurrentUser))
    {
        store.Open(OpenFlags.ReadWrite);

        store.Add(certs["localhost"]);
        store.Add(certs["host.docker.internal"]);
    }

    builder.WebHost.ConfigureKestrel(options =>
        options.ConfigureHttpsDefaults(opt =>
        {
            opt.SslProtocols = System.Security.Authentication.SslProtocols.Tls12;
            opt.ServerCertificateSelector = (connectionContext, name) =>
            {
                if (name is not null && certs.TryGetValue(name, out var cert))
                    return cert;

                return certs["localhost"];
            };
        }));
}

builder.Services.AddDbContext<EventOrganazerMySqlDbContext>(options =>
{
    options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.UseTriggers(triggerOptions =>
    {
        triggerOptions.AddTrigger<BeforeEventModifying>();
        triggerOptions.AddTrigger<AfterTagToEventRemoving>();
    });
});

builder.Services.AddTransient<ISchedulerClient, SchedulerClient>();

builder.Services.AddTransient<IQuery<GetEventListQueryParameters, IList<EventDTO>>, GetEventListQuery>();
builder.Services.AddTransient<IQuery<GetEventByIdQueryParameters, EventDetailDTO>, GetEventByIdQuery>();
builder.Services.AddTransient<IQuery<VoidParameters, UserOwnEventsDTO>, GetCurrentUserOwnEventsQuery>();
builder.Services.AddTransient<IQuery<GetWeeklyScheduleQueryParameters, WeeklyScheduleDTO>, GetWeeklyScheduleQuery>();
builder.Services.AddTransient<ICommand<CreateEventCommandParameters, EventDetailDTO>, CreateEventCommand>();
builder.Services.AddTransient<ICommand<UpdateEventCommandParameters, EventDetailDTO>, UpdateEventCommand>();
builder.Services.AddTransient<ICommand<DeleteEventCommandParameters, VoidResult>, DeleteEventCommand>();
builder.Services.AddTransient<ICommand<ScheduleEventCommandParameters, EventDetailDTO>, ScheduleEventCommand>();
builder.Services.AddTransient<ICommand<CreateSubscriptionCommandParameters, VoidResult>, CreateSubscriptionCommand>();

builder.Services.AddTransient<IQuery<VoidParameters, IList<string>>, GetTagListQuery>();

builder.Services.AddTransient<IQuery<VoidParameters, UserDTO>, GetCurrentUserQuery>();

builder.Services.AddTransient<EventOrganazerDbContext, EventOrganazerMySqlDbContext>();

builder.Services.AddTransient<IEventRepository, EventRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<ITagRepository, TagRepository>();
builder.Services.AddTransient<ISubscriptionRepository, SubscriptionRepository>();
builder.Services.AddTransient<ILogRepository, LogRepository>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IUserContextAccessor, UserContextAccessor>();

builder.Services.AddTransient<IUserHandler, UserHandler>();
builder.Services.AddTransient<IWeekHandler, WeekHandler>();

builder.Services.AddTransient<IHealthService, HealthService>();

//builder.Services.AddSingleton<ILoggerProvider, CustomLoggerProvider>();

builder.Services.AddValidators();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Event Organizer",
        Description = "Web API for Event Management"
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

/*
builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.All ^ HttpLoggingFields.RequestHeaders;
    logging.MediaTypeOptions.AddText("application/javascript");
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;
});
*/

builder.Services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
    .AddIdentityServerAuthentication(options =>
    {
        options.ApiName = webOptions.WebApiName;
        options.Authority = webOptions.Authority;
    });

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins(webOptions.WebClient)
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

builder.Services.Configure<WebOptions>(builder.Configuration.GetSection(nameof(WebOptions)));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();

    var context = scope.ServiceProvider.GetRequiredService<EventOrganazerMySqlDbContext>();

    context.Database.Migrate();
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Event Organizer V1");
});

//app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseHttpLogging();

app.UseCors();

app.Run();



[ExcludeFromCodeCoverage]
public partial class Program
{ }
