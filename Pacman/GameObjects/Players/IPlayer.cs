using Pacman.GameObjects.Interfaces;
using Pacman.GameObjects.Objects;

namespace Pacman.GameObjects.Players
{
    interface IPlayer : IMovableGameObject
    {
        int Score { get; }
        byte Health { get; }
        bool IsAlive { get; }
        void PickUp(IPickableObject obj);
        bool Hit();
    }
}