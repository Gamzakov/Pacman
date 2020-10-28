using Pacman.Interfaces;
using Pacman.Tools;
using Pacman.Views.UserControlsViews;

using System;
using System.Reflection;
using System.Windows.Controls;

namespace Pacman.ViewModels.WindowsViewModels
{
#pragma warning disable CA1812 // Avoid uninstantiated internal classes
    internal class MainWindowViewModel : BaseViewModel
#pragma warning restore CA1812 // Avoid uninstantiated internal classes
    {
        #region Public Members

        #region Properties

        /// <summary>
        /// Title of current <see cref="Context"/> is using for naming a <see cref="MainWindow"/>
        /// </summary>
        public sealed override string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Current context, which user see
        /// </summary>
        public object Context
        {
            get => _context;
            private set
            {
                if (_context == value)
                    return;

                _context = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Commands
        /// <summary>
        /// Command for switching between <see cref="Context"/>
        /// </summary>
        public RelayCommand SwitchViewCommand => new RelayCommand(SwitchView);
        public RelayCommand ExitCommand => new RelayCommand(Exit);

        #endregion

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public MainWindowViewModel()
        {
            SwitchView(new MainMenuView());
        }

        #endregion

        private void SwitchView(object obj)
        {
            Type type = typeof(UserControl);
            Type objectType = obj.GetType().BaseType;
            Type objectRuntimeType = (obj as Type)?.BaseType;

            if (objectType == type)
            {
                Context = obj;

                PropertyInfo propertyInfo = obj.GetType().GetProperty("DataContext");
                var value = propertyInfo?.GetValue(obj);

                if (value is IViewModel viewModel)
                {
                    Title = viewModel.Title;
                }
            }

            if (objectRuntimeType == type)
            {
                var instance = Activator.CreateInstance(obj as Type) as UserControl;
                Context = instance;

                var viewModel = instance.DataContext as IViewModel;
                Title = viewModel?.Title;
            }
        }

        private void Exit(object obj) => System.Windows.Application.Current.Shutdown(0);

        #region Private Members

        private object _context;
        private string _title;

        #endregion
    }
}