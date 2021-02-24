using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using Autofac.SmartNavigation.Base;

namespace Autofac.SmartNavigation.Extensions
{
    internal static partial class AutofacExtensions
    {
        internal static ContainerBuilder RegisterViewModels(this ContainerBuilder builder)
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
                //Log.Logger.Debug($"Анализ сборки \"{assembly.FullName}\"");
                builder.RegisterAssemblyTypes(assembly)
                    .PublicOnly()
                    .Keyed<INotifyPropertyChanged>(t => t.Name.ToLower())
                    .Named<INotifyPropertyChanged>(t => t.Name.ToLower().Replace("viewmodel", ""))
                    .AsSelf();
            }

            return builder;
        }
    }
}
