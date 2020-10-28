using System.ComponentModel;
using System.Runtime.CompilerServices;
using Pacman.Interfaces;
using Pacman.ViewModels.UserControlsViewModels;

namespace Pacman.ViewModels
{
    internal abstract class BaseViewModel : IViewModel
    {
        public abstract string Title { get; set; }

        public static IViewModel MainMenuViewModel => new MainMenuViewModel();

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null) => 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
