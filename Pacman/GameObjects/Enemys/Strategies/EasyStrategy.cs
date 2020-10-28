using Pacman.GameObjects.Enemys.Interfaces;
using Pacman.Tools;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Pacman.GameObjects.Enemys.Strategies
{
    internal class EasyStrategy : IStrategy
    {
        public void NextMove(IEnemy enemy, Action<Direction> MoveAction, Position pacmanPosition)
        {
            Direction movement;
            Direction farMovement;
            Direction nearMovement;

            farMovement = GetMovementFarFromPacman(enemy, enemy.AvailableDirections, pacmanPosition);

            if (farMovement != Direction.None)
            {
                MoveAction(farMovement);
                return;
            }

            nearMovement = GetMovementNearToPacman(enemy, enemy.AvailableDirections, pacmanPosition);

            if (nearMovement != Direction.None)
            {
                MoveAction(nearMovement);
                return;
            }

            movement = GetRandomMovement(enemy);
            MoveAction(movement);
        }

        private Direction GetMovementFarFromPacman(IEnemy enemy, IEnumerable<Direction> availableDirections, Position pacmanPosition)
        {
            Direction bestMove = Direction.None;
            double minDistance = double.MinValue;

            foreach (Direction direction in availableDirections)
            {
                int movementLeft = 0;
                int movementUp = 0;

                if (direction == Direction.Left)
                    movementLeft = -32;
                if (direction == Direction.Right)
                    movementLeft = 32;

                if (direction == Direction.Up)
                    movementUp = -32;

                if (direction == Direction.Down)
                    movementUp = 32;

                double x = Math.Pow(pacmanPosition.X - (enemy.Position.X + movementLeft), 2);
                double y = Math.Pow(pacmanPosition.Y - (enemy.Position.Y + movementUp), 2);

                double sqrt = Math.Sqrt(x + y);

                if (sqrt > minDistance)
                {
                    bestMove = direction;
                    minDistance = sqrt;
                }
            }

            if (minDistance < 50)
            {
                return bestMove;
            }
            else
            {
                return Direction.None;
            }
        }

        private Direction GetMovementNearToPacman(IEnemy enemy, IEnumerable<Direction> availableDirections, Position pacmanPosition)
        {
            Direction bestMove = Direction.None;
            double minDistance = double.MaxValue;

            foreach (Direction direction in availableDirections)
            {
                int movementLeft = 0;
                int movementUp = 0;

                if (direction == Direction.Left)
                    movementLeft = -32;
                if (direction == Direction.Right)
                    movementLeft = 32;

                if (direction == Direction.Up)
                    movementUp = -32;

                if (direction == Direction.Down)
                    movementUp = 32;

                double x = Math.Pow(pacmanPosition.X - (enemy.Position.X + movementLeft), 2);
                double y = Math.Pow(pacmanPosition.Y - (enemy.Position.Y + movementUp), 2);

                double sqrt = Math.Sqrt(x + y);

                if (sqrt < minDistance)
                {
                    bestMove = direction;
                    minDistance = sqrt;
                }
            }

            if (minDistance < 150)
            {
                return bestMove;
            }
            else
            {
                return Direction.None;
            }
        }

        private Direction GetRandomMovement(IEnemy enemy)
        {
            var availableMovements = enemy.AvailableDirections.ToList();

            var moveIndex = RandomNumberGenerator.Next(availableMovements.Count);

            return availableMovements[moveIndex];
        }
    }
}