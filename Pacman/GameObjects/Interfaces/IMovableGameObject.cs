namespace Pacman.GameObjects.Interfaces
{
    internal interface IMovableGameObject : IGameObject
    {
        Direction LookingAt { get; }
        float SpeedMultiplier { get; }
        float BaseSpeed { get; }

        void Move(Direction direction);
    }
}