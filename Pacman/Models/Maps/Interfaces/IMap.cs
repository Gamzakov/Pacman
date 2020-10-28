using Pacman.GameObjects.Enemys.Interfaces;
using Pacman.GameObjects.Objects;
using Pacman.GameObjects.Players;

using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace Pacman.Models.Maps.Interfaces
{
    internal interface IMap
    {
        public IEnumerable<IPickableObject> PickableObjects { get; }
        public IEnumerable<IObject> GameObjects { get; }
        public IEnumerable<IEnemy> Enemies { get; }
        public GameStates GameState { get; }
        public IPlayer Player { get; }

        public float Height { get; set; }
        public float Width { get; set; }

        public Action OnDataUpdated { get; }

        void StartGame();
        void StopGame();

        void OnKeyDown(object sender, KeyEventArgs keyEventArgs);
    }
}