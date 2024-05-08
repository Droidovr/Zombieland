using System;
using UnityEngine;

namespace Zombieland.GameScene0.NPCModule.NPCAnimationModule
{
    public interface INPCAnimationController
    {
        event Action<Vector3> OnAnimatorMoveEvent;
        event Action<bool> OnAnimationAttack;
        event Action OnStep;

        INPCController NPCController { get; }

        void ApplyImpactHandler(Vector3 impactCollisionPosition, Vector3 impactDirection);
    }
}