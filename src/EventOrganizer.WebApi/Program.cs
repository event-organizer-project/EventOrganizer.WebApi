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
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Diagnostics.CodeAnalysis;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<EventOrganazerMySqlDbContext>(options => {
    options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.UseTriggers(triggerOptions => {
        triggerOptions.AddTrigger<BeforeEventModifying>();
        triggerOptions.AddTrigger<AfterTagToEventRemoving>();
    });
});

builder.Services.AddTransient<IQuery<GetEventListQueryParameters, IList<EventDTO>>, GetEventListQuery>();
builder.Services.AddTransient<IQuery<GetEventByIdQueryParameters, EventDetailDTO>, GetEventByIdQuery>();
builder.Services.AddTransient<IQuery<VoidParameters, UserOwnEventsDTO>, GetCurrentUserOwnEventsQuery>();
builder.Services.AddTransient<ICommand<CreateEventCommandParameters, EventDetailDTO>, CreateEventCommand>();
builder.Services.AddTransient<ICommand<UpdateEventCommandParameters, EventDetailDTO>, UpdateEventCommand>();
builder.Services.AddTransient<ICommand<DeleteEventCommandParameters, VoidResult>, DeleteEventCommand>();
builder.Services.AddTransient<ICommand<ScheduleEventCommandParameters, EventDetailDTO>, ScheduleEventCommand>();


builder.Services.AddTransient<IQuery<VoidParameters, UserDTO>, GetCurrentUserQuery>();

builder.Services.AddTransient<EventOrganazerDbContext, EventOrganazerMySqlDbContext>();

builder.Services.AddTransient<IEventRepository, EventRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IUserContextAccessor, UserContextAccessor>();

builder.Services.AddTransient<IUserHandler, UserHandler>();

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

builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.All ^ HttpLoggingFields.RequestHeaders;
    logging.MediaTypeOptions.AddText("application/javascript");
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;
});

builder.Services.AddAuthentication("Bearer")
    .AddIdentityServerAuthentication("Bearer", options =>
    {
        options.ApiName = "eventorganizerapi";
        options.Authority = builder.Configuration["Authority"];
    });

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins(builder.Configuration.GetValue<string>("AllowedOrigins:WebClient"))
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Event Organizer V1");
});

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseHttpLogging();

app.UseCors();

app.Run();



[ExcludeFromCodeCoverage]
public partial class Program
{ }
