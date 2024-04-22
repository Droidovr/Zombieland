using UnityEngine;

namespace Zombieland.GameScene0.NPCModule.AIModule
{
    public class GoalWander : GoalBase
    {
        [SerializeField]
        private int _minPriority = 0;
        [SerializeField]
        private int _maxPriority = 30;
        [SerializeField]
        private float _prirotyBuildRate = 1f;
        [SerializeField]
        private float _prirotyDecayRate = 0.1f;

        private float _currentPriority;

        public override int CalculatePriority()
        {
            return Mathf.FloorToInt(_currentPriority);
        }

        public override bool CanRun()
        {
            return true;
        }

        public override void GoalTick()
        {
            if(NPCController == null) return;
            if (NPCController.MovingController.IsMoving())
            {
                _currentPriority -= _prirotyDecayRate * Time.deltaTime;
            }
            else
            {
                _currentPriority += _prirotyDecayRate * Time.deltaTime;
            }
        }

        public override void OnGoalActivated()
        {
            _currentPriority = _maxPriority;
        }
    }
}
