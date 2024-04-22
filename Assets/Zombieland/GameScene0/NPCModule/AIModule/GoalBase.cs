using System;
using UnityEngine;

namespace Zombieland.GameScene0.NPCModule.AIModule
{
    public class GoalBase : MonoBehaviour, IGoal
    {
        protected INPCController NPCController;
        private TestGoalsUIManager _goalsUIManager;

        private void Awake()
        {
            _goalsUIManager = FindObjectOfType<TestGoalsUIManager>();
        }

        public void Init(INPCController NPCController)
        {
            this.NPCController = NPCController;
        }

        private void Update()
        {
            GoalTick();
            _goalsUIManager.UpdateGoal(this, GetType().Name, "", CalculatePriority());
        }

        public virtual bool CanRun()
        {
            return false;
        }

        public virtual void OnGoalActivated()
        {
        }

        public virtual void GoalTick()
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
