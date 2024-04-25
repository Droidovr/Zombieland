using UnityEngine;

namespace Zombieland.GameScene0.NPCModule.AIModule
{
    public class GoalIdle : GoalBase
    {
        [SerializeField] private int _maxPriority = 10;

        public override bool CanRun()
        {
            return true;
        }

        public override int CalculatePriority()
        {
            return _maxPriority;
        }
    }
}
