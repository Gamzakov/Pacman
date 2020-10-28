using Pacman.GameObjects;
using Pacman.GameObjects.Enemys;
using Pacman.GameObjects.Enemys.Interfaces;
using Pacman.GameObjects.Enemys.Strategies;
using Pacman.GameObjects.Interfaces;
using Pacman.GameObjects.Objects;
using Pacman.GameObjects.Players;
using Pacman.Models.Maps.Interfaces;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Input;
using System.Windows.Threading;

namespace Pacman.Models.Maps
{
    internal class ClassicMap : IMap
    {
        public IEnumerable<IPickableObject> PickableObjects { get; }
        public GameStates GameState { get; private set; }
        public IEnumerable<IObject> GameObjects { get; }
        public IEnumerable<IEnemy> Enemies { get; }
        public IPlayer Player { get; private set; }

        public Action OnDataUpdated { get; }

        public float Height { get; set; } = 768;
        public float Width { get; set; } = 1024;

        public ClassicMap(Action onDataUpdated = null)
        {
            OnDataUpdated = onDataUpdated;

            _timer = new DispatcherTimer(new TimeSpan(0, 0, 0, 0, 400), DispatcherPriority.Render, Update, Dispatcher.CurrentDispatcher);

            _gameObjects = new List<IObject>();
            GameObjects = _gameObjects;

            _enemies = new List<IEnemy>();
            Enemies = _enemies;

            _pickableObjects = new List<IPickableObject>();
            PickableObjects = _pickableObjects;
        }

        public void StartGame()
        {
            GameState = GameStates.Paused;

            if (_timer.IsEnabled)
                _timer.Stop();

            _enemiesLoaded = false;
            _gameObjects.Clear();
            _enemies.Clear();
            Player = null;

            LoadMap();
            _timer.Start();

            GameState = GameStates.Running;

            OnDataUpdated();
        }

        public void StopGame()
        {
            _timer.Stop();
            GameState = GameStates.Stoped;
        }

        // Updates enemies positions
        private void Update(object _, EventArgs __)
        {
            if (!_enemiesLoaded)
            {
                LoadEnemies();
                _enemiesLoaded = true;
            }

            foreach (IEnemy enemy in Enemies)
            {
                var directions = new List<Direction>();

                if (CanMove(enemy, Direction.Left))
                    directions.Add(Direction.Left);
                if (CanMove(enemy, Direction.Up))
                    directions.Add(Direction.Up);
                if (CanMove(enemy, Direction.Right))
                    directions.Add(Direction.Right);
                if (CanMove(enemy, Direction.Down))
                    directions.Add(Direction.Down);

                enemy.AvailableDirections = directions;

                enemy.Strategy.NextMove(enemy, enemy.Move, Player.Position);

                if (HitTest(Player, enemy))
                    break;
            }
        }

        private void LoadMap()
        {
            var textMap = Properties.Resources.SimplePacmanMap.Replace("\n", "").Split('\r');

            for (var lineIndex = 0; lineIndex < textMap.Length; lineIndex++)
            {
                var line = textMap[lineIndex];
                for (var charIndex = 0; charIndex < line.Length; charIndex++)
                {
                    var ch = line[charIndex];
                    var x = charIndex * 32;
                    var y = lineIndex * 32;

                    switch (ch)
                    {
                        case '#':
                            _gameObjects.Add(new Wall(new Position() { X = x, Y = y }));
                            break;
                        case 'P':
                            if (Player != null)
                                throw new Exception("Only one player will be on the map.");

                            Player = new Player(new Position() { X = x, Y = y }, _speed, _speedMultiplier);
                            break;
                        case 'F':
                            _pickableObjects.Add(new Food(new Position() { X = x, Y = y }));
                            break;
                    }
                }
            }
        }

