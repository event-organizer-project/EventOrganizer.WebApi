using EventOrganizer.Core.Commands.EventCommands;
using EventOrganizer.Core.Commands;
using EventOrganizer.Core.Queries.EventQueries;
using EventOrganizer.Core.Queries;
using EventOrganizer.Core.Repositories;
using EventOrganizer.Domain.Models;
using EventOrganizer.EF.Repositories;
using EventOrganizer.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<EventOrganazerDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddTransient<IQuery<GetEventListQueryParamters, IList<EventModel>>, GetEventListQuery>();
builder.Services.AddTransient<ICommand<CreateEventCommandParameters, EventModel>, CreateEventCommand>();

builder.Services.AddTransient<IEventRepository, EventRepository>();

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

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Event Organizer V1");
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors();

app.MapControllers();

app.UseHttpLogging();

app.Run();
