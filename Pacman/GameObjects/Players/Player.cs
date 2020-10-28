using Pacman.GameObjects.Objects;
using Pacman.Tools;

using System.Collections.Generic;
using System.Windows.Controls;

namespace Pacman.GameObjects.Players
{
    internal class Player : IPlayer
    {
        public byte Health { get; private set; }
        public bool IsAlive => Health != 0;
        public Direction LookingAt { get; private set; }
        public Position Position => _position;
        public int Score { get; private set; }
        public Size Size { get; private set; }
        public float SpeedMultiplier { get; }
        public float BaseSpeed { get; }
        public Image Image { get; }

        public Player(Position position, float baseSpeed = 100.0f, float speedMultiplier = 0.15f)
        {
            _images = new Dictionary<Direction, System.Windows.Media.Imaging.BitmapImage>
            {
                { Direction.Left, Properties.Resources.LeftPacman.ConvertToBitmapImage() },
                { Direction.Up, Properties.Resources.UpPacman.ConvertToBitmapImage() },
                { Direction.Right, Properties.Resources.RightPacman.ConvertToBitmapImage() },
                { Direction.Down, Properties.Resources.DownPacman.ConvertToBitmapImage() }
            };

            Health = 3;

            _position = position;
            LookingAt = Direction.Right;
            SpeedMultiplier = speedMultiplier;
            BaseSpeed = baseSpeed;
            Image = new Image
            {
                Source = _images[Direction.Right]
            };

            _height = (int)_images[Direction.Right].Height;
            _width = (int)_images[Direction.Right].Width;

            Size = new Size(_position, _width, _height);
        }

        public void Move(Direction direction)
        {
            LookingAt = direction;
            var actualSpeed = BaseSpeed * SpeedMultiplier;

            if (_images.TryGetValue(LookingAt, out System.Windows.Media.Imaging.BitmapImage image))
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

        public bool Hit()
        {
            Health -= 1;

            return IsAlive;
        }

        public void PickUp(IPickableObject obj)
        {
            Score += obj.Reward;
            obj.Destroy();
        }

        private readonly Dictionary<Direction, System.Windows.Media.Imaging.BitmapImage> _images;
        private readonly int _height;
        private readonly int _width;
        private Position _position;
    }
}