namespace Dns.Api.Tests.Infrastructure
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Api.Infrastructure;
    using Api.Infrastructure.Configuration;
    using Api.Infrastructure.Exceptions;
    using Autofac;
    using Autofac.Extensions.DependencyInjection;
    using Be.Vlaanderen.Basisregisters.Api;
    using Be.Vlaanderen.Basisregisters.Api.Exceptions;
    using Domain.Exceptions;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.ApiExplorer;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Swashbuckle.AspNetCore.Swagger;

    public class TestStartup
    {
        private IContainer _applicationContainer;

        private readonly IConfiguration _configuration;

        public TestStartup(
            IConfiguration configuration) =>
            _configuration = configuration;

        /// <summary>Configures services for the application.</summary>
        /// <param name="services">The collection of services to configure the application with.</param>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services
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
                    new[]
                    {
                        typeof(Startup).GetTypeInfo().Assembly.GetName().Name,
                    },
                    _configuration.GetSection("Cors").GetChildren().Select(c => c.Value).ToArray());

            var containerBuilder = new ContainerBuilder();
            containerBuilder.Populate(services);
            _applicationContainer = containerBuilder.Build();

            return new AutofacServiceProvider(_applicationContainer);
        }

        public void Configure(
            IServiceProvider serviceProvider,
            IApplicationBuilder app,
            IHostingEnvironment env,
            IApplicationLifetime appLifetime,
            ILoggerFactory loggerFactory,
            IApiVersionDescriptionProvider apiVersionProvider)
        {
            app.UseDefaultForApi(new StartupOptions
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
                        new Api.Infrastructure.Exceptions.ApiExceptionHandler(),
                        new AggregateNotFoundExceptionHandling(),
                        new WrongExpectedVersionExceptionHandling(),
                        new InvalidTopLevelDomainExceptionHandling(),
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
        }

        private static string GetApiLeadingText(ApiVersionDescription description)
            => $"Right now you are reading the documentation for version {description.ApiVersion} of the exira.com Dns API{string.Format(description.IsDeprecated ? ", **this API version is not supported any more**." : ".")}";
    }
}
