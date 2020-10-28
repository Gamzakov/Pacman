namespace Pacman.GameObjects
{
    internal struct Position
    {
        public float X;
        public float Y;

        public static bool operator ==(Position firstPosition, Position secondPosition)
        {
            return firstPosition.X == secondPosition.X && firstPosition.Y == secondPosition.Y;
        }

        public static bool operator !=(Position firstPosition, Position secondPosition)
        {
            return firstPosition.X != secondPosition.X || firstPosition.Y != secondPosition.Y;
        }
    }
}