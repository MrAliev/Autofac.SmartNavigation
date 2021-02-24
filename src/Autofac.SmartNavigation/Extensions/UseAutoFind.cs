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
            builder.RegisterViewModels().RegisterViews();
            return builder;
        }
    }
}
