using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Autofac.SmartNavigation.Base
{
    /// <summary>
    /// Базовый класс вью-моделей
    /// </summary>
    public abstract class BaseVM : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        /// <summary>
        /// Устанавливает новое значение в случае его отличия от текущего, с вызовом INPC
        /// </summary>
        /// <typeparam name="T">Тип значения</typeparam>
        /// <param name="field">переменная свойства</param>
        /// <param name="value">новое значение</param>
        /// <param name="propertyName">Имя свойства</param>
        /// <returns>результат операции</returns>
        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
