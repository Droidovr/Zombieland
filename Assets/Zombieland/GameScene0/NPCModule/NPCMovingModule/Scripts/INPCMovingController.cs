using System;
using UnityEngine;

namespace Zombieland.GameScene0.NPCModule.NPCMovingModule
{
    public interface INPCMovingController
    {
        public bool IsMoving();
        public void MoveToTarget(Vector3 targetPosition, float stoppingDistance, Action onTargetReached = null);
        public void FollowTarget(Transform targetTransform, float stoppingDistance, Action onTargetReached = null);
        public void StopMoving();
    }
}
