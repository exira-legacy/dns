namespace Dns.Api.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using Be.Vlaanderen.Basisregisters.Api;
    using Autofac;
    using Autofac.Extensions.DependencyInjection;
    using Be.Vlaanderen.Basisregisters.Api.Exceptions;
    using Be.Vlaanderen.Basisregisters.DataDog.Tracing;
    using Be.Vlaanderen.Basisregisters.DataDog.Tracing.AspNetCore;
    using Be.Vlaanderen.Basisregisters.DataDog.Tracing.Autofac;
    using Configuration;
    using Domain.Exceptions;
    using Exceptions;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Localization;
    using Microsoft.AspNetCore.Mvc.ApiExplorer;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Localization;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Modules;
    using SqlStreamStore;
    using Swashbuckle.AspNetCore.Swagger;

    /// <summary>Represents the startup process for the application.</summary>
    public class Startup
    {
        private IContainer _applicationContainer;

        private readonly IConfiguration _configuration;
        private readonly ILoggerFactory _loggerFactory;

        public Startup(
            IConfiguration configuration,
            ILoggerFactory loggerFactory)
        {
            _configuration = configuration;
            _loggerFactory = loggerFactory;
        }

        /// <summary>Configures services for the application.</summary>
        /// <param name="services">The collection of services to configure the application with.</param>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services
                .AddLocalization(opts => opts.ResourcesPath = "Resources")  // TODO: This can go in ConfigureDefaultForApi

                .Configure<RequestLocalizationOptions>(opts =>  // TODO: This can partly go in ConfigureDefaultForApi, just pass in defaultRequestCulture and supportedCultures
                {
                    var defaultRequestCulture = new RequestCulture("en-GB");

                    var supportedCultures = new List<CultureInfo>
                    {
                        new CultureInfo("en-GB"),
                        new CultureInfo("en-US"),
                        new CultureInfo("en"),
                        new CultureInfo("fr-FR"),
                        new CultureInfo("fr"),
                    };

                    opts.DefaultRequestCulture = defaultRequestCulture;
                    opts.SupportedCultures = supportedCultures;
                    opts.SupportedUICultures = supportedCultures;

                    opts.FallBackToParentCultures = true;
                    opts.FallBackToParentUICultures = true;
                })

                .ConfigureDefaultForApi<Startup>(
                    (provider, description) => new Info
                    {
                        Version = description.ApiVersion.ToString(),
                        Title = "Dns API",
                        Description = GetApiLeadingText(description),
                        Contact = new Contact
                        {
                            Name = "exira.com",
                            Email = "info@exira.com",
                            Url = "https://exira.com"
                        }
                    },
                    new [] { typeof(Startup).GetTypeInfo().Assembly.GetName().Name, },
                    corsHeaders: _configuration.GetSection("Cors").GetChildren().Select(c => c.Value).ToArray(),
                    configureFluentValidation: fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>(),
                    configureMvcBuilder: builder => builder.AddDataAnnotationsLocalization());

            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule(new ApiModule(_configuration, services, _loggerFactory));
            _applicationContainer = containerBuilder.Build();

            return new AutofacServiceProvider(_applicationContainer);
        }

        public void Configure(
            IServiceProvider serviceProvider,
            IApplicationBuilder app,
            IHostingEnvironment env,
            IApplicationLifetime appLifetime,
            ILoggerFactory loggerFactory,
            IApiVersionDescriptionProvider apiVersionProvider,
            ApiDataDogToggle datadogToggle,
            ApiDebugDataDogToggle debugDataDogToggle,
            MsSqlStreamStore streamStore,
            IOptions<RequestLocalizationOptions> requestLocalizationOptions,
            IStringLocalizer<SharedResources> sharedStringLocalizer)
        {
            //StartupHelpers.EnsureSqlStreamStoreSchema<Startup>(streamStore, loggerFactory);

            if (datadogToggle.FeatureEnabled)
            {
                if (debugDataDogToggle.FeatureEnabled)
                    StartupHelpers.SetupSourceListener(serviceProvider.GetRequiredService<TraceSource>());

                app.UseDataDogTracing(
                    serviceProvider.GetRequiredService<TraceSource>(),
                    _configuration["DataDog:ServiceName"],
                    pathToCheck => pathToCheck != "/");
            }

            app
                .UseRequestLocalization(requestLocalizationOptions.Value) // TODO: This can go in UseDefaultForApi

                .UseDefaultForApi(new StartupOptions
                {
                    ApplicationContainer = _applicationContainer,
                    ServiceProvider = serviceProvider,
                    HostingEnvironment = env,
                    ApplicationLifetime = appLifetime,
                    LoggerFactory = loggerFactory,
                    Api =
                    {
                        VersionProvider = apiVersionProvider,
                        Info = groupName => $"exira.com - Dns API {groupName}",
                        CustomExceptionHandlers = new IExceptionHandler[]
                        {
                            new DomainExceptionHandler(),
                            new Exceptions.ApiExceptionHandler(),
                            new AggregateNotFoundExceptionHandling(),
                            new WrongExpectedVersionExceptionHandling(),
                            new InvalidTopLevelDomainExceptionHandling(),
                            new InvalidRecordTypeExceptionHandling(),
                            new InvalidServiceTypeExceptionHandling(),
                            new ValidationExceptionHandling(),
                        }
                    },
                    Server =
                    {
                        PoweredByName = "exira.com - exira.com",
                        ServerName = "exira.com"
                    },
                    MiddlewareHooks =
                    {
                        AfterMiddleware = x => x.UseMiddleware<AddNoCacheHeadersMiddleware>(),
                    },
                });

            GlobalStringLocalizer.Instance = sharedStringLocalizer; // TODO: This can go in UseDefaultForApi
        }

        private static string GetApiLeadingText(ApiVersionDescription description)
            => $"Right now you are reading the documentation for version {description.ApiVersion} of the exira.com Dns API{string.Format(description.IsDeprecated ? ", **this API version is not supported any more**." : ".")}";
    }
}

namespace Dns.Api
{
    using Microsoft.Extensions.Localization;

    public class GlobalStringLocalizer  // TODO: This can go in UseDefaultForApi
    {
        public static IStringLocalizer<SharedResources> Instance;
    }

    public class SharedResources {}
}
