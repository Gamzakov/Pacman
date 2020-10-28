using System.Windows.Controls;

namespace Pacman.GameObjects.Interfaces
{
    interface IGameObject
    {
        public Position Position { get; }
        public Image Image { get; }
        public Size Size { get; }
    }
}