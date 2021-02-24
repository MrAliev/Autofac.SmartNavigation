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
        internal static ContainerBuilder RegisterViews(this ContainerBuilder builder)
        {
            var allFiles = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.*", SearchOption.AllDirectories).ToList();

            var assemblies = allFiles.Where(f => f.EndsWith(".dll") || f.EndsWith(".exe"))
                .Select(Assembly.LoadFile).ToList();

            foreach (var assembly in assemblies)
            {
                RegisterAsm(assembly);
            }

            void RegisterAsm(Assembly assembly)
            {
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
