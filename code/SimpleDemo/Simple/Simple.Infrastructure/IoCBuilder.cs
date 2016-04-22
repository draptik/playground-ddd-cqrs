using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using MassTransit;
using Simple.Infrastructure.Modules;

namespace Simple.Infrastructure
{
    public class IoCBuilder
    {
        public static IContainer Container { get; private set; }

        public static void InitWeb(Assembly assembly, HttpConfiguration configuration)
        {
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(assembly).InstancePerDependency();

            // Required for Web API! See http://stackoverflow.com/a/24141348/1062607
            builder.RegisterAssemblyTypes(assembly).PropertiesAutowired();

            // Module registration
            builder.RegisterModule<EventStoreModule>();
            builder.RegisterModule<ServiceModule>();
            builder.RegisterModule<BusModule>();
            builder.RegisterModule<RepositoryModule>();
            builder.RegisterModule<ReadModelModule>();
            builder.RegisterModule<SnapshotModule>();

            Container = builder.Build();

            // Start service bus
            var busControl = Container.Resolve<IBusControl>();
            busControl.Start();

            // Set the dependency resolver for Web API.
            var webApiResolver = new AutofacWebApiDependencyResolver(Container);
            configuration.DependencyResolver = webApiResolver;
        }
    }
}