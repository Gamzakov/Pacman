using Pacman.ViewModels.UserControlsViewModels;
using System.Windows.Controls;

namespace Pacman.Views.UserControlsViews
{
    /// <summary>
    /// Логика взаимодействия для GameView.xaml
    /// </summary>
    public partial class GameView : UserControl
    {
        public GameView()
        {
            InitializeComponent();
        }

        private void OnLoaded(object sender, System.Windows.RoutedEventArgs e) =>
            (DataContext as GameViewModel)?.OnLoadedCommand?.Execute(e);

        private void OnUnloaded(object sender, System.Windows.RoutedEventArgs e) =>
            (DataContext as GameViewModel)?.OnUnloadedCommand?.Execute(e);
    }
}