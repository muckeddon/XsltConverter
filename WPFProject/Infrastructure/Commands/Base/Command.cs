using System;
using System.Windows.Input;

namespace WPFProject.Infrastructure.Commands.Base
{
    /// <summary>
    /// Представляет базовый класс команды.
    /// </summary>
    internal abstract class Command : ICommand
    {
        #region События

        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        #endregion События

        #region Методы

        /// <summary>
        /// Определяет, может ли команда выполняться в текущем состоянии.
        /// </summary>
        /// <param name="parameter"> Данные используемые командой. </param>
        /// <returns> Флаг, означающий возможность выболнения команды </returns>
        public abstract bool CanExecute(object parameter);

        /// <summary>
        /// Определяет метод, вызываемый при вызове команды.
        /// </summary>
        /// <param name="parameter"> Данные используемые командой. </param>
        public abstract void Execute(object parameter);

        #endregion Методы
    }
}
