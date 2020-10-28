using Pacman.Tools;
using System.Windows.Controls;

namespace Pacman.GameObjects.Objects
{
    internal class Food : IPickableObject
    {
        public int Reward { get; } = 1;
        public bool IsDestroyed { get; private set; }
        public Position Position { get; }
        public Image Image { get; }
        public Size Size { get; }

        public Food(Position position)
        {
            Position = position;

            System.Windows.Media.Imaging.BitmapImage image = Properties.Resources.Food.ConvertToBitmapImage();
            _width = (int)image.Width;
            _height = (int)image.Height;

            Image = new Image { Source = image };

            Size = new Size(Position, _width, _height);
        }

        public void Destroy()
        {
            IsDestroyed = true;
        }

        private readonly int _width;
        private readonly int _height;
    }
}