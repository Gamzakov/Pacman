namespace Pacman.GameObjects.Objects
{
    interface IPickableObject : IObject
    {
        bool IsDestroyed { get; }

        int Reward { get; }
        void Destroy();
    }
}