using Pacman.GameObjects.Interfaces;

using System.Collections.Generic;

namespace Pacman.GameObjects.Enemys.Interfaces
{
    interface IEnemy : IMovableGameObject
    {
        IEnumerable<Direction> AvailableDirections { get; set; }
        IStrategy Strategy { get; }
    }
}