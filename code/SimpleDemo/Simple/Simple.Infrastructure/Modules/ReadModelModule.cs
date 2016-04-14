using Autofac;
using Simple.Readmodels;

namespace Simple.Infrastructure.Modules
{
    public class ReadModelModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof (CustomerReadModel).Assembly).AsImplementedInterfaces();
        }
    }
}