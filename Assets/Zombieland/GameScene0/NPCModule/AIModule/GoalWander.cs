using UnityEngine;

namespace Zombieland.GameScene0.NPCModule.AIModule
{
    public class GoalWander : GoalBase
    {
        [SerializeField] private int _maxPriority = 30;
        [SerializeField] private float _prirotyBuildRate = 10f;
        [SerializeField] private float _prirotyDecayRate = 10f;
        private float _currentPriority;

        public override bool CanRun()
        {
            return true;
        }
        
        public override void OnGoalActivated()
        {
            _currentPriority = _maxPriority;
        }

        public override void OnTickGoal()
        {
            if (npcController.NpcMovingController.IsMoving)
            {
                _currentPriority -= _prirotyDecayRate * Time.deltaTime;
            }
            else
            {
                _currentPriority += _prirotyBuildRate * Time.deltaTime;
            }
        }
        
        public override int CalculatePriority()
        {
            return Mathf.FloorToInt(_currentPriority);
        }
    }
}
