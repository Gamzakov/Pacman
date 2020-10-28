using Pacman.Models.Maps;
using Pacman.Models.Maps.Interfaces;
using Pacman.SubSystems.Renderers;
using Pacman.Tools;

using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Pacman.ViewModels.UserControlsViewModels
{
    internal class GameViewModel : BaseViewModel
    {
        public override string Title { get; set; } = "Game.";
        public RelayCommand OnLoadedCommand => new RelayCommand(OnLoaded);
        public RelayCommand OnUnloadedCommand => new RelayCommand(OnUnloaded);

        public string ScoreMessage
        {
            get => _scoreMessage;
            set
            {
                _scoreMessage = _defaultScoreMessage + value;
                OnPropertyChanged();
            }
        }

        public string PlayerHealth
        {
            get => "Health: " + _playerHealth.ToString();
            set
            {
                if (byte.TryParse(value, out var val))
                {
                    _playerHealth = val;
                    OnPropertyChanged();
                }
            }
        }

        public GameViewModel()
        {
            _map = new ClassicMap(OnMapDataUpdated);
            _gameBoard = new GameBoard(_map);
        }

        private void OnMapDataUpdated()
        {
            PlayerHealth = _map.Player.Health.ToString();
            ScoreMessage = _map.Player.Score.ToString();
        }

        private void OnLoaded(object obj)
        {
            ContentPresenter cp = VisualTreeHelper.FindVisualChildren<ContentPresenter>(Application.Current.MainWindow).ToList().Last();
            Application.Current.MainWindow.KeyDown += OnKeyDown;
            cp.Content = _gameBoard;

            _gameBoard.Map.StartGame();
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            _map.OnKeyDown(sender, e);
        }

        private void OnUnloaded(object obj)
        {
            Application.Current.MainWindow.KeyDown -= _map.OnKeyDown;
        }

        private byte _playerHealth;
        private readonly IMap _map;
        private string _scoreMessage;
        private readonly GameBoard _gameBoard;
        private const string _defaultScoreMessage = "Score:";
    }
}