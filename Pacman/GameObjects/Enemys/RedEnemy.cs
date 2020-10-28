using Pacman.GameObjects.Enemys.Interfaces;
using Pacman.Tools;

using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Pacman.GameObjects.Enemys
{
    internal class RedEnemy : IEnemy
    {
        public Position Position => _position;
        public IEnumerable<Direction> AvailableDirections { get; set; }
        public Direction LookingAt { get; private set; }
        public Image Image { get; private set; }
        public Size Size { get; private set; }
        public float SpeedMultiplier { get; }
        public IStrategy Strategy { get; }
        public float BaseSpeed { get; }

        public RedEnemy(Position position, IStrategy strategy, float baseSpeed = 100.0f, float speedMultiplier = 0.15f)
        {
            _images = new Dictionary<Direction, BitmapImage>
            {
                { Direction.Left, Properties.Resources.RedLeft.ConvertToBitmapImage() },
                { Direction.Up, Properties.Resources.RedUp.ConvertToBitmapImage() },
                { Direction.Right, Properties.Resources.RedRight.ConvertToBitmapImage() },
                { Direction.Down, Properties.Resources.RedDown.ConvertToBitmapImage() }
            };

            _width = (int)_images[Direction.Up].Width;
            _height = (int)_images[Direction.Up].Height;

            _position = position;
            Size = new Size(_position, _width, _height);

            BaseSpeed = baseSpeed;
            SpeedMultiplier = speedMultiplier;

            Image = new Image { Source = _images[Direction.Up] };

            Strategy = strategy;
        }

        public void Move(Direction direction)
        {
            if (AvailableDirections.ToList().Count == 0)
                return;

            LookingAt = direction;
            var actualSpeed = BaseSpeed * SpeedMultiplier;

            if (_images.TryGetValue(LookingAt, out BitmapImage image))
                Image.Source = image;

            switch (LookingAt)
            {
                case Direction.Left:
                    // Strange behavior this is a fix
                    var xPos = _position.X;
                    var newXPosition = xPos - actualSpeed;
                    _position.X = newXPosition;
                    break;
                case Direction.Up:
                    var yPos = _position.Y;
                    var newYPosition = yPos - actualSpeed;
                    _position.Y = newYPosition;
                    break;
                case Direction.Right:
                    _position.X += BaseSpeed * SpeedMultiplier;
                    break;
                case Direction.Down:
                    _position.Y += BaseSpeed * SpeedMultiplier;
                    break;
                case Direction.None:
                    break;
            }

            Size = new Size(_position, _width, _height);
        }

        private readonly Dictionary<Direction, BitmapImage> _images;
        private readonly int _height;
        private readonly int _width;
        private Position _position;
    }
}