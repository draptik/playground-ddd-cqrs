using Autofac;
using Simple.Contracts;
using Simple.Readmodels;

namespace Simple.Infrastructure.Modules
{
    public class ReadModelModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(UpdateCustomerReadModelRepository).Assembly).AsImplementedInterfaces();
        }
    }
}