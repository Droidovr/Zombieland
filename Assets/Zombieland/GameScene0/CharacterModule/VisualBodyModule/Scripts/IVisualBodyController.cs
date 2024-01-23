using UnityEngine;

namespace Zombieland.GameScene0.VisualBodyModule
{
    public interface IVisualBodyController
    {
        GameObject CharacterInScene { get; }
        Collider SensorCollider { get; }
    }
}