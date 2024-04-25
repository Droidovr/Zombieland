using System;
using UnityEngine;

namespace Zombieland.GameScene0.NPCModule.NPCMovingModule
{
    public interface INpcMovingController
    {
        public bool IsMoving { get; }
        public void MoveToTarget(Vector3 targetPosition, float stoppingDistance, Action onTargetReached = null);
        public void FollowTarget(Transform targetTransform, float stoppingDistance, Action onTargetReached = null);
        public void StopMoving();
    }
}