        private void LoadEnemies()
        {
            var textMap = Properties.Resources.SimplePacmanMap.Replace("\n", "").Split('\r');

            for (var lineIndex = 0; lineIndex < textMap.Length; lineIndex++)
            {
                var line = textMap[lineIndex];
                for (var charIndex = 0; charIndex < line.Length; charIndex++)
                {
                    var ch = line[charIndex];
                    var x = charIndex * 32;
                    var y = lineIndex * 32;

                    switch (ch)
                    {
                        case 'R':
                            var redEnemy = new RedEnemy(new Position() { X = x, Y = y }, new EasyStrategy(), _speed, _speedMultiplier);
                            _enemies.Add(redEnemy);
                            break;
                        case 'B':
                            var blueEnemy = new BlueEnemy(new Position() { X = x, Y = y }, new EasyStrategy(), _speed, _speedMultiplier);
                            _enemies.Add(blueEnemy);
                            break;
                    }
                }
            }
        }

        // Handles user input
        public void OnKeyDown(object sender, KeyEventArgs keyEventArgs)
        {
            Key key = keyEventArgs.Key;

            switch (key)
            {
                case Key.W:
                    if (CanMove(Player, Direction.Up))
                        Player.Move(Direction.Up);
                    break;
                case Key.A:
                    if (CanMove(Player, Direction.Left))
                        Player.Move(Direction.Left);
                    break;
                case Key.D:
                    if (CanMove(Player, Direction.Right))
                        Player.Move(Direction.Right);
                    break;
                case Key.S:
                    if (CanMove(Player, Direction.Down))
                        Player.Move(Direction.Down);
                    break;
            }

            PickableTest(Player);
            Debug.WriteLine($"Object name: {Player.GetType().Name}, X: {Player.Position.X}, Y: {Player.Position.Y}");
        }

        private bool CanMove(IMovableGameObject movableGameObject, Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    var yUpConstraint = movableGameObject.Position.Y - movableGameObject.Size.Height / 2;

                    var wallsUpperThen = _gameObjects.FindAll(
                        obj => (int)movableGameObject.Position.Y - 32 == (int)obj.Position.Y && (int)movableGameObject.Position.X == (int)obj.Position.X).Count;

                    return yUpConstraint >= 0 && wallsUpperThen == 0;

                case Direction.Left:
                    var xLConstraint = movableGameObject.Position.X - movableGameObject.Size.Width / 2;

                    var wallsLeftThen = _gameObjects.FindAll(
                        obj => (int)movableGameObject.Position.X - 32 == (int)obj.Position.X && (int)movableGameObject.Position.Y == (int)obj.Position.Y).Count;

                    return xLConstraint >= 0 && wallsLeftThen == 0;

                case Direction.Right:
                    var xRConstraint = movableGameObject.Position.X + movableGameObject.Size.Width / 2;

                    var wallsRightThen = _gameObjects.FindAll(
                        obj => (int)movableGameObject.Position.X + 32 == (int)obj.Position.X && (int)movableGameObject.Position.Y == (int)obj.Position.Y).Count; 

                    return xRConstraint <= Width && wallsRightThen == 0;

                case Direction.Down:
                    var yDownConstraint = movableGameObject.Position.Y + movableGameObject.Size.Height / 2;

                    var wallsLowerThen = _gameObjects.FindAll(
                        obj => (int)movableGameObject.Position.Y + 32 == (int)obj.Position.Y && (int)movableGameObject.Position.X == (int)obj.Position.X).Count;

                    return yDownConstraint <= Height && wallsLowerThen == 0;

                default:
                    return false;
            }
        }

        private bool HitTest(IPlayer player, IEnemy enemy)
        {
            if (player.Position == enemy.Position)
            {
                Debug.WriteLine("Player hitted!");
                if (!Player.Hit())
                {
                    StartGame();

                    OnDataUpdated();
                    return true;
                }
            }

            OnDataUpdated();
            return false;
        }

        private void PickableTest(IPlayer player)
        {
            foreach (IPickableObject pickableObject in _pickableObjects)
            {
                if (player.Position == pickableObject.Position)
                    player.PickUp(pickableObject);
            }

            _pickableObjects.RemoveAll(obj => obj.IsDestroyed);
            OnDataUpdated();
        }

        private bool _enemiesLoaded = false;

        private readonly List<IPickableObject> _pickableObjects;
        private readonly List<IObject> _gameObjects;
        private readonly List<IEnemy> _enemies;
        private DispatcherTimer _timer;

        private const float _speedMultiplier = 10.0f;
        private const float _speed = 3.2f;
    }
}