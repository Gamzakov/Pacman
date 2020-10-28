using Pacman.GameObjects;
using Pacman.Models.Maps.Interfaces;

using System;
using System.Windows;
using System.Windows.Threading;

namespace Pacman.SubSystems.Renderers
{
    internal class GameBoard : System.Windows.Controls.Canvas, IRenderer
    {
        public IMap Map { get; }

        public GameBoard(IMap map)
        {
            Map = map;

            _timer = new DispatcherTimer(DispatcherPriority.Render)
            {
                Interval = TimeSpan.FromMilliseconds(18),
            };
            _timer.Tick += Draw;
            _timer.Start();
        }

        private void Draw(object sender, EventArgs e)
        {
            Children.Clear();

            foreach (GameObjects.Objects.IObject gameobject in Map.GameObjects)
            {
                Draw(gameobject.Image, gameobject.Position, gameobject.Size);
            }

            foreach (GameObjects.Enemys.Interfaces.IEnemy enemies in Map.Enemies)
            {
                Draw(enemies.Image, enemies.Position, enemies.Size);
            }

            foreach (GameObjects.Objects.IPickableObject pickableObject in Map.PickableObjects)
            {
                Draw(pickableObject.Image, pickableObject.Position, pickableObject.Size);
            }

            if (Map.Player != null)
                Draw(Map.Player.Image, Map.Player.Position, Map.Player.Size);
        }

        private void Draw(UIElement element, Position position, GameObjects.Size size)
        {
            SetLeft(element, (int)(position.X - (size.Width / 2)));
            SetTop(element, (int)(position.Y - (size.Height / 2)));
            Children.Add(element);
        }

        private DispatcherTimer _timer;
    }
}