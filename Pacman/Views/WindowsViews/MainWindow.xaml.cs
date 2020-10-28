using System.Windows;

// For test purposes.
#if DEBUG
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Tests")]
#endif

namespace Pacman.Views.WindowsViews
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
