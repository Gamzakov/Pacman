using System.ComponentModel;

namespace Pacman.Interfaces
{
    interface IViewModel : INotifyPropertyChanged
    {
        string Title { get; set; }
    }
}
