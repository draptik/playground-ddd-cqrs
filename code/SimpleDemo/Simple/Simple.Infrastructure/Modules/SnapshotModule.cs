using Autofac;
using Simple.SnapshotJob;

namespace Simple.Infrastructure.Modules
{
    public class SnapshotModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(CustomerSnapshotJob).Assembly).AsImplementedInterfaces();
        }
    }
}