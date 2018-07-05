using AdminUWP.BL;
using AdminUWP.Interfaces;
using Autofac;
using Autofac.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AdminUWP.Infrastructure
{
    public static class DependencyResolver
    {
        private static ILifetimeScope _rootScope;

        public static void Start()
        {

            var builder = new ContainerBuilder();


            builder.RegisterInstance(new DataService())
                   .As<IDataService>().SingleInstance();

            _rootScope = builder.Build();

        }

        public static void AddDependecy<T>(Object obj)
        {
            var builder = new ContainerBuilder();


            builder.RegisterInstance(obj)
                   .As<T>().SingleInstance();

            builder.Update (_rootScope.ComponentRegistry);
        }

        public static T Resolve<T>()
        {
            if (_rootScope == null)
            {
                throw new Exception("Bootstrapper hasn't been started!");
            }

            return _rootScope.Resolve<T>(new Parameter[0]);
        }

        public static T Resolve<T>(Parameter[] parameters)
        {
            if (_rootScope == null)
            {
                throw new Exception("Bootstrapper hasn't been started!");
            }

            return _rootScope.Resolve<T>(parameters);
        }

    }
}
