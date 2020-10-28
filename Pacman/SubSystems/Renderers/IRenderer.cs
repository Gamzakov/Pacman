using Pacman.Models.Maps.Interfaces;

namespace Pacman.SubSystems.Renderers
{
    internal interface IRenderer
    {
        IMap Map { get; }
    }
}