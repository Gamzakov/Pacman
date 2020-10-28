using System;

namespace Pacman.GameObjects.Enemys.Interfaces
{
    interface IStrategy
    {
        void NextMove(IEnemy enemy, Action<Direction> MoveAction, Position pacmanPosition);
    }
}