﻿using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;

namespace ESDemo.Infrastructure
{
    public class IoCBuilder
    {
        private static IContainer _container;

        public static IContainer Container => _container;

        public static void InitWeb(Assembly assembly, HttpConfiguration configuration)
        {
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(assembly).InstancePerDependency();

            // Required for Web API! See http://stackoverflow.com/a/24141348/1062607
            builder.RegisterAssemblyTypes(assembly).PropertiesAutowired();

            // Module registration
            //builder.RegisterModule<SomeModule>();


            _container = builder.Build();

            // Set the dependency resolver for Web API.
            var webApiResolver = new AutofacWebApiDependencyResolver(_container);
            configuration.DependencyResolver = webApiResolver;
        }
    }
}