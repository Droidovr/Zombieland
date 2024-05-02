using UnityEngine;

namespace Zombieland.GameScene0.NPCModule.AIModule
{
    public class ActionWander : ActionBase
    {
        private System.Type[] supportedGoals = new[] {typeof(GoalIdle)};
        private int _currentWanderPositionIndex;

        public override System.Type[] GetSupportedGoals()
        {
            return supportedGoals;
        }

        public override float Cost()
        {
            return 0f;
        }

        public override void OnActivated()
        {
            SetWanderTarget();
            base.OnActivated();
        }

        public override void OnDeactivated()
        {
            npcController.NpcMovingController.StopMoving();
            base.OnDeactivated();
        }

        public override void OnTick()
        {
            base.OnTick();
        }

        private void SetWanderTarget()
        {
            var wanderTargetPosition =
                npcController.NpcDataController.NpcData.SpawnData.WanderPositionTransforms[_currentWanderPositionIndex].position;
            npcController.NpcMovingController.MoveToTarget(wanderTargetPosition, 0f, SetWanderTarget);

            _currentWanderPositionIndex = _currentWanderPositionIndex + 1 < npcController.NpcDataController.NpcData.SpawnData.WanderPositionTransforms.Count
                ? _currentWanderPositionIndex + 1
                : 0;
        }
    }
}
