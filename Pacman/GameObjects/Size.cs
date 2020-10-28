namespace Pacman.GameObjects
{
    internal struct Size
    {
        public Position Position { get; }
        public int Width { get; }
        public int Height { get; }

        public Size(Position position, int width, int height)
        {
            Position = position;
            Width = width;
            Height = height;
        }
    }
}