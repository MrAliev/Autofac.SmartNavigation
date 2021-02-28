using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Autofac.SmartNavigation.Extensions
{
    public static class AutofacExtension
    {
        /// <summary>
        /// Использовать автопоиск и регистрацию представлений и моделей представлений
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static ContainerBuilder UseAutofind(this ContainerBuilder builder)
        {
            var allFiles = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.*", SearchOption.AllDirectories)
                .Where(file => file.EndsWith(".dll") || file.EndsWith(".exe"))
                .ToList(); 
             
            var assemblies = new List<Assembly>();

            foreach (var file in allFiles)
            {
                try
                {
                    var asm = Assembly.LoadFrom(file);
                    assemblies.Add(asm);
                }
                catch (Exception e)
                {
                    Debug.WriteLine($"Не удалось загрузить сборку \"{file}\".\nОшибка: {e}");
                }
            }

            builder.RegisterViewModels(assemblies).RegisterViews(assemblies);
            return builder;
        }
    }
}
