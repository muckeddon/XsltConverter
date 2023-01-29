using Microsoft.Win32;
using System.Windows.Input;
using WPFProject.Infrastructure.Commands;

namespace WPFProject.ViewModels.Base
{
    /// <summary>
    /// Представляет модель представления выбора пути для открытия/сохранения файла.
    /// </summary>
    internal class PathViewModel : BaseViewModel
    {
        #region Поля и свойства

        private readonly PathType _pathType;

        private string? _path;

        /// <summary>
        /// Задает или возвращает путь к файлу.
        /// </summary>
        public string? Path
        {
            get => _path;
            set => Set(ref _path, value);
        }

        #region Команды

        /// <summary>
        /// Возвращает команду по выбору пути.
        /// </summary>
        public ICommand ChoosePathCommand { get; }

        private bool CanChoosePathCommandExecute(object parameter) => true;

        private void OnChoosePathCommandExecuted(object parameter)
        {
            if (_pathType == PathType.PathForOpen)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "xml files (*.xml)|*.xml";

                if (openFileDialog.ShowDialog() == true)
                    Path = openFileDialog.FileName;
            }

            if (_pathType == PathType.PathForSave)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "xml files (*.xml)|*.xml";

                if (saveFileDialog.ShowDialog() == true)
                    Path = saveFileDialog.FileName;
            }
        }

        #endregion Команды

        #endregion Поля и свойства

        #region Конструктор

        /// <summary>
        /// Инициализирует модель представления <see cref="PathViewModel"/>.
        /// </summary>
        /// <param name="pathType"> Тип пути. </param>
        public PathViewModel(PathType pathType)
        {
            _pathType = pathType;

            ChoosePathCommand = new RelayCommand(OnChoosePathCommandExecuted, CanChoosePathCommandExecute);
        }

        #endregion Конструктор
    }
}
