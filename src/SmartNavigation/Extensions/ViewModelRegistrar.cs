using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Documents;
using Autofac.SmartNavigation.Base;

namespace Autofac.SmartNavigation.Extensions
{
    internal static partial class AutofacExtensions
    {
        internal static ContainerBuilder RegisterViewModels(this ContainerBuilder builder, List<Assembly> assemblies)
        {
            foreach (var assembly in assemblies)
            {
                RegisterAsm(assembly);
            }

            void RegisterAsm(Assembly assembly)
            {
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
