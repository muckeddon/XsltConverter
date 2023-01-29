using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using WPFProject.Data;
using WPFProject.Infrastructure.Commands;
using WPFProject.Services;
using WPFProject.ViewModels.Base;

namespace WPFProject.ViewModels
{
    /// <summary>
    /// Представляет класс модели представления главного окна.
    /// </summary>
    internal class MainWindowViewModel : BaseViewModel, IDisposable
    {
        #region Поля и свойства

        private PathViewModel _xmlBeforeChangeViewModel;
        private PathViewModel _xmlAfterChangeViewModel;
        private ObservableCollection<Node> _xmlNodesBeforeChanging;
        private ObservableCollection<Node> _xmlNodesAfterChanging;
        private Converter _converter;
        private NodeFinder _nodeFinder;
        private AttributeAdder _attributeAdder;
        private bool _isConvertEnabled;

        /// <summary>
        /// Задает или возвращает флаг о доступности конвертации.
        /// </summary>
        public bool IsConvertEnabled {
            get => _isConvertEnabled;
            set => Set(ref _isConvertEnabled, value);
        }

        /// <summary>
        /// Задает или возвращает модель представления выбора пути файла до изменений.
        /// </summary>
        public PathViewModel XmlBeforeChangeViewModel
        {
            get => _xmlBeforeChangeViewModel;
            set => Set(ref _xmlBeforeChangeViewModel, value);
        }

        /// <summary>
        /// Задает или возвращает модель представления выбора пути файла после изменений.
        /// </summary>
        public PathViewModel XmlAfterChangeViewModel
        {
            get => _xmlAfterChangeViewModel;
            set => Set(ref _xmlAfterChangeViewModel, value);
        }

        /// <summary>
        /// Задает или возвращает коллекцию нодов, хранящую наименование ноды и количество вложенных item.
        /// Коллекция хранит ноды из изначального файла.
        /// </summary>
        public ObservableCollection<Node> XmlNodesBeforeChanging
        {
            get => _xmlNodesBeforeChanging;
            set => Set(ref _xmlNodesBeforeChanging, value);
        }

        /// <summary>
        /// Задает или возвращает коллекцию нодов, хранящую наименование ноды и количество вложенных item.
        /// Коллекция хранит ноды из созданного файла.
        /// </summary>
        public ObservableCollection<Node> XmlNodesAfterChanging
        {
            get => _xmlNodesAfterChanging;
            set => Set(ref _xmlNodesAfterChanging, value);
        }

        #region Команды

        public ICommand ConvertCommand { get; }

        private bool CanConvertCommandExecute(object parameter) => ArePathEmpty();

        private void OnConvertCommandExecuted(object parameter)
        {
            XmlNodesBeforeChanging.Clear();
            XmlNodesAfterChanging.Clear();

            _converter.Convert(_xmlBeforeChangeViewModel.Path, _xmlAfterChangeViewModel.Path);
            _attributeAdder.AddAttributeWithNumberOfElements(_xmlBeforeChangeViewModel.Path);
            _attributeAdder.AddAttributeWithNumberOfElements(_xmlAfterChangeViewModel.Path);

            _nodeFinder.SetItems(_xmlBeforeChangeViewModel.Path, _xmlNodesBeforeChanging);
            _nodeFinder.SetItems(_xmlAfterChangeViewModel.Path, _xmlNodesAfterChanging);

            MessageBox.Show("Преобразование выполнено");
        }

        #endregion Команды

        #endregion Поля и свойства

        #region Конструктор 

        /// <summary>
        /// Инициализирует экземпляр класса <see cref="MainWindowViewModel"/>.
        /// </summary>
        public MainWindowViewModel()
        {
            _xmlBeforeChangeViewModel = new PathViewModel(PathType.PathForOpen);
            _xmlAfterChangeViewModel = new PathViewModel(PathType.PathForSave);

            _xmlNodesBeforeChanging = new ObservableCollection<Node>();
            _xmlNodesAfterChanging = new ObservableCollection<Node>();

            _converter = new Converter();
            _attributeAdder = new AttributeAdder();
            _nodeFinder = new NodeFinder();

            IsConvertEnabled = false;

            ConvertCommand = new RelayCommand(OnConvertCommandExecuted, CanConvertCommandExecute);

            _xmlAfterChangeViewModel.PropertyChanged += IsXmlPropertyChanged;
            _xmlBeforeChangeViewModel.PropertyChanged += IsXmlPropertyChanged;
        }

        #endregion Конструктор

        #region Методы

        private bool ArePathEmpty()
        {
            if (string.IsNullOrEmpty(_xmlAfterChangeViewModel.Path) || string.IsNullOrEmpty(_xmlBeforeChangeViewModel.Path))
                return false;

            return true;
        }

        /// <summary>
        /// Возврщает флаг об изменении свойств вложенных моделей представления.
        /// </summary>
        /// <param name="sender"> Объект в котором изменилось свойство. </param>
        /// <param name="e"> Аргументы. </param>
        public void IsXmlPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Path")
                IsConvertEnabled = ArePathEmpty();
        }

        public void Dispose()
        {
            _xmlAfterChangeViewModel.PropertyChanged -= IsXmlPropertyChanged;
            _xmlBeforeChangeViewModel.PropertyChanged -= IsXmlPropertyChanged;
        }

        #endregion Методы
    }
}
