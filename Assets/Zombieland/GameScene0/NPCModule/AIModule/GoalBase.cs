using UnityEngine;

namespace Zombieland.GameScene0.NPCModule.AIModule
{
    public class GoalBase : MonoBehaviour, IGoal
    {
        protected INpcController npcController;
        private TestGoalsUIManager _goalsUIManager;

        private void Awake()
        {
            _goalsUIManager = FindObjectOfType<TestGoalsUIManager>();
        }

        public void Init(INpcController NpcController)
        {
            this.npcController = NpcController;
        }

        private void Update()
        {
            if(npcController == null) return;
            OnTickGoal();
            _goalsUIManager.UpdateGoal(this, GetType().Name, "", CalculatePriority());
        }

        public virtual bool CanRun()
        {
            return false;
        }

        public virtual void OnGoalActivated()
        {
        }

        public virtual void OnTickGoal()
        {
            
        }

        public virtual int CalculatePriority()
        {
            return -1;
        }


        public virtual void OnGoalDeactivated()
        {
        }
    }
}
