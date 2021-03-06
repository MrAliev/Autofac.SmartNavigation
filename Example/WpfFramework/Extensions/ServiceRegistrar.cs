﻿using Autofac;
using Autofac.SmartNavigation;
using Autofac.SmartNavigation.Interfaces;
using WpfFramework.Services;

namespace WpfFramework.Extensions
{
    public static class AutofacExtensions
    {
        public static ContainerBuilder RegisterServices(this ContainerBuilder builder)
        {
            builder.RegisterType<AppNavigationService>().As<INavigationService>().SingleInstance().AsSelf();
            builder.RegisterType<DataProvider>().AsImplementedInterfaces().SingleInstance().AsSelf();

            return builder;
        }
    }
}
