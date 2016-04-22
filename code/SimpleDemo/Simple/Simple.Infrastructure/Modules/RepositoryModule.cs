using Autofac;
using Simple.Repositories;

namespace Simple.Infrastructure.Modules
{
    public class RepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(CustomerRepository).Assembly).AsImplementedInterfaces();
        }
    }
}