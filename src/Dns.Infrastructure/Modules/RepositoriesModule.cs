namespace Dns.Infrastructure.Modules
{
    using Autofac;
    using Domain;
    using Repositories;

    public class RepositoriesModule : Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            containerBuilder
                .RegisterType<Domains>()
                .As<IDomains>();
        }
    }
}
