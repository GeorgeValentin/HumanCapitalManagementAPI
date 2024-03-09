using HumanCapitalManagement.Domain.Data;
using Microsoft.EntityFrameworkCore;
using HumanCapitalManagement.Entities.Profiles;
using HumanCapitalManagement.Service.Middleware;
using HumanCapitalManagement.API.DependencyInjection;
using HumanCapitalManagement.API.DependencyInjection.Skill;
using HumanCapitalManagement.API.DependencyInjection.Contract;
using HumanCapitalManagement.API.DependencyInjection.Employee;
using HumanCapitalManagement.API.DependencyInjection.Address;
using HumanCapitalManagement.API.DependencyInjection.Institution;
using Serilog;
using HumanCapitalManagement.API.DependencyInjection.JobTitle;
using HumanCapitalManagement.API.DependencyInjection.Feedback;
using HumanCapitalManagement.API.DependencyInjection.Authentication;
using HumanCapitalManagement.API.DependencyInjection.JwtToken;
using HumanCapitalManagement.API.DependencyInjection.KeyVault;
using HumanCapitalManagement.API.DependencyInjection.AzureSvBus;
using HumanCapitalManagement.API.DependencyInjection.Department;
using HumanCapitalManagement.API.DependencyInjection.Nationality;
using HumanCapitalManagement.API.DependencyInjection.Degree;

namespace HumanCapitalManagement.API;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Host.UseSerilog((ctx, lc) => lc
            .ReadFrom.ConfigurationSection(ctx.Configuration.GetSection("Logging"))
            .WriteTo.File("Logs/" + DateTime.Now.ToString("dd") + "/"+ DateTime.Now.ToString("HH") + "/" + ".txt",
                rollingInterval: RollingInterval.Minute,
                outputTemplate: "[{Timestamp:dd/MM/yyyy HH:mm:ss.fff} {Level:u3}] {Message:lj}{NewLine}{Exception}{NewLine}")
            .WriteTo.Console(outputTemplate: "[{Timestamp:dd/MM/yyyy HH:mm:ss.fff} {Level:u3}] {Message:lj}{NewLine}{Exception}{NewLine}")
        );

        builder.Services.AddControllers()
            .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddAuthentication("OAuth")       
            .AddJwtBearer("OAuth", configureOptions: null!);

        builder.Services.AddCors(policy =>
        {
            policy.AddPolicy("CorsPolicy", opt => opt
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod()
                .WithExposedHeaders("X-Pagination"));
        });

        if (builder.Environment.IsDevelopment())
        {
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(
                        builder.Configuration.GetConnectionString("DefaultDevConnection"));
            });
        }
        else
        {
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(
                        builder.Configuration.GetConnectionString("DefaultProdConnection"));
            });
        }

        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        builder.Services.AddAutoMapper(typeof(EmployeesProfile));
        builder.Services.AddAutoMapper(typeof(SkillsProfile));
        builder.Services.AddAutoMapper(typeof(ContractProfile));
        builder.Services.AddAutoMapper(typeof(JobTitlesProfile));
        builder.Services.AddAutoMapper(typeof(FeedbackProfile));
        builder.Services.AddAutoMapper(typeof(InstitutionProfile));
        builder.Services.AddAutoMapper(typeof(DepartmentsProfile));
        builder.Services.AddAutoMapper(typeof(NationalitiesProfile));

        builder.Services
            .AddEmployeeServices()
            .AddSkillServices()
            .AddAddressServices()
            .AddContractServices()
            .AddJobTitleServices()
            .AddEntitiesServices()
            .AddInstitutionServices()
            .AddFeedbackServices()
            .AddDepartmentServices()
            .AddNationalitiesServices()
            .AddDegreeServices();

        builder.Services
            .AddJobTitleValidations()
            .AddSkillValidations()
            .AddContractValidations()
            .AddEmployeeValidations()
            .AddInstitutionValidations()
            .AddFeedbackValidations();

        builder.Services
            .AddAuthenticationServices()
            .AddKeyVaultServices()
            .AddJwtServices()
            .AddMemoryCache()
            .AddApplicationInsightsTelemetry()
            .AddAzureServiceBusServices();

        builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        var app = builder.Build();

        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseMiddleware<ErrorHandlerMiddleware>();
        app.UseMiddleware<JwtMiddleware>();

        app.MapControllers();

        if(app.Environment.IsDevelopment())
            DbInitializer.EnsurePopulated(app);

        app.Run();
    }
}
