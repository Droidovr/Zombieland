using UnityEngine;

namespace Zombieland.GameScene0.NPCModule.AIModule
{
    public class GoalChase : GoalBase
    {
        [SerializeField] private int _maxPriority = 60;
        [SerializeField] private float _prirotyDecayRate = 10f;

        private float _currentPriority;
        
        public override bool CanRun()
        {
            return npcController.AwarenessController.IsTargetInFocus;
        }

        public override void OnGoalActivated()
        {
            _currentPriority = _maxPriority;
        }
        
        public override void OnTickGoal()
        {
            if (!npcController.AwarenessController.IsTargetInFocus)
                _currentPriority -= _prirotyDecayRate * Time.deltaTime;
        }
        
        public override int CalculatePriority()
        {
            return Mathf.FloorToInt(_currentPriority);
        }
        
        public override void OnGoalDeactivated()
        {
            
        }
    }
}
