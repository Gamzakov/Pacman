using Pacman.Tools;
using System.Windows.Controls;

namespace Pacman.GameObjects.Objects
{
    internal class Wall : IObject
    {
        public Position Position { get; }
        public Image Image { get; }
        public Size Size { get; }

        public Wall(Position position)
        {
            Position = position;

            System.Windows.Media.Imaging.BitmapImage image = Properties.Resources.WallBlock.ConvertToBitmapImage();
            _width = (int)image.Width;
            _height = (int)image.Height;

            Image = new Image { Source = image };

            Size = new Size(Position, _width, _height);
        }

        private readonly int _width;
        private readonly int _height;
    }
}