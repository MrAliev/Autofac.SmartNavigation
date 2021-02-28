using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using Autofac.SmartNavigation.Base;

namespace Autofac.SmartNavigation.Extensions
{
    internal static partial class AutofacExtensions
    {
        internal static ContainerBuilder RegisterViews(this ContainerBuilder builder, List<Assembly> assemblies)
        {
            foreach (var assembly in assemblies)
            {
                RegisterAsm(assembly);
            }

            void RegisterAsm(Assembly assembly)
            {
                //if(assembly.ImageRuntimeVersion == Application.Current.Properties.)

                builder.RegisterAssemblyTypes(assembly)
                    .PublicOnly()
                    .Keyed<NavigationalWindow>(t => t.Name.ToLower())
                    .AsImplementedInterfaces()
                    .AsSelf();

                builder.RegisterAssemblyTypes(assembly)
                    .PublicOnly()
                    .Keyed<Window>(t => t.Name.ToLower())
                    .AsImplementedInterfaces()
                    .AsSelf();

                builder.RegisterAssemblyTypes(assembly)
                    .PublicOnly()
                    .Keyed<Page>(t => t.Name.ToLower())
                    .AsImplementedInterfaces()
                    .AsSelf();
            }
            
            return builder;
        }
    }
}
