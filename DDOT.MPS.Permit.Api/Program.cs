using AutoMapper;
using DDOT.MPS.Permit.Api.CustomConfigurations;
using DDOT.MPS.Permit.Api.Managers.LocationManager;
using DDOT.MPS.Permit.Api.Managers;
using DDOT.MPS.Permit.Api.Managers.TestManager;
using DDOT.MPS.Permit.Api.Middlewares;
using DDOT.MPS.Permit.Core.CoreSettings;
using DDOT.MPS.Permit.Core.Utilities;
using DDOT.MPS.Permit.DataAccess.DBContexts;
using DDOT.MPS.Permit.DataAccess.Repositories.EwrRepository;
using DDOT.MPS.Permit.DataAccess.Repositories.TestRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using DDOT.MPS.Permit.DataAccess.Repositories.UserRepository;
using DDOT.MPS.Permit.Api.Managers.UserManager;
using DDOT.MPS.Permit.DataAccess.Repositories;
using DDOT.MPS.Permit.DataAccess.Repositories.InspectionRepository;
using DDOT.MPS.Permit.DataAccess.Repositories.ApplicationTypeRepository;
using DDOT.MPS.Permit.Api.Managers.InspectionManagement;
using DDOT.MPS.Permit.Api.Managers.PdrmSettingsManager;
using DDOT.MPS.Permit.DataAccess.Repositories.SwoRepository;
using DDOT.MPS.Permit.Api.Managers.SwoManagement;
using DDOT.MPS.Permit.DataAccess.Repositories.PdrmSettingsRepository;

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

// DB service config
builder.Services.AddDbContext<MpsDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Automapper config
IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Register IHttpClientFactory
builder.Services.AddHttpClient();

//Application Insights
builder.Services.AddApplicationInsightsTelemetry();
/*
 * Dependency Injection
 */
// Repositories
builder.Services.AddScoped<ITestRepository, TestRepository>();
builder.Services.AddScoped<IMpsProjectRepository, MpsProjectRepository>();
builder.Services.AddScoped<IMpsEwrRepository, MpsEwrRepository>();
builder.Services.AddScoped<IMpsUserRepository, MpsUserRepository>();
builder.Services.AddScoped<IInspectionRepository, InspectionRepository>();
builder.Services.AddScoped<IMpsApplicationTypeRepository, MpsApplicationTypeRepository>();
builder.Services.AddScoped<ISwoRepository, SwoRepository>();
builder.Services.AddScoped<IPdrmSettingsRepository, PdrmSettingsRepository>();


// Add logging services
builder.Services.AddLogging();

// Managers
builder.Services.AddScoped<ITestManager, TestManager>();
builder.Services.AddScoped<IProjectManager, ProjectManager>();
builder.Services.AddScoped<ILocationManager, LocationManager>();
builder.Services.AddScoped<IEwrManager, EwrManager>();
builder.Services.AddScoped<IUserManager, UserManager>();
builder.Services.AddScoped<IInspectionManager, InspectionManager>();
builder.Services.AddScoped<ISwoManager, SwoManager>();
builder.Services.AddScoped<IMeetingSettingsManager, MeetingSettingsManager>();

// Utils
builder.Services.AddScoped<IAppUtils, AppUtils>();
builder.Services.AddScoped<IStringUtils, StringUtils>();

// Options
builder.Services.AddOptions<GlobalAppSettings>()
    .Configure(options => builder.Configuration.GetSection("GlobalAppSettings").Bind(options));

builder.Services.Configure<GlobalAppSettings>(builder.Configuration.GetSection("GlobalAppSettings"));
builder.Services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<GlobalAppSettings>>().Value);


// Add authorization services
builder.Services.AddAuthorization();
builder.Services.AddControllers();

// Register the environment name as a singleton service
builder.Services.AddSingleton(builder.Environment.EnvironmentName);

// Register custom health check
builder.Services.AddHealthChecks()
    .AddCheck<CustomHealthCheck>("custom_health_check");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

WebApplication? app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.MapHealthChecks("/api/v1/healthcheck");

// global error handler
app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
