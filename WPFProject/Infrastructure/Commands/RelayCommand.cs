using System;
using WPFProject.Infrastructure.Commands.Base;

namespace WPFProject.Infrastructure.Commands
{
    /// <summary>
    /// <inheritdoc cref="Command"/>
    /// </summary>
    internal class RelayCommand : Command
    {
        #region Поля

        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        #endregion Поля

        #region Конструктор

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="RelayCommand"/>.
        /// </summary>
        /// <param name="execute"> Делегат Action. </param>
        /// <param name="canExecute"> Делегат Func. </param>
        /// <exception cref="ArgumentNullException"> Создает исключение при равенстве null первого параметра. </exception>
        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        #endregion Конструктор

        public override bool CanExecute(object parameter) => _canExecute?.Invoke(parameter) ?? true;

        public override void Execute(object parameter) => _execute(parameter);
    }
}
