namespace Pacman.ViewModels.UserControlsViewModels
{
    internal class MainMenuViewModel : BaseViewModel
    {
        public override string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        public MainMenuViewModel()
        {
        }

        private string _title = "Main menu";
    }
}