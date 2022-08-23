using MassTransit;
using Serilog.Enrichers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;
using LT.DigitalOffice.Kernel.Extensions;
using LT.DigitalOffice.Kernel.Configurations;
using LT.DigitalOffice.Kernel.EFSupport.Helpers;
using LT.DigitalOffice.Kernel.EFSupport.Extensions;
using LT.DigitalOffice.Kernel.Middlewares.ApiInformation;
using LT.DigitalOffice.Kernel.BrokerSupport.Helpers;
using LT.DigitalOffice.Kernel.BrokerSupport.Extensions;
using LT.DigitalOffice.Kernel.BrokerSupport.Configurations;
using LT.DigitalOffice.Kernel.BrokerSupport.Middlewares.Token;
using LT.DigitalOffice.FamilyService.Models.Dto.Configurations;
using LT.DigitalOffice.FamilyService.Data.Provider.MsSql.Ef;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace LT.DigitalOffice.FamilyService
{
    public class Startup : BaseApiInfo
    {
        public const string CorsPolicyName = "LtDoCorsPolicy";

        private readonly BaseServiceInfoConfig _serviceInfoConfig;
        private readonly RabbitMqConfig _rabbitMqConfig;
        
        public IConfiguration Configuration { get; }

        #region private methods

        private void ConfigureMassTransit(IServiceCollection services)
        {
            (string username, string password) = RabbitMqCredentialsHelper.Get(_rabbitMqConfig, _serviceInfoConfig);

            services.AddMassTransit(x=>
            {
                //x.AddConsumer<>();
                //x.AddConsumer<>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(_rabbitMqConfig.Host, "/", host =>
                    {
                        host.Username(username);
                        host.Password(password);
                    });

                   // cfg.ReceiveEndpoint();
                });

                x.AddRequestClients(_rabbitMqConfig);
            });

            services.AddMassTransitHostedService();
        }

        #endregion
        
        #region public methods

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            _serviceInfoConfig = Configuration
                .GetSection(BaseServiceInfoConfig.SectionName)
                .Get<BaseServiceInfoConfig>();
            
            _rabbitMqConfig = Configuration
                .GetSection(BaseServiceInfoConfig.SectionName)
                .Get<RabbitMqConfig>();

            Version = "1.0.0.0";
            Description = "FamilyService is an API intended to work with an information about workers' families.";
            StartTime = DateTime.UtcNow;
            ApiName = $"LT Digital Office - {_serviceInfoConfig.Name}";
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(
                    CorsPolicyName,
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                    });
            });

            services.AddHttpContextAccessor();

            services.Configure<TokenConfiguration>(Configuration.GetSection("CheckTokenMiddleware"));
            services.Configure<BaseServiceInfoConfig>(Configuration.GetSection(BaseServiceInfoConfig.SectionName));
            services.Configure<BaseRabbitMqConfig>(Configuration.GetSection(BaseRabbitMqConfig.SectionName));

            string connStr = ConnectionStringHandler.Get(Configuration);

            services.AddDbContext<FamilyServiceDbContext>(options =>
            {
                options.UseSqlServer(connStr);
            });

            services.AddBusinessObjects();

            services
                .AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                })
                .AddNewtonsoftJson();

            services.AddHealthChecks()
                .AddSqlServer(connStr)
                .AddRabbitMqCheck();

            ConfigureMassTransit(services);            
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            app.UpdateDatabase<FamilyServiceDbContext>();

            app.UseForwardedHeaders();

            app.UseExceptionsHandler(loggerFactory);

            app.UseApiInformation();

            app.UseRouting();

            app.UseMiddleware<TokenMiddleware>();

            app.UseCors(CorsPolicyName);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers().RequireCors(CorsPolicyName);

                endpoints.MapHealthChecks($"/{_serviceInfoConfig}/hc", new HealthCheckOptions
                {
                    ResultStatusCodes = new Dictionary<HealthStatus, int>
                    {
                        { HealthStatus.Unhealthy, 200 },
                        { HealthStatus.Healthy, 200 },
                        { HealthStatus.Degraded, 200 },
                    },
                    Predicate = check => check.Name != "masstransit-bus",
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
            });
        }
        #endregion
    }
}
