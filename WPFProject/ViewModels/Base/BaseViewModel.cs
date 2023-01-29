using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WPFProject.ViewModels.Base
{
    /// <summary>
    /// Представляет класс базовой модели представления.
    /// </summary>
    internal abstract class BaseViewModel : INotifyPropertyChanged
    {
        #region События

        /// <summary>
        /// Событие возникающее при смене значения свойства.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion События

        #region Методы

        /// <summary>
        /// Вызывает выполнение делегата для события.
        /// </summary>
        /// <param name="propertyName"> Наименование свойства. </param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Метод проверки идентичности объектов.
        /// </summary>
        /// <typeparam name="T"> Тип свойства. </typeparam>
        /// <param name="field"> Значение содержащееся в поле. </param>
        /// <param name="value"> Передаваемое значение. </param>
        /// <param name="propertyName"> Наименование свойства. </param>
        /// <returns> Возвращает флаг, означающий что значение изменено. </returns>
        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(field, value)) return false;

            field = value;
            OnPropertyChanged(propertyName);

            return true;
        }

        #endregion Методы

    }
}
